using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Service
{
    public class GiaiTrinhChamCongService : IGiaiTrinhChamCongService
    {
        private readonly HrmDbContext _context;
        public GiaiTrinhChamCongService(HrmDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> CreateGiaiTrinhChamCong(TenantInfo tenantInfo, GiaiTrinhChamCongRequest request)
        {
            var chamCongDiemDanh = await _context.HrmChamCongDiemDanh.FindAsync(request.ChamCongDiemDanhId);

            if (chamCongDiemDanh.TrangThai == TrangThaiEnum.ChoXacNhan || chamCongDiemDanh.TrangThai == TrangThaiEnum.XacNhan)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                    Message = "Giải trình không thể tạo vì đã được xác nhận hoặc chưa được xác nhận"
                };
            }

            var giaiTrinh = new HrmGiaiTrinhChamCong
            {
                HrmChamCongDiemDanh = chamCongDiemDanh,
                LyDo = request.LyDo,
                HrmCongKhaiBao = _context.HrmCongKhaiBao.Find(request.CongKhaiBaoId),
                TrangThai = false,
                NguoiXacNhan = _context.Users.Find(request.NguoiXacNhan),
            };

            await _context.HrmGiaiTrinhChamCong.AddAsync(giaiTrinh);
            await _context.SaveChangesAsync();

            // update trang thai cham cong diem danh
            chamCongDiemDanh.TrangThai = TrangThaiEnum.ChoXacNhan;
            _context.HrmChamCongDiemDanh.Update(chamCongDiemDanh);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = giaiTrinh.Id,
                IsSucceeded = true
            };
        }

        public Task<CudResponseDto> DeleteGiaiTrinhChamCong(TenantInfo tenantInfo, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HrmGiaiTrinhChamCong>> GetGiaiTrinhChamCong(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }

        public Task<HrmGiaiTrinhChamCong> GetGiaiTrinhChamCongById(TenantInfo tenantInfo, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HrmGiaiTrinhChamCong>> GetGiaiTrinhChamCongByUserId(TenantInfo tenantInfo, Guid userId)
        {
            return await _context.HrmGiaiTrinhChamCong
                                                        .Include(x => x.HrmChamCongDiemDanh).ThenInclude(h => h.User)
                                                        .Include(x => x.HrmCongKhaiBao)
                                                        .Include(x => x.HrmChamCongDiemDanh).ThenInclude(h => h.HrmTrangThaiChamCong)
                                                        .Where(x => x.NguoiXacNhan.Id == userId
                                                        && x.TrangThai == false
                                                        && x.NguoiXacNhan.Tenant.Id == tenantInfo.TenantId)
                                                      .OrderByDescending(o => o.CreatedAt)
                                                      .ToListAsync();
        }

        public Task<CudResponseDto> UpdateGiaiTrinhChamCong(Guid id, TenantInfo tenantInfo, GiaiTrinhChamCongRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<CudResponseDto> XacNhanGiaiTrinh(TenantInfo tenantInfo, XacNhanGiaiTrinhRequest request)
        {
            var giaitrinh = await _context.HrmGiaiTrinhChamCong
                .Include(h => h.HrmCongKhaiBao)
                .Include(h => h.HrmChamCongDiemDanh).ThenInclude(HrmChamCongDiemDanh => HrmChamCongDiemDanh.HrmCongKhaiBao)
                .Include(h => h.HrmChamCongDiemDanh).ThenInclude(HrmChamCongDiemDanh => HrmChamCongDiemDanh.HrmTrangThaiChamCong)
                .Include(h => h.NguoiXacNhan)
                .FirstOrDefaultAsync(h => h.Id == request.GiaiTrinhChamCongId);

            if (giaitrinh == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                    Message = "Không tìm thấy giải trình"
                };
            }

            if (request.IsXacNhan)
            {
               giaitrinh.TrangThai = true;
               giaitrinh.HrmChamCongDiemDanh.TrangThai = TrangThaiEnum.XacNhan;
               giaitrinh.HrmChamCongDiemDanh.HrmCongKhaiBao = giaitrinh.HrmCongKhaiBao;
               giaitrinh.HrmChamCongDiemDanh.HrmTrangThaiChamCong = await _context.HrmTrangThaiChamCong
               .FirstOrDefaultAsync(t => t.MaTrangThai == MaTrangThaiChamCongSystem.HOP_LE);

               giaitrinh.UpdatedAt = DateTime.UtcNow;
               giaitrinh.HrmChamCongDiemDanh.UpdatedAt = DateTime.UtcNow;
            } else
            {
                giaitrinh.TrangThai = false;
                giaitrinh.HrmChamCongDiemDanh.TrangThai = TrangThaiEnum.ChoGiaiTrinh;
                giaitrinh.HrmChamCongDiemDanh.HrmCongKhaiBao = null;
                giaitrinh.NguoiXacNhan = null;

                giaitrinh.UpdatedAt = DateTime.UtcNow;
                giaitrinh.HrmChamCongDiemDanh.UpdatedAt = DateTime.UtcNow;
            }

            _context.HrmGiaiTrinhChamCong.Update(giaitrinh);
            _context.HrmChamCongDiemDanh.Update(giaitrinh.HrmChamCongDiemDanh);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = giaitrinh.Id,
                IsSucceeded = true
            };
        }
    }
}

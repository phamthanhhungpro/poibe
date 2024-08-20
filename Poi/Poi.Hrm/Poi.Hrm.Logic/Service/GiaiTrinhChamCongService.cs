using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;
using System.Linq.Expressions;

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
                GhiChu = request.GhiChu
            };

            await _context.HrmGiaiTrinhChamCong.AddAsync(giaiTrinh);
            await _context.SaveChangesAsync();

            // update trang thai cham cong diem danh
            chamCongDiemDanh.TrangThai = TrangThaiEnum.ChoXacNhan;
            chamCongDiemDanh.NguoiXacNhan = giaiTrinh.NguoiXacNhan;
            chamCongDiemDanh.UpdatedAt = DateTime.UtcNow;
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
            Expression<Func<HrmGiaiTrinhChamCong, bool>> filterExpression = x => true;
            // check scope
            if (tenantInfo.IsNeedCheckScope && tenantInfo.RequestScopeCode != null && tenantInfo.RequestScopeCode.Count > 0)
            {
                // check scope
                var user = await _context.Users
                                        .Include(x => x.LanhDaoPhongBan)
                                        .FirstOrDefaultAsync(x => x.Id == tenantInfo.UserId);

                var userInPhongBanIds = user.LanhDaoPhongBan.Where(u => u.ThanhVien != null)
                                    .SelectMany(u => u.ThanhVien.Select(t => t.Id)).ToList();

                switch (tenantInfo.RequestScopeCode.First())
                {
                    // Tất cả user của đơn vị
                    case ScopeCode.XNCC_ALL:
                        filterExpression = x => true;
                        break;

                    // User của phòng ban
                    case ScopeCode.XNCC_PHONGBAN:
                        filterExpression = x => userInPhongBanIds.Any(u => u == x.NguoiXacNhan.Id) || x.NguoiXacNhan.Id == userId;
                        break;

                    default:
                        // Handle other cases here
                        break;
                }
            }

            return await _context.HrmGiaiTrinhChamCong.Include(x => x.HrmChamCongDiemDanh).ThenInclude(h => h.User)
                                                      .Include(x => x.HrmCongKhaiBao)
                                                      .Include(x => x.HrmChamCongDiemDanh).ThenInclude(h => h.HrmTrangThaiChamCong)
                                                      .Where(filterExpression)
                                                      .Where(x => x.TrangThai == false && x.NguoiXacNhan.Tenant.Id == tenantInfo.TenantId)
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

            var congXacNhan = await _context.HrmCongKhaiBao.FirstOrDefaultAsync(x => x.Id == request.CongXacNhanId);

            if (request.IsXacNhan)
            {
               giaitrinh.TrangThai = true;
               giaitrinh.HrmChamCongDiemDanh.TrangThai = TrangThaiEnum.XacNhan;
               giaitrinh.HrmChamCongDiemDanh.HrmCongXacNhan = congXacNhan;
               giaitrinh.HrmChamCongDiemDanh.LyDo = giaitrinh.LyDo;
               giaitrinh.HrmChamCongDiemDanh.GhiChu = giaitrinh.GhiChu;

               giaitrinh.HrmChamCongDiemDanh.HrmCongXacNhan = congXacNhan;
               giaitrinh.HrmChamCongDiemDanh.HrmTrangThaiChamCong = await _context.HrmTrangThaiChamCong.FirstOrDefaultAsync(t => t.MaTrangThai == MaTrangThaiChamCongSystem.HOP_LE);
                
               giaitrinh.UpdatedAt = DateTime.UtcNow;
               giaitrinh.HrmChamCongDiemDanh.UpdatedAt = DateTime.UtcNow;
            } else
            {
                giaitrinh.TrangThai = false;
                giaitrinh.HrmChamCongDiemDanh.TrangThai = TrangThaiEnum.ChoGiaiTrinh;
                giaitrinh.HrmChamCongDiemDanh.NguoiXacNhan = null;
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

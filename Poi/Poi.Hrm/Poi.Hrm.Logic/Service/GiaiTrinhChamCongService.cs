using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
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

        public Task<CudResponseDto> UpdateGiaiTrinhChamCong(Guid id, TenantInfo tenantInfo, GiaiTrinhChamCongRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

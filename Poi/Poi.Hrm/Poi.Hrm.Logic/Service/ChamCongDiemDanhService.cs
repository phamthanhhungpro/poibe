using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Dtos;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Service
{
    public class ChamCongDiemDanhService : IChamCongDiemDanhService
    {
        private readonly HrmDbContext _context;
        public ChamCongDiemDanhService(HrmDbContext context)
        {

            _context = context;

        }

        public async Task<CudResponseDto> CreateChamCongDiemDanh(TenantInfo tenantInfo, ChamCongDiemDanhRequest request)
        {
            var timeChamCongRa = new DateTime(request.NgayChamCong.Value.Year, request.NgayChamCong.Value.Month, request.NgayChamCong.Value.Day, 16, 0, 0);
            var timeChamCongVao = new DateTime(request.NgayChamCong.Value.Year, request.NgayChamCong.Value.Month, request.NgayChamCong.Value.Day, 10, 0, 0);

            if (request.NgayChamCong == null)
            {
                request.NgayChamCong = DateTime.Now;
            }

            var data = new List<HrmChamCongDiemDanh>();

            var updatedDataUserIds = await _context.HrmChamCongDiemDanh
               .Include(x => x.User)
               .Where(x => x.ThoiGian <= request.NgayChamCong.Value.ToUniversalTime()
                        && x.ThoiGian >= request.NgayChamCong.Value.ToStartOfDayUtc())
               .Select(x => x.User.Id)
               .ToListAsync();

            var diemDanhHistoryToday = _context.HrmDiemDanhHistory
               .Include(x => x.User)
               .Where(x => x.User.IsActive && !updatedDataUserIds.Contains(x.User.Id))
               .Where(x => x.Time <= request.NgayChamCong.Value.ToUniversalTime()
                        && x.Time >= request.NgayChamCong.Value.ToStartOfDayUtc())
               .GroupBy(x => x.User)
               .Select(x => new DiemDanhHistoryGroupDto
               {
                   User = x.Key,
                   Times = x.Select(x => x.Time)
               })
               .ToList();

            var listUserIds = diemDanhHistoryToday.Select(x => x.User.Id).ToList();
            var listUser = await _context.Users.Where(x => listUserIds.Contains(x.Id)).ToListAsync();
            var defaultCongKhaiBao = await _context.HrmCongKhaiBao.FirstOrDefaultAsync(x => x.IsDefault && x.IsSystem);
            var defaultTrangThaiChamCong = await _context.HrmTrangThaiChamCong.Where(x => x.IsSystem).ToListAsync();

            foreach (var item in diemDanhHistoryToday)
            {
                var model = new HrmChamCongDiemDanh()
                {
                    User = listUser.FirstOrDefault(x => x.Id == item.User.Id),
                    HrmCongKhaiBao = defaultCongKhaiBao,
                    ThoiGian = request.NgayChamCong.Value,
                    HrmTrangThaiChamCong = defaultTrangThaiChamCong.FirstOrDefault(x => x.MaTrangThai == MaTrangThaiChamCongSystem.HOP_LE)
                };

                model.TrangThai = model.HrmTrangThaiChamCong.YeuCauGiaiTrinh ? TrangThaiEnum.ChoGiaiTrinh : TrangThaiEnum.XacNhan;

                if (!item.Times.Any())
                {
                    model.HrmTrangThaiChamCong = defaultTrangThaiChamCong.FirstOrDefault(x => x.MaTrangThai == MaTrangThaiChamCongSystem.CHUA_CHAM_CONG);
                    model.TrangThai = model.HrmTrangThaiChamCong.YeuCauGiaiTrinh ? TrangThaiEnum.ChoGiaiTrinh : TrangThaiEnum.XacNhan;
                    data.Add(model);
                    continue;
                }
                var minTime = item.Times.Min();
                var maxTime = item.Times.Max();

                if (minTime.Hour > 10)
                {
                    // chua cham cong vao
                    model.HrmTrangThaiChamCong = defaultTrangThaiChamCong.FirstOrDefault(x => x.MaTrangThai == MaTrangThaiChamCongSystem.CHUA_CHAM_CONG_VAO);
                    model.TrangThai = model.HrmTrangThaiChamCong.YeuCauGiaiTrinh ? TrangThaiEnum.ChoGiaiTrinh : TrangThaiEnum.XacNhan;
                    data.Add(model);
                    continue;
                }

                if (maxTime.Hour < 16)
                {
                    // chua cham cong ra
                    model.HrmTrangThaiChamCong = defaultTrangThaiChamCong.FirstOrDefault(x => x.MaTrangThai == MaTrangThaiChamCongSystem.CHUA_CHAM_CONG_RA);
                    model.TrangThai = model.HrmTrangThaiChamCong.YeuCauGiaiTrinh ? TrangThaiEnum.ChoGiaiTrinh : TrangThaiEnum.XacNhan;
                    data.Add(model);
                    continue;
                }

                data.Add(model);

            }

            // Data user không có dữ liệu điểm danh trong ngày
            var listUserNotInData = await _context.Users.Where(x => !updatedDataUserIds.Contains(x.Id) && !listUserIds.Contains(x.Id) && x.IsActive).ToListAsync();
            foreach (var user in listUserNotInData)
            {
                var model = new HrmChamCongDiemDanh()
                {
                    User = user,
                    HrmCongKhaiBao = defaultCongKhaiBao,
                    ThoiGian = request.NgayChamCong.Value,
                    HrmTrangThaiChamCong = defaultTrangThaiChamCong.FirstOrDefault(x => x.MaTrangThai == "CHUA_CHAM_CONG")
                };
                model.TrangThai = model.HrmTrangThaiChamCong.YeuCauGiaiTrinh ? TrangThaiEnum.ChoGiaiTrinh : TrangThaiEnum.XacNhan;
                data.Add(model);
            }

            _context.HrmChamCongDiemDanh.AddRange(data);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true
            };
        }

        public Task<CudResponseDto> DeleteChamCongDiemDanh(TenantInfo tenantInfo, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<HrmChamCongDiemDanh>> GetChamCongDiemDanh(TenantInfo tenantInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HrmChamCongDiemDanh>> GetChamCongDiemDanhByUserId(TenantInfo tenantInfo, Guid userId, DateTime start, DateTime end)
        {
            return await _context.HrmChamCongDiemDanh
                .Include(x => x.User)
                .Include(x => x.HrmCongKhaiBao)
                .Include(x => x.HrmTrangThaiChamCong)
                .Where(x => x.User.Id == userId && x.ThoiGian >= start && x.ThoiGian <= end)
                .ToListAsync();
        }

        public Task<CudResponseDto> UpdateChamCongDiemDanh(Guid id, TenantInfo tenantInfo, ChamCongDiemDanhRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

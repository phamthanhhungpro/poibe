using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Dtos;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Constants;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;
using System.Linq.Expressions;

namespace Poi.Hrm.Logic.Service
{
    public class ChamCongDiemDanhService : IChamCongDiemDanhService
    {
        private readonly HrmDbContext _context;
        public ChamCongDiemDanhService(HrmDbContext context)
        {

            _context = context;

        }

        public async Task<List<BangChamCongDto>> BangChamCong(TenantInfo tenantInfo, BangChamCongRequest request)
        {
            Expression<Func<User, bool>> filterExpression = x => true;
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
                    case ScopeCode.LSCC_ALL:
                        filterExpression = x => true;
                        break;

                        // User của phòng ban
                     case ScopeCode.LSCC_PHONGBAN:
                        filterExpression = x => userInPhongBanIds.Any(u => u == x.Id) || x.Id == tenantInfo.UserId;
                        break;

                    default:
                        // Handle other cases here
                        break;
                }
            }

            var startOfMonth = request.Time.ToStartOfMonth();
            var endOfMonth = request.Time.ToEndOfMonth();

            // Generate a list of all dates in the month based on the request time
            var allDaysInMonth = new List<DateTime>();
            for (var date = startOfMonth; date <= endOfMonth; date = date.AddDays(1))
            {
                allDaysInMonth.Add(date);
            }

            var usersWithAttendance = await _context.Users
                .AsNoTracking()
                .Where(x => x.Tenant.Id == tenantInfo.TenantId && x.IsActive)
                .Where(filterExpression)
                .Include(x => x.HrmChamCongDiemDanh).ThenInclude(x => x.HrmCongXacNhan)
                .ToListAsync(); // Load the data into memory

            var data = usersWithAttendance.Select(x => new BangChamCongDto
            {
                UserId = x.Id,
                UserName = x.UserName,
                FullName = x.FullName,
                Days = allDaysInMonth
                        .Select(day => x.HrmChamCongDiemDanh
                                        .Where(d => d.ThoiGian.AddHours(7).Date == day.Date)
                                        .Select(d => d.HrmCongXacNhan?.MaCongKhaiBao)
                                        .FirstOrDefault() ?? "")
                        .ToList()
            }).ToList();

            return data;
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
                    ThoiGian = request.NgayChamCong.Value.ToUniversalTime(),
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
                    ThoiGian = request.NgayChamCong.Value.ToUniversalTime(),
                    HrmTrangThaiChamCong = defaultTrangThaiChamCong.FirstOrDefault(x => x.MaTrangThai == "CHUA_CHAM_CONG")
                };
                model.TrangThai = (model.HrmTrangThaiChamCong != null && model.HrmTrangThaiChamCong.YeuCauGiaiTrinh) ? TrangThaiEnum.ChoGiaiTrinh : TrangThaiEnum.XacNhan;
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

        public async Task<CudResponseDto> DiemDanhThuCong(TenantInfo tenantInfo, DiemDanhThuCongRequest request)
        {
            var startDay = DateTime.UtcNow.ToStartOfDay();
            var endDay = DateTime.UtcNow.ToEndOfDay();
            // check if user has checkin today
            var check = await _context.HrmChamCongDiemDanh.AnyAsync(x => x.User.Id == tenantInfo.UserId && x.ThoiGian >= startDay && x.ThoiGian <= endDay);

            if (check)
            {
                return new CudResponseDto
                {
                    Message = "Bạn đã điểm danh hôm nay rồi",
                    IsSucceeded = false
                };
            }

            var defaultCongKhaiBao = await _context.HrmCongKhaiBao.FirstOrDefaultAsync(x => x.Id == request.CongKhaiBaoId);
            var defaultTrangThaiChamCong = await _context.HrmTrangThaiChamCong.Where(x => x.IsSystem && x.MaTrangThai == MaTrangThaiChamCongSystem.CHAM_CONG_THU_CONG).FirstOrDefaultAsync();


            var model = new HrmChamCongDiemDanh()
            {
                User = await _context.Users.FirstOrDefaultAsync(x => x.Id == tenantInfo.UserId),
                HrmCongKhaiBao = defaultCongKhaiBao,
                ThoiGian = DateTime.Now.ToUniversalTime(),
                HrmTrangThaiChamCong = defaultTrangThaiChamCong,
                TrangThai = TrangThaiEnum.ChoXacNhan,
                LyDo = request.LyDo,
                GhiChu = request.GhiChu,
                NguoiXacNhanId = request.NguoiXacNhanId
            };

            await _context.HrmChamCongDiemDanh.AddAsync(model);
            await _context.SaveChangesAsync();

            var giaiTrinh = new HrmGiaiTrinhChamCong
            {
                HrmChamCongDiemDanh = model,
                LyDo = request.LyDo,
                GhiChu = request.GhiChu,
                HrmCongKhaiBao = defaultCongKhaiBao,
                TrangThai = false,
                NguoiXacNhan = _context.Users.Find(request.NguoiXacNhanId),
            };

            await _context.HrmGiaiTrinhChamCong.AddAsync(giaiTrinh);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Message = "Điểm danh thành công"
            };
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

        public async Task<HrmChamCongDiemDanh> GetDetailChamCong(TenantInfo tenantInfo, Guid id)
        {
            return await _context.HrmChamCongDiemDanh
                        .Include(x => x.User)
                        .Include(x => x.HrmCongKhaiBao)
                        .Include(x => x.HrmTrangThaiChamCong)
                        .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<CudResponseDto> UpdateChamCongDiemDanh(Guid id, TenantInfo tenantInfo, ChamCongDiemDanhRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

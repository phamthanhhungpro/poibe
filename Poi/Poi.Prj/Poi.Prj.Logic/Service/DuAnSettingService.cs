using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;
using System.Text.Json;

namespace Poi.Prj.Logic.Service
{
    public class DuAnSettingService : IDuAnSettingService
    {
        private readonly PrjDbContext _context;
        public DuAnSettingService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(DuAnSettingRequest request, TenantInfo info)
        {
            // Check if the key already exists
            var check = _context.PrjDuAnSetting.Any(x => x.Key == request.Key && x.DuAnNvChuyenMonId == request.DuAnId);
            if (check)
            {
                return new CudResponseDto
                {
                    Message = "Key đã tồn tại",
                    IsSucceeded = false
                };
            }
            var entity = new PrjDuAnSetting
            {
                DuAnNvChuyenMonId = request.DuAnId,
                Key = request.Key,
                Value = request.Value,
                MoTa = request.MoTa
            };

            _context.PrjDuAnSetting.Add(entity);
            await _context.SaveChangesAsync();
            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Thêm thành công",
                IsSucceeded = true
            };

        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjDuAnSetting.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return new CudResponseDto
            {
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjDuAnSetting> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjDuAnSetting.FindAsync(id);
        }

        public async Task<IEnumerable<PrjDuAnSetting>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjDuAnSetting.Where(x => x.DuAnNvChuyenMonId == DuanId).ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAllSetting(UpdateDuAnSettingRequest request, TenantInfo info)
        {
            var settings = await _context.PrjDuAnSetting.Where(x => x.DuAnNvChuyenMonId == request.DuAnId).ToListAsync();

            // clear then add new settings
            _context.PrjDuAnSetting.RemoveRange(settings);

            foreach(var item in request.Settings)
            {
                var entity = new PrjDuAnSetting
                {
                    DuAnNvChuyenMonId = request.DuAnId,
                    Key = item.Key,
                    Value = item.Value,
                };
                _context.PrjDuAnSetting.Add(entity);
            }

            foreach(var item in request.JsonSettings)
            {
                var jsonSettingEntity = new PrjDuAnSetting
                {
                    DuAnNvChuyenMonId = request.DuAnId,
                    Key = item.Key,
                    JsonValue = JsonSerializer.Serialize(item.Value)
                };

                _context.PrjDuAnSetting.Add(jsonSettingEntity);
            }

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, DuAnSettingRequest request, TenantInfo info)
        {
            var entity = await _context.PrjDuAnSetting.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            entity.Value = request.Value;
            entity.MoTa = request.MoTa;
            await _context.SaveChangesAsync();
            return new CudResponseDto
            {
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };
        }
    }
}

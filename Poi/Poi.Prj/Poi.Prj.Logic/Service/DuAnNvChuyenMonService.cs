using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using Poi.Shared.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Service
{
    public class DuAnNvChuyenMonService : IDuAnNvChuyenMonService
    {
        private readonly PrjDbContext _context;

        public DuAnNvChuyenMonService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(DuAnNvChuyenMonRequest request, TenantInfo info)
        {
            var entity = new PrjDuAnNvChuyenMon
            {
                TenDuAn = request.TenDuAn,
                MoTaDuAn = request.MoTaDuAn,
                ToNhomId = request.ToNhomId,
                PhongBanBoPhanId = request.PhongBanBoPhanId,
                QuanLyDuAnId = request.QuanLyDuAnId,
                ThoiGianBatDau = request.ThoiGianBatDau.ToUTC(),
                ThoiGianKetThuc = request.ThoiGianKetThuc.ToUTC(),
                LinhVucId = request.LinhVucId,
                IsNhiemVuChuyenMon = request.IsNhiemVuChuyenMon,
                TenantId = info.TenantId
            };
            entity.CreatedBy = info.UserId;
            entity.ThanhVienDuAn = _context.Users.Where(x => request.ThanhVienDuAnIds.Contains(x.Id)).ToList();

            _context.PrjDuAnNvChuyenMon.Add(entity);
            await _context.SaveChangesAsync();

            if (request.IsSaoChepThietLap && request.DuAnCanSaoChepId.HasValue)
            {
                // Sao chép thiết lập từ dự án khác
                var duAnCanSaoChep = await _context.PrjDuAnNvChuyenMon
                    .Include(x => x.DuAnSetting)
                    .Include(x => x.NhomCongViec)
                    .Include(x => x.LoaiCongViec)
                    .Include(x => x.TagCongViec)
                    .Include(x => x.TagComment)
                    .Include(x => x.Kanban)
                    .FirstOrDefaultAsync(x => x.Id == request.DuAnCanSaoChepId.Value);

                if (duAnCanSaoChep != null)
                {
                    entity.DuAnSetting = duAnCanSaoChep.DuAnSetting.Select(x => new PrjDuAnSetting
                    {
                        DuAnNvChuyenMonId = entity.Id,
                        Key = x.Key,
                        JsonValue = x.JsonValue,
                    }).ToList();

                    entity.NhomCongViec = duAnCanSaoChep.NhomCongViec.Select(x => new PrjNhomCongViec
                    {
                        DuAnNvChuyenMonId = entity.Id,
                        MaNhomCongViec = x.MaNhomCongViec,
                        TenNhomCongViec = x.TenNhomCongViec,
                        MoTa = x.MoTa,
                        TenantId = info.TenantId
                    }).ToList();

                    entity.LoaiCongViec = duAnCanSaoChep.LoaiCongViec.Select(x => new PrjLoaiCongViec
                    {
                        DuAnNvChuyenMonId = entity.Id,
                        MaLoaiCongViec = x.MaLoaiCongViec,
                        TenLoaiCongViec = x.TenLoaiCongViec,
                        MoTa = x.MoTa,
                        TenantId = info.TenantId
                    }).ToList();

                    entity.TagCongViec = duAnCanSaoChep.TagCongViec.Select(x => new PrjTagCongViec
                    {
                        DuAnNvChuyenMonId = entity.Id,
                        TenTag = x.TenTag,
                        MaTag = x.MaTag,
                        MauSac = x.MauSac,
                        TenantId = info.TenantId
                    }).ToList();

                    entity.TagComment = duAnCanSaoChep.TagComment.Select(x => new PrjTagComment
                    {
                        DuAnNvChuyenMonId = entity.Id,
                        TenTag = x.TenTag,
                        MaTag = x.MaTag,
                        MauSac = x.MauSac,
                        TenantId = info.TenantId
                    }).ToList();

                    entity.Kanban = duAnCanSaoChep.Kanban.Select(x => new PrjKanban
                    {
                        DuAnNvChuyenMonId = entity.Id,
                        TenCot = x.TenCot,
                        MoTa = x.MoTa,
                        TrangThaiCongViec = x.TrangThaiCongViec,
                        ThuTu = x.ThuTu,
                        YeuCauXacNhan = x.YeuCauXacNhan,
                    }).ToList();

                    _context.PrjDuAnNvChuyenMon.Update(entity);
                    await _context.SaveChangesAsync();
                }


            } else
            {
                // Khởi tạo mặc định 1 số thông tin cho Dự án, Nhiệm vụ

                // Khởi tạo Tag

                // Khởi tạo nhóm công việc
                var nhomCongViec = new PrjNhomCongViec
                {
                    TenNhomCongViec = "Chưa xác định",
                    MoTa = "Nhóm công việc mặc định",
                    MaNhomCongViec = "CHUAXACDINH",
                    DuAnNvChuyenMonId = entity.Id,
                    TenantId = info.TenantId
                };

                _context.PrjNhomCongViec.Add(nhomCongViec);
                await _context.SaveChangesAsync();

                // Khởi tạo loại công việc
                var loaiCongViec = new PrjLoaiCongViec
                {
                    TenLoaiCongViec = "Chưa xác định",
                    MoTa = "Loại công việc mặc định",
                    MaLoaiCongViec = "CHUAXACDINH",
                    DuAnNvChuyenMonId = entity.Id,
                    TenantId = info.TenantId
                };

                _context.PrjLoaiCongViec.Add(loaiCongViec);
                await _context.SaveChangesAsync();

                // Khởi tạo trạng thái công việc mặc định
                var jsonSettingEntity = new PrjDuAnSetting
                {
                    DuAnNvChuyenMonId = entity.Id,
                    Key = TrangThaiCongViecHelper.TrangThaiSettingKey,
                    JsonValue = $@"[{{""key"": ""{TrangThaiCongViecHelper.DefaultTrangThaiKey}"", ""value"": ""{TrangThaiCongViecHelper.DefaultTrangThaiValue}"", ""yeuCauXacNhan"": false }}]"
                };

                _context.PrjDuAnSetting.Add(jsonSettingEntity);

                await _context.SaveChangesAsync();
            }

            return new CudResponseDto
            {
                Id = entity.Id,
                Message = "Success",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var entity = await _context.PrjDuAnNvChuyenMon.FindAsync(id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Not found",
                    IsSucceeded = false
                };
            }

            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;

            _context.PrjDuAnNvChuyenMon.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Success",
                IsSucceeded = true
            };
        }

        public async Task<PrjDuAnNvChuyenMon> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjDuAnNvChuyenMon
                .Include(x => x.ThanhVienDuAn)
                .Include(x => x.QuanLyDuAn)
                .Include(x => x.NhomCongViec)
                .Include(x => x.DuAnSetting)
                .Include(x => x.LoaiCongViec)
                .Include(x => x.TagCongViec)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<PrjDuAnNvChuyenMon>> GetNoPaging(bool isNvChuyenMon, TenantInfo info, bool isGetAll)
        {
            if(isGetAll)
            {
                return await _context.PrjDuAnNvChuyenMon
                                    .Where(x => x.TenantId == info.TenantId)
                                    .ToListAsync();
            }
            return await _context.PrjDuAnNvChuyenMon
                                .Include(x => x.ThanhVienDuAn)
                                .Include(x => x.QuanLyDuAn)
                                .Include(x => x.LinhVuc)
                                .Include(x => x.ToNhom).ThenInclude(x => x.LanhDao)
                                .Include(x => x.ToNhom).ThenInclude(x => x.ThanhVien)

                                .Include(x => x.PhongBanBoPhan).ThenInclude(x => x.QuanLy)
                                .Where(x => x.IsNhiemVuChuyenMon == isNvChuyenMon && x.TenantId == info.TenantId)
                                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, DuAnNvChuyenMonRequest request, TenantInfo info)
        {
            var entity = await _context.PrjDuAnNvChuyenMon
                                        .Include(x => x.ThanhVienDuAn)
                                        .FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Not found",
                    IsSucceeded = false
                };
            }

            entity.TenDuAn = request.TenDuAn;
            entity.MoTaDuAn = request.MoTaDuAn;
            entity.ToNhomId = request.ToNhomId;
            entity.PhongBanBoPhanId = request.PhongBanBoPhanId;
            entity.QuanLyDuAnId = request.QuanLyDuAnId;
            entity.ThoiGianBatDau = request.ThoiGianBatDau;
            entity.ThoiGianKetThuc = request.ThoiGianKetThuc;
            entity.LinhVucId = request.LinhVucId;
            entity.IsNhiemVuChuyenMon = request.IsNhiemVuChuyenMon;

            entity.ThanhVienDuAn = _context.Users.Where(x => request.ThanhVienDuAnIds.Contains(x.Id)).ToList();

            _context.PrjDuAnNvChuyenMon.Update(entity);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Success",
                IsSucceeded = true
            };
        }
    }
}

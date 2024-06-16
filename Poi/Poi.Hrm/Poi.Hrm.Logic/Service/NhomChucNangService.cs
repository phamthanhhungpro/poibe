using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Service
{
    public class NhomChucNangService : INhomChucNangService
    {
        private readonly HrmDbContext _hrmDbContext;
        public NhomChucNangService(HrmDbContext hrmDbContext)
        {
            _hrmDbContext = hrmDbContext;
        }

        public async Task<CudResponseDto> AssignChucNang(AssignChucNangToNhomChucNangRequest request, TenantInfo tenantInfo)
        {
            var nhomChucNang = await _hrmDbContext.HrmNhomChucNang
                                .Include(x => x.HrmChucNang)
                                .FirstOrDefaultAsync(a => a.Id == request.NhomChucNangId);

            if (nhomChucNang == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                    Message = "Nhom chuc nang khong ton tai"
                };
            }

            var chucNangs = await _hrmDbContext.HrmChucNang.Where(x => request.ChucNangIds.Contains(x.Id)).ToListAsync();
            if (chucNangs == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                    Message = "Chuc nang khong ton tai"
                };
            }

            nhomChucNang.HrmChucNang = chucNangs;

            _hrmDbContext.Update(nhomChucNang);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = nhomChucNang.Id,
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> AssignPermission(AssignNhomChucNangToVaiTroRequest request, TenantInfo tenantInfo)
        {
            var vaiTro = await _hrmDbContext.HrmVaiTro
                                .Include(x => x.HrmNhomChucNang)
                                .FirstOrDefaultAsync(a => a.Id == request.VaiTroId);

            if (vaiTro == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                    Message = "Vai tro khong ton tai"
                };
            }

            var nhomChucNangs = await _hrmDbContext.HrmNhomChucNang.Where(x => request.NhomChucNangIds.Contains(x.Id)).ToListAsync();
            if (nhomChucNangs == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                    Message = "Nhom chuc nang khong ton tai"
                };
            }

            vaiTro.HrmNhomChucNang = nhomChucNangs;

            _hrmDbContext.Update(vaiTro);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = vaiTro.Id,
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> CreateNhomChucNang(TenantInfo tenantInfo, NhomChucNangRequest NhomChucNang)
        {
            var nhomChucNang = new HrmNhomChucNang
            {
                TenNhomChucNang = NhomChucNang.TenNhomChucNang,
                MoTa = NhomChucNang.MoTa,
                Tenant = _hrmDbContext.Tenants.Find(tenantInfo.TenantId)
            };

            await _hrmDbContext.HrmNhomChucNang.AddAsync(nhomChucNang);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = nhomChucNang.Id,
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteNhomChucNang(TenantInfo tenantInfo, Guid id)
        {
            var nhomChucNang = await _hrmDbContext.HrmNhomChucNang.FindAsync(id);
            if (nhomChucNang == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false
                };
            }

            nhomChucNang.IsDeleted = true;
            nhomChucNang.DeletedAt = DateTime.UtcNow;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = nhomChucNang.Id,
                IsSucceeded = true
            };
        }

        public async Task<List<HrmNhomChucNang>> GetNhomChucNang(TenantInfo tenantInfo)
        {
            return await _hrmDbContext.HrmNhomChucNang
                .Include(x => x.HrmVaiTro)
                .Where(x => x.TenantId == tenantInfo.TenantId)
                .ToListAsync();
        }

        public async Task<HrmNhomChucNang> GetNhomChucNangById(TenantInfo tenantInfo, Guid id)
        {
            return await _hrmDbContext.HrmNhomChucNang
                .Include(x => x.HrmVaiTro)
                .Include(x => x.HrmChucNang)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PagingResponse<HrmNhomChucNang>> GetPagingNhomChucNang(PagingRequest request, TenantInfo info)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;

            var query = _hrmDbContext.HrmNhomChucNang
                .Where(x => x.TenantId == info.TenantId)
                .OrderByDescending(o => o.CreatedAt).AsNoTracking();

            var data = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();

            return new PagingResponse<HrmNhomChucNang>()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Count = count,
                Items = data
            };
        }

        public async Task<CudResponseDto> UpdateNhomChucNang(Guid id, TenantInfo tenantInfo, NhomChucNangRequest NhomChucNang)
        {
            var nhomChucNang = await _hrmDbContext.HrmNhomChucNang.FindAsync(id);
            if (nhomChucNang == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false
                };
            }

            nhomChucNang.TenNhomChucNang = NhomChucNang.TenNhomChucNang;
            nhomChucNang.MoTa = NhomChucNang.MoTa;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = nhomChucNang.Id,
                IsSucceeded = true
            };
        }
    }
}

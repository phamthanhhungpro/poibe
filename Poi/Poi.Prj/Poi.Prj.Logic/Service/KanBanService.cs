using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Service
{
    public class KanbanService : IKanbanService
    {
        private readonly PrjDbContext _context;

        public KanbanService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(KanbanRequest request, TenantInfo info)
        {
            // Check if the Kanban already exists
            var isExist = await _context.PrjKanban.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                            && x.ThuTu == request.ThuTu);

            if (isExist)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    Message = "Thứ tự đã tồn tại",
                    IsSucceeded = false
                };
            };

            // check trangthai exists
            var isExistTrngTahi = await _context.PrjKanban.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                && x.TrangThaiCongViec == request.TrangThaiCongViec);

            if (isExistTrngTahi)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    Message = "Trạng thái công việc đã tồn tại",
                    IsSucceeded = false
                };
            };

            var Kanban = new PrjKanban
            {
                TenCot = request.TenCot,
                MoTa = request.MoTa,
                DuAnNvChuyenMonId = request.DuAnNvChuyenMonId,
                TrangThaiCongViec = request.TrangThaiCongViec,
                YeuCauXacNhan = request.YeuCauXacNhan,
                ThuTu = request.ThuTu,
            };

            _context.PrjKanban.Add(Kanban);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = Kanban.Id,
                Message = "Thêm mới thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var Kanban = await _context.PrjKanban.FindAsync(id);
            if (Kanban == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            Kanban.IsDeleted = true;
            Kanban.DeletedAt = DateTime.UtcNow;

            _context.PrjKanban.Update(Kanban);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjKanban> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjKanban.FindAsync(id);
        }

        public async Task<IEnumerable<PrjKanban>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjKanban
                .Where(x => x.DuAnNvChuyenMonId == DuanId)
                .OrderBy(x => x.ThuTu)
                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, KanbanRequest request, TenantInfo info)
        {
            var Kanban = await _context.PrjKanban.FindAsync(id);
            if (Kanban == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            // Check if the Kanban already exists
            var isExist = await _context.PrjKanban.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                                      && x.ThuTu == request.ThuTu
                                                                                      && x.Id != id);

            if (isExist)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Thứ tự đã tồn tại",
                    IsSucceeded = false
                };
            };

            // check trangthai exists
            var isExistTrngTahi = await _context.PrjKanban.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                           && x.TrangThaiCongViec == request.TrangThaiCongViec
                                                                           && x.Id != id);
            if (isExistTrngTahi)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Trạng thái công việc đã tồn tại",
                    IsSucceeded = false
                };
            };
            Kanban.TenCot = request.TenCot;
            Kanban.MoTa = request.MoTa;
            Kanban.TrangThaiCongViec = request.TrangThaiCongViec;
            Kanban.YeuCauXacNhan = request.YeuCauXacNhan;
            Kanban.ThuTu = request.ThuTu;

            _context.PrjKanban.Update(Kanban);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Cập nhật thành công",
                IsSucceeded = true
            };

        }
    }
}

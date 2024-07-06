using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Service
{
    public class TagCommentService : ITagCommentService
    {
        private readonly PrjDbContext _context;

        public TagCommentService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAsync(TagCommentRequest request, TenantInfo info)
        {
            // Kiểm tra xem tag đã tồn tại chưa
            var isExist = await _context.PrjTagComment.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                  && x.MaTag == request.MaTag);
            if (isExist)
            {
                return new CudResponseDto
                {
                    Message = "Mã Tag đã tồn tại",
                    IsSucceeded = false
                };
            };

            var TagComment = new PrjTagComment
            {
                TenantId = info.TenantId,
                TenTag = request.TenTag,
                MaTag = request.MaTag,
                YeuCauXacThuc = request.YeuCauXacThuc,
                DuAnNvChuyenMonId = request.DuAnNvChuyenMonId,
                MauSac = request.MauSac
            };

            _context.PrjTagComment.Add(TagComment);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = TagComment.Id,
                Message = "Thêm mới thành công",
                IsSucceeded = true
            };
        }

        public async Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info)
        {
            var TagComment = await _context.PrjTagComment.FindAsync(id);
            if (TagComment == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            TagComment.IsDeleted = true;
            TagComment.DeletedAt = DateTime.UtcNow;

            _context.PrjTagComment.Update(TagComment);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = id,
                Message = "Xóa thành công",
                IsSucceeded = true
            };
        }

        public async Task<PrjTagComment> GetByIdAsync(Guid id, TenantInfo info)
        {
            return await _context.PrjTagComment.FindAsync(id);
        }

        public async Task<IEnumerable<PrjTagComment>> GetNoPaging(TenantInfo info, Guid DuanId)
        {
            return await _context.PrjTagComment
                .Where(x => x.TenantId == info.TenantId && x.DuAnNvChuyenMonId == DuanId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<CudResponseDto> UpdateAsync(Guid id, TagCommentRequest request, TenantInfo info)
        {
            var TagComment = await _context.PrjTagComment
                .Include(x => x.DuAnNvChuyenMon)
                .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == info.TenantId);

            if (TagComment == null)
            {
                return new CudResponseDto
                {
                    Id = id,
                    Message = "Không tìm thấy dữ liệu",
                    IsSucceeded = false
                };
            }

            // Kiểm tra xem tag đã tồn tại chưa
            var isExist = await _context.PrjTagComment.AnyAsync(x => x.DuAnNvChuyenMonId == request.DuAnNvChuyenMonId
                                                                             && x.MaTag == request.MaTag
                                                                             && x.Id != id);
            if (isExist)
            {
                return new CudResponseDto
                {
                    Message = "Mã Tag đã tồn tại",
                    IsSucceeded = false
                };
            }


            TagComment.TenTag = request.TenTag;
            TagComment.MaTag = request.MaTag;
            TagComment.YeuCauXacThuc = request.YeuCauXacThuc;
            TagComment.UpdatedAt = DateTime.UtcNow;
            TagComment.MauSac = request.MauSac;

            _context.PrjTagComment.Update(TagComment);
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

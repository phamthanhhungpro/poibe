using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.InfraModel.DataAccess;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Interface;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System.Text.RegularExpressions;

namespace Poi.Prj.Logic.Service
{
    public class CommentService : ICommentService
    {
        private readonly PrjDbContext _context;
        public CommentService(PrjDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> CreatePrjCommentAsync(CongViecCommentRequest request, TenantInfo info)
        {
            // #TAG @user content
            List<string> Tags = [];
            List<string> Usernames = [];
            string content = string.Empty;

            var tagRegex = new Regex(@"#\w+");
            var usernameRegex = new Regex(@"@\w+");

            var input = request.NoiDung;
            // Find all tags and usernames
            var tagMatches = tagRegex.Matches(input);
            var usernameMatches = usernameRegex.Matches(input);

            foreach (Match match in tagMatches)
            {
                Tags.Add(match.Value.Replace("#", ""));
            }

            foreach (Match match in usernameMatches)
            {
                Usernames.Add(match.Value);
            }

            // Remove tags and usernames from input to get the content
            content = tagRegex.Replace(input, "").Trim();
            //content = usernameRegex.Replace(contentWithoutTags, "").Trim();

            // Gửi thông báo cho người được tag


            var comment = new PrjComment
            {
                NoiDung = content,
                TenantId = info.TenantId,
                CongViecId = request.CongViecId,
                TagComments = _context.PrjTagComment.Where(x => Tags.Contains(x.MaTag) && x.DuAnNvChuyenMonId == request.DuAnId).ToList(),
                NguoiComment = _context.Users.FirstOrDefault(x => x.Id == info.UserId).UserName
            };

            _context.PrjComment.Add(comment);
            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                Id = comment.Id,
                Message = "Comment thành công",
                IsSucceeded = true
            };
        }

        public async Task<List<CongViecCommentDto>> GetCommentByIdCongViec(Guid congViecId)
        {
            var comment = await _context.PrjComment
                .Include(x => x.TagComments)
                .Where(x => x.CongViecId == congViecId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new CongViecCommentDto
                {
                    Id = x.Id,
                    NoiDung = x.NoiDung,
                    ThoiGian = x.CreatedAt.ToString("dd/MM/yyyy HH:mm"),
                    TagComments = x.TagComments,
                    NguoiComment = x.NguoiComment,
                }).ToListAsync();

            var listUserNames = comment.Select(x => x.NguoiComment).Distinct().ToList();
            var listUsers = _context.Users.Where(x => listUserNames.Contains(x.UserName)).ToList();

            foreach (var item in comment)
            {
                if(item.NguoiComment == null)
                {
                    continue;
                }
                item.NguoiCommentFullName = listUsers.FirstOrDefault(x => x.UserName == item.NguoiComment).FullName;
            };

            return comment;
        }
    }
}

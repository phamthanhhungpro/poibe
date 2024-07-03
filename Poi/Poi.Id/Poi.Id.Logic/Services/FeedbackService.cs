using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Interfaces;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Services
{
    public class FeedbackService : IAFeedbackService
    {
        private readonly IdDbContext _context;
        public FeedbackService(IdDbContext context)
        {
            _context = context;
        }

        public async Task<CudResponseDto> AddAFeedback(FeedbackRequest AFeedback, TenantInfo info)
        {
            var model = new AFeedback
            {
                UserId = info.UserId,
                AppName = AFeedback.AppName,
                Tittle = AFeedback.Title,
                MoTa = AFeedback.Description,
                TrangThai = "Tạo mới",
                Attachments = string.Join(",", AFeedback.AttachmentUrls)
            };

            _context.AFeedbacks.Add(model);

            await _context.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true
            };
        }

        public async Task<AFeedback> DeleteAFeedback(Guid id)
        {
            var model = _context.AFeedbacks.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                _context.AFeedbacks.Remove(model);
                await _context.SaveChangesAsync();
            }
            return model;
        }

        public async Task<AFeedback> GetAFeedbackById(Guid id)
        {
            return await _context.AFeedbacks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AFeedback>> GetAFeedbacks()
        {
            return await _context.AFeedbacks.ToListAsync();
        }

        public async Task<AFeedback> UpdateAFeedback(Guid id, AFeedback AFeedback)
        {
            var model = _context.AFeedbacks.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                model.UserId = AFeedback.UserId;
                model.AppName = AFeedback.AppName;
                model.Tittle = AFeedback.Tittle;
                model.MoTa = AFeedback.MoTa;
                model.TrangThai = AFeedback.TrangThai;
                model.Attachments = AFeedback.Attachments;

                await _context.SaveChangesAsync();
            }
            return model;
        }
    }
}

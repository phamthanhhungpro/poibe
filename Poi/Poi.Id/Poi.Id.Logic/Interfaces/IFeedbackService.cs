using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces
{
    public interface IAFeedbackService
    {
        Task<IEnumerable<AFeedback>> GetAFeedbacks();
        Task<AFeedback> GetAFeedbackById(Guid id);
        Task<CudResponseDto> AddAFeedback(FeedbackRequest AFeedback, TenantInfo info);
        Task<AFeedback> UpdateAFeedback(Guid id, AFeedback AFeedback);
        Task<AFeedback> DeleteAFeedback(Guid id);
    }
}

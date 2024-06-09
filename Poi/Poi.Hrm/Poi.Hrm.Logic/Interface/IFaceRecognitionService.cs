using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Interface
{
    public interface IFaceRecognitionService
    {
        Task TrainAsync(string datasetDir);

        Task<string> RecognizeAsync(byte[] imageBytes, TenantInfo tenantInfo);
    }
}

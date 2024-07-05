using Poi.Id.Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces
{
    public interface ITokenExpiredService
    {
        Task<CudResponseDto> AddTokenExpired(string token);
    }
}

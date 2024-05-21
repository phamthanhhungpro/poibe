using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Dtos
{
    public class CudResponseDto
    {
        public bool IsSucceeded { get; set; } = true;
        public Guid Id { get; set; }
    }
}

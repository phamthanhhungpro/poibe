using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests
{
    public class PagingRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

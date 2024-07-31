using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class GetCongViecGridRequest
    {
        public Guid DuAnId { get; set; }
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> AssignedUserIds { get; set; }
        public List<string> Status { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public GetCongViecGridRequest()
        {
            AssignedUserIds = [];
            Status = [];
            PageIndex = 0;
            PageSize = 50;
        }
    }
}

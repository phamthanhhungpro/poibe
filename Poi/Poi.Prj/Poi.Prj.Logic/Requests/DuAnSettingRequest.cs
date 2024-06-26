using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class DuAnSettingRequest
    {
        public List<KeyValuePair<string, object>> Settings { get; set; }
        public Guid DuAnId { get; set; }
    }
}

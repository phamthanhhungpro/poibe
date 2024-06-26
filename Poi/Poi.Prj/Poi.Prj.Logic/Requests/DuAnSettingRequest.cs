using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Prj.Logic.Requests
{
    public class DuAnSettingRequest
    {
        [Required]
        public Guid DuAnId { get; set; }
        [Required]
        public string Key { get; set; }
        [Required]
        public string Value { get; set; }

        public string MoTa { get; set; }
    }

    public class UpdateDuAnSettingRequest
    {
        [Required]
        public Guid DuAnId { get; set; }

        public Dictionary<string, string> Settings { get; set; }
    }
}

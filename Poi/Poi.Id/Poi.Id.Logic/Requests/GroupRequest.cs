using Poi.Shared.Model.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Requests
{
    public class GroupRequest
    {
        [Required(ErrorMessage = Error.requriedMessage)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = Error.requriedMessage)]
        public string Code { get; set; }
    }
}

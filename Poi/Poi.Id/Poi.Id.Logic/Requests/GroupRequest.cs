﻿using Poi.Shared.Model.Constants;
using System.ComponentModel.DataAnnotations;

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

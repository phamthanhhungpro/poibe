﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Dtos
{
    public class UserListInfoDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string GroupName { get; set; }
        public string TenantName { get; set; }
        public bool IsActive { get; set; }
    }
}
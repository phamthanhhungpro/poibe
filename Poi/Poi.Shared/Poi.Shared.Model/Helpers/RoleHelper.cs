using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Helpers
{
    public static class RoleHelper
    {
        public static bool IsAdmin(this string role)
        {
            return role == "ADMIN";
        }

        public static bool IsSSA(this string role)
        {
            return role == "SSA";
        }

        public static bool IsAppAdmin(this string role)
        {
            return role == "APPADMIN";
        }

        public static bool IsMember(this string role)
        {
            return role == "MEMBER";
        }

        public static bool IsOWNER(this string role)
        {
            return role == "OWNER";
        }

        public static bool IsHigherThanAdmin(this string role)
        {
            return role == "ADMIN" || role == "SSA" || role == "APPADMIN" || role == "OWNER";
        }
    }
}

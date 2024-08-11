using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Helpers
{
    public static class StringHelper
    {
        public static string Random6charString()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 6)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());
        }
    }
}

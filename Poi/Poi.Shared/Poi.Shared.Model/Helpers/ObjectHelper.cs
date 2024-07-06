using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Shared.Model.Helpers
{
    public static class ObjectHelper
    {
        //public static Dictionary<string, object> ToDictionary(this object source)
        //{
        //    if (source == null)
        //    {
        //        throw new ArgumentNullException(nameof(source));
        //    }

        //    var dictionary = new Dictionary<string, object>();
        //    var properties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    foreach (var property in properties)
        //    {
        //        if (property.CanRead)
        //        {
        //            dictionary[property.Name] = property.GetValue(source, null);
        //        }
        //    }

        //    return dictionary;
        //}c

        public static Dictionary<string, object> ToDictionary(this object source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source), "Source object cannot be null.");

            var dictionary = new Dictionary<string, object>();
            var properties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                if (property.CanRead)
                {
                    object value = property.GetValue(source, null);
                    dictionary.Add(property.Name, value);
                }
            }

            return dictionary;
        }
    }
}

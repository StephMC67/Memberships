using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memberships.Extensions
{
    public static class ReflectionExtensions   // static obligatoire / Extension
    {

        public static string GetPropertyValue<T>(this T item, string propertyName)  // premier param toujour "this T item" cause <T>
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }



    }
}
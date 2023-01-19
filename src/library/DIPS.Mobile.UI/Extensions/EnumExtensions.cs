using System;
using System.Collections.Generic;
using System.Linq;

namespace DIPS.Mobile.UI.Extensions
{
    public static class Enum
    {
        public static IEnumerable<T> ToList<T>()
        {
            return System.Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
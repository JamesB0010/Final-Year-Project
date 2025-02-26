using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class EnumUtility 
    {
        public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();
    }
}

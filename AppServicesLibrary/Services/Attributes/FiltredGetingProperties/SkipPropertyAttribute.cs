using System;
using System.Linq;
using System.Reflection;

namespace AppServicesLibrary.Services.Attributes.FiltredGetingProperties
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SkipPropertyAttribute : Attribute { }

    public static class TypeExtension
    {
        public static PropertyInfo[] GetFilteredProperties(this Type type)
        {
            return type.GetProperties()
              .Where(pi => !Attribute.IsDefined(pi, typeof(SkipPropertyAttribute)))
              .ToArray();
        }
    }
}

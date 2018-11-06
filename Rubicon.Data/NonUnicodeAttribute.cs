using System;

namespace Rubicon.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    sealed class NonUnicodeAttribute : Attribute
    {
        public NonUnicodeAttribute()
        {
        }
    }
}

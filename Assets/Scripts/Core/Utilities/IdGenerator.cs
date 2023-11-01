using System;

namespace Core.Utilities
{
    public static class IdGenerator
    {
        public static string Get()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

using System;

namespace Socket_Debugger.Utils
{
    public static class IdHelper
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
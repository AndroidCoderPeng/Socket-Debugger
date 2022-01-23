using System;

namespace Socket_Debugger.Utils
{
    public static class IdHelper
    {
        public static string Generat()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}

using System;

namespace SlamBot
{
    internal static class Helpers
    {
        internal static Exception UnwindException(this Exception e)
        {
            while(e.InnerException != null)
            {
                e = e.InnerException;
            }

            return e;
        }
    }
}
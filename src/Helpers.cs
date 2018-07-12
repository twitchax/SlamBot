
using System;
using System.Text;

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

        internal static string UnwindExceptionAsString(this Exception e)
        {
            var sb = new StringBuilder();

            sb.Append($"\n{e.Message}\n{e.StackTrace}\n");

            while(e.InnerException != null)
            {
                e = e.InnerException;
                sb.Append($"\n{e.Message}\n{e.StackTrace}\n");
            }

            return sb.ToString();
        }
    }
}

using System;
using System.IO;
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

        internal static string Resolve(string environmentVariable, string secretFile = null, string defaultValue = null)
        {
            string result = Environment.GetEnvironmentVariable(environmentVariable);

            if(string.IsNullOrWhiteSpace(result))
                result = ReadAllTextProtected(secretFile);

            if(string.IsNullOrWhiteSpace(result))
                result = defaultValue;

            return result;
        }

        internal static string ReadAllTextProtected(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
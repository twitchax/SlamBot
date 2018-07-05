
using System;
using System.IO;

namespace SlamBot
{
    internal static class Secrets
    {
        internal static string NewsApiKey => Resolve("NEWSAPI_KEY", "/etc/secrets/newsapi_key");
        internal static string TwitterConsumerKey => Resolve("TWITTER_CONSUMER_KEY", "/etc/secrets/twitter_consumer_key");
        internal static string TwitterConsumerSecret => Resolve("TWITTER_CONSUMER_SECRET", "/etc/secrets/twitter_consumer_secret");
        internal static string TwitterUserToken => Resolve("TWITTER_USER_TOKEN", "/etc/secrets/twitter_user_token");
        internal static string TwitterUserSecret => Resolve("TWITTER_USER_SECRET", "/etc/secrets/twitter_user_secret");


        private static string Resolve(string environmentVariable, string secretFile = null, string defaultValue = null)
        {
            string result = Environment.GetEnvironmentVariable(environmentVariable);

            if(string.IsNullOrWhiteSpace(result))
                result = ReadAllTextProtected(secretFile);

            if(string.IsNullOrWhiteSpace(result))
                result = defaultValue;

            return result;
        }

        private static string ReadAllTextProtected(string fileName)
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
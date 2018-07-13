using System;

namespace SlamBot
{
    internal static class Secrets
    {
        internal static string NewsApiKey => Helpers.Resolve("NEWSAPI_KEY", "/etc/secrets/newsapi_key");
        internal static string TwitterConsumerKey => Helpers.Resolve("TWITTER_CONSUMER_KEY", "/etc/secrets/twitter_consumer_key");
        internal static string TwitterConsumerSecret => Helpers.Resolve("TWITTER_CONSUMER_SECRET", "/etc/secrets/twitter_consumer_secret");
        internal static string TwitterUserToken => Helpers.Resolve("TWITTER_USER_TOKEN", "/etc/secrets/twitter_user_token");
        internal static string TwitterUserSecret => Helpers.Resolve("TWITTER_USER_SECRET", "/etc/secrets/twitter_user_secret");
    }
}
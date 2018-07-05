using Tweetinvi;

namespace SlamBot
{
    internal static class Twitter
    {
        static Twitter()
        {
            Auth.SetUserCredentials(Secrets.TwitterConsumerKey, Secrets.TwitterConsumerSecret, Secrets.TwitterUserToken, Secrets.TwitterUserSecret);
        }

        internal static void Tweet(string message)
        {
            Tweetinvi.Tweet.PublishTweet(message);
        }
    }
}
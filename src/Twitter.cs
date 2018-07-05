using Tweetinvi;
using Tweetinvi.Models;

namespace SlamBot
{
    internal static class Twitter
    {
        static Twitter()
        {
            Auth.SetUserCredentials(Secrets.TwitterConsumerKey, Secrets.TwitterConsumerSecret, Secrets.TwitterUserToken, Secrets.TwitterUserSecret);
        }

        internal static ITweet Tweet(string message)
        {
            return Tweetinvi.Tweet.PublishTweet(message);
        }
    }
}
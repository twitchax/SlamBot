using System;
using System.Collections.Generic;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

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

        internal static IEnumerable<ITweet> GetRecentTweets()
        {
            long userId = 1014383895834316800;
            return Timeline.GetUserTimeline(userId, 200);
        }
    }
}
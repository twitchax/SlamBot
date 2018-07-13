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

        internal static ITweet PublishTweet(string message)
        {
            var tweet = Tweet.PublishTweet(message);

            if(tweet == null)
                throw new Exception(ExceptionHandler.GetLastException().TwitterDescription);

            return tweet;
        }

        internal static IEnumerable<ITweet> GetRecentTweets()
        {
            long userId = 1014383895834316800;
            return Timeline.GetUserTimeline(userId, 200);
        }
    }
}
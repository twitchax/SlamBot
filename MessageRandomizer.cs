
using System;
using System.Collections.Generic;

namespace SlamBot
{
    internal static class MessageRandomizer
    {
        private static Random _rnd = new Random();

        private static List<string> _messages = new List<string>
        {
            "It's a happening...a SLAM!",
            "That's right: it's another SLAM!",
            "Did you hear about the SLAM today?",
            "I bet you can't guess what just happened.  Yeah, ok, you were right.  SLAM!",
            "No, but really, this SLAM had to hurt.",
            "SLAM!",
            "Starting to feel like there are too many SLAMs...",
            "Do you think they had SLAMs in the 18th century?",
            "...SLAMSLAMSLAM...",
            "Houston, the SLAM has landed!"
        };

        internal static string GetMessage()
        {
            return _messages[_rnd.Next(0, _messages.Count)];
        }
    }
}
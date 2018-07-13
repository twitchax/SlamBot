using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SlamBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var query = "slams";
            var refreshRateInMinutes = Settings.RefreshRateInMinutes;

            var urls = new HashSet<string>();
            
            while(true)
            {
                try
                {
                    var windowInMinutes = Settings.WindowInMinutes;
                    
                    Log($"Getting news within last {windowInMinutes} minutes ...");
                    var slams = await GetLatestNews(query, windowInMinutes);
                    
                    foreach(var s in slams.Reverse())
                    {
                        // Temporary for CoreRT debugging.
                        Log($"   IsNull(s) == {s == null}");
                        Log($"   IsNull(urls) == {urls == null}");
                        Log($"   IsNull(s.Url) == {s.Url == null}");

                        // Check inside loop in case two articles came back in the same request.
                        if(urls.Contains(s.Url))
                            continue;
                        
                        var tweet = Twitter.Tweet($"{MessageRandomizer.GetMessage()}\n\nPowered by @NewsAPIorg.\n\n{s.Url}");
                        Log($"   IsNull(tweet) == {tweet == null}");
                        urls.Add(s.Url);
                        Log($"   IsNull(tweet.Url) == {tweet.Url == null}");
                        Log($"   TWEET: {s.Title} => {tweet.Url}.");
                    }
                }
                catch (Exception e)
                {
                    Log($"   FAILURE:\n{e.UnwindExceptionAsString()}\n");
                }
                finally
                {
                    Log($"Waiting {refreshRateInMinutes} minutes ...");
                    await Task.Delay(TimeSpan.FromMinutes(refreshRateInMinutes));
                }
            }
        }

        static async Task<IEnumerable<Article>> GetLatestNews(string query, int windowInMinutes)
        {
            var now = DateTime.Now.ToUniversalTime();

            var response = await WebRequest.Create($"https://newsapi.org/v2/everything?q={query}&sortBy=publishedAt&apiKey={Secrets.NewsApiKey}").GetResponseAsync();
            var jsonString = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();

            var newsResponse = NewsResponse.Parse(jsonString);
            
            return newsResponse.Articles
                .Where(
                    a => a.Title.ToLower().Contains(query) && 
                    now - a.PublishedAt < TimeSpan.FromMinutes(windowInMinutes)
                );
        }

        static void Log(string text)
        {
            var time = DateTime.Now.ToUniversalTime().ToString();

            Console.WriteLine($"[{time}] {text}");
            File.AppendAllText("log.txt", $"[{time}] {text}\n");
        }
    }
}
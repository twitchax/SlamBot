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
            var refreshRateInMinutes = 5;
            
            while(true)
            {
                try
                {
                    Log($"Getting news within last {refreshRateInMinutes} minutes ...");
                    var slams = await GetLatestNews(query, refreshRateInMinutes);

                    foreach(var a in slams)
                    {
                        Log($"   TWEET: {a.Title}");
                        Twitter.Tweet($"{MessageRandomizer.GetMessage()}\n\n{a.Url}");
                    }
                }
                catch (Exception e)
                {
                    Log($"   FAILURE.\n\n{e.Message}\n\n{e.StackTrace}\n");
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
                    a => a.Title.Contains(query) && 
                    now - a.PublishedAt > TimeSpan.FromMinutes(0) && 
                    now - a.PublishedAt < TimeSpan.FromMinutes(windowInMinutes)
                );
        }

        static void Log(string text)
        {
            var time = DateTime.Now.ToString();

            Console.WriteLine($"[{time}] {text}");
            File.AppendAllText("log.txt", $"[{time}] {text}\n");
        }
    }
}
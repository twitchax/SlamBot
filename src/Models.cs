using System;
using System.Collections.Generic;
using System.Linq;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace SlamBot
{
    public partial class NewsResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalResults")]
        public long TotalResults { get; set; }

        [JsonProperty("articles")]
        public List<Article> Articles { get; set; }

        internal static NewsResponse Parse(string json)
        {
            var o = JObject.Parse(json);
            
            return new NewsResponse
            {
                Status = o.Value<string>("status"),
                TotalResults = o.Value<long>("totalResults"),
                Articles = o.Value<JArray>("articles").AsEnumerable().Select(a => new Article
                {
                    Source = new Source
                    {
                        Id = a.SelectToken("source.id").Value<string>(),
                        Name = a.SelectToken("source.name").Value<string>()
                    },
                    Author = a.Value<string>("author"),
                    Title = a.Value<string>("title"),
                    Description = a.Value<string>("description"),
                    Url = a.Value<string>("url"),
                    UrlToImage = a.Value<string>("urlToImage"),
                    PublishedAt = a.Value<DateTime>("publishedAt")
                }).ToList()
            };
        }
    }

    public partial class Article
    {
        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("urlToImage")]
        public string UrlToImage { get; set; }

        [JsonProperty("publishedAt")]
        public DateTime PublishedAt { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using FinalsProjectAPI.Data;
using FinalsProjectAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Net.Http.Headers;

namespace FinalsProjectAPI.News
{
    public class NewsApiScraper : INewsScraper
    {
        private readonly HttpClient _httpClient;

        public NewsApiScraper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MyUserAgent");
        }
        public async Task<ActionResult<List<NewsArticle>>> GetNews()
        {
            List<NewsArticle> newsArticles = new List<NewsArticle>();

            var url = "https://newsapi.org/v2/everything?q=Apple&from=2023-04-10&sortBy=popularity&apiKey=" + Config.NEWS_API_KEY;

            var response = _httpClient.GetAsync(url);

            if (response != null)
            {
                var result = await response.Result.Content.ReadAsStringAsync();
                JsonDocument doc = JsonDocument.Parse(result);
                JsonElement root = doc.RootElement;
                JsonElement articles = root.GetProperty("articles");

                var id = 1;
                foreach (JsonElement article in articles.EnumerateArray())
                {

                    string title = article.GetProperty("title").GetString();
                    string description = article.GetProperty("description").GetString();

                    newsArticles.Add(new NewsArticle
                    {
                        Id = id,
                        Title = title,
                        Description = description
                    });

                    id = id + 1;
                }
            }

            return newsArticles;
        }
    }
}

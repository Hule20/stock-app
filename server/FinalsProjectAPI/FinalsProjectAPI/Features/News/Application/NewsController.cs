using FinalsProjectAPI.Features.News.Domain;
using FinalsProjectAPI.Features.News.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.Features.News.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public NewsController() { }


        [HttpGet]
        public async Task<ActionResult<List<NewsArticle>>> getNews()
        {
            HttpClient httpClient = new();

            var ns = new NewsApiScraper(httpClient);
            var newsList = await ns.GetNews();

            if (newsList == null)
            {
                return NotFound();
            }

            return Ok(newsList.Value);
        }
    }
}

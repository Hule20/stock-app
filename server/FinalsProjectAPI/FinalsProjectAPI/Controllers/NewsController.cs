using FinalsProjectAPI.News;
using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        public NewsController() { }


        [HttpGet]
        public async Task<ActionResult<List<NewsArticle>>> getNews()
        {
            HttpClient httpClient = new HttpClient();

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

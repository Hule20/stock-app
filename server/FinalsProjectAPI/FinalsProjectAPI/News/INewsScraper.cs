using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.News
{
    public interface INewsScraper
    {
        Task<ActionResult<List<NewsArticle>>> GetNews();
    }
}

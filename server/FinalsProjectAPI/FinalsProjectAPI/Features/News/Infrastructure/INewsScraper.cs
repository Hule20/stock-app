using FinalsProjectAPI.Features.News.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FinalsProjectAPI.Features.News.Infrastructure
{
    public interface INewsScraper
    {
        Task<ActionResult<List<NewsArticle>>> GetNews();
    }
}

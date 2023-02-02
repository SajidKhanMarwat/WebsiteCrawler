using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using WebsiteCrawler.Models;

namespace WebsiteCrawler.Controllers
{
    public class CrawlerController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(URL inPut)
        {
            try
            {
                if (inPut.Url != "" && inPut.Url.StartsWith("http"))
                {
                    ViewBag.URL = inPut.Url;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}

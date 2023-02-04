using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using WebsiteCrawler.Models;
using System.Linq;

namespace WebsiteCrawler.Controllers
{
    public class CrawlerController : Controller
    {

        List<string> _AllUrls = new List<string>();

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
                // Initial or Main url checker

                if (!inPut.Url.StartsWith("http"))
                {
                    inPut.Url = "https://" + inPut.Url;
                }
                HtmlWeb web = new HtmlWeb();
                var htmlCode = web.Load(inPut.Url);
                var href = htmlCode.DocumentNode.SelectNodes("//a[@href]");

                foreach (var node in href)
                {
                    HtmlAttribute attribute = node.Attributes["href"];
                    if (attribute.Value.Contains("a"))
                    {
                        _AllUrls.Add(attribute.Value);
                    }
                }


                // Remove Duplicate & Unknown URLs
                for (int index = 0; index <= _AllUrls.Count - 1; index++)
                {
                    for (int matcher = 0; matcher < _AllUrls.Count; matcher++)
                    {
                        if (_AllUrls[index] == _AllUrls[matcher])
                        {
                            _AllUrls.RemoveAt(index);
                            //_AllUrls.Add(_AllUrls[index]);
                        }
                        else if (!_AllUrls[index].StartsWith(inPut.Url))
                        {
                            _AllUrls.RemoveAt(index);
                        }
                    }
                }


                // Deep checker (Crawl more & more urls)

                for (int i = 0; i < _AllUrls.Count - 1; i++)
                {
                    HtmlWeb htmlWeb = new HtmlWeb();
                    var htmlLoad = htmlWeb.Load(_AllUrls[i]);
                    var hrefSeperated = htmlLoad.DocumentNode.SelectNodes("//a[@href]");

                    foreach (var listItem in hrefSeperated)
                    {
                        HtmlAttribute htmlAttribute = listItem.Attributes["href"];

                        if (htmlAttribute.Value.Contains("a"))
                        {
                            _AllUrls.Add(htmlAttribute.Value);

                            // Remove Unknown URLs
                            for (int index = 0; index < _AllUrls.Count - 1; index++)
                            {
                                if (!_AllUrls[index].StartsWith(inPut.Url))
                                {
                                    _AllUrls.RemoveAt(index);
                                }
                            }
                        }
                    }
                }

                for (int index = 0; index <= _AllUrls.Count - 1; index++)
                {
                    for (int matcher = 0; matcher < _AllUrls.Count; matcher++)
                    {
                        if (_AllUrls[index] == _AllUrls[matcher])
                        {
                            _AllUrls.RemoveAt(index);
                            //_AllUrls.Add(_AllUrls[index]);
                        }
                    }
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

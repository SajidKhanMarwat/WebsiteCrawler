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
                if (!inPut.Url.StartsWith("http"))
                {
                    inPut.Url = "https://" + inPut.Url;
                }
                HtmlWeb web = new HtmlWeb();
                var htmlCode = web.Load(inPut.Url);
                var href = htmlCode.DocumentNode.SelectNodes("//a[@href]");

                foreach(var node in href)
                {
                    HtmlAttribute attribute = node.Attributes["href"];
                    if (attribute.Value.Contains("a"))
                    {
                        _AllUrls.Add(attribute.Value);
                    }
                    
                }

                for (int index = 0; index <= _AllUrls.Count; index++)
                {
                    for (int reverseIndex = 0; reverseIndex <= _AllUrls.Count; reverseIndex++)
                    {
                        if (_AllUrls[index] == _AllUrls[reverseIndex])
                        {
                            _AllUrls.RemoveAt(index);
                            //_AllUrls.Add(_AllUrls[index]);
                        }
                    }
                                        
                }

                for (int i = 0; i < _AllUrls.Count; i++)
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

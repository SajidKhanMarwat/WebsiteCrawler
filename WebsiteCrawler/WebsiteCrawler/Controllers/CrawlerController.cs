using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using WebsiteCrawler.Models;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;

namespace WebsiteCrawler.Controllers
{
    public class CrawlerController : Controller
    {
        HashSet<string> _InitialUrls = new HashSet<string>();
        //List<string> _AllUrls = new List<string>();
        List<StatusURL> _UrlsWithStatus = new List<StatusURL>();

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
                        _InitialUrls.Add(attribute.Value);
                    }
                }

                // Deep checker (Crawl more & more urls)

                foreach (var url in _InitialUrls)
                {
                    if (url.StartsWith(inPut.Url))
                    {
                        HtmlWeb htmlWeb = new HtmlWeb();
                        var htmlLoad = htmlWeb.Load(url); // Loading the HTML of the particular WebPages

                        var hrefSeperated = htmlLoad.DocumentNode.SelectNodes("//a[@href]");

                        //for (int i = 0; i <= hrefSeperated.Count - 1; i++)
                        //{
                        //    HtmlAttribute htmlAttribute = hrefSeperated[i].Attributes["href"];
                        //    if (htmlAttribute.Value.Contains("a"))
                        //    {
                        //        _InitialUrls.Add(htmlAttribute.Value);
                        //    }
                        //}

                        foreach (var listItem in hrefSeperated)
                        {
                            HtmlAttribute htmlAttribute = listItem.Attributes["href"];

                            if (htmlAttribute.Value.Contains("a"))
                            {
                                _InitialUrls.Add(htmlAttribute.Value);
                            }
                        }


                        //// Getting the Status of the specified URL
                        var request = HttpWebRequest.Create(url);
                        var response = request.GetResponse() as HttpWebResponse;

                        StatusURL statusURL = new StatusURL(); // Creating StatusURL Object for storing URL & Status in the List
                        statusURL.Url = url;
                        statusURL.Status = (Int32)response.StatusCode;
                        _UrlsWithStatus.Add(statusURL);

                    }
                    else
                    {
                        continue;
                    }
                }

                return View(_InitialUrls);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}

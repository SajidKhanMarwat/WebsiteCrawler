using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using WebsiteCrawler.Models;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;

namespace WebsiteCrawler.Controllers
{
    public class TestController : Controller
    {
        List<string> _InitialUrls = new List<string>();
        //List<string> _UniqueURLs = new List<string>();
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
                
                for(int i = 0; i < _InitialUrls.Count; i++)
                {
                    if (_InitialUrls[i].StartsWith(inPut.Url))
                    {
                        HtmlWeb htmlWeb = new HtmlWeb();
                        var htmlLoad = htmlWeb.Load(_InitialUrls[i]); // Loading the HTML of the particular WebPages

                        var hrefSeperated = htmlLoad.DocumentNode.SelectNodes("//a[@href]");

                        foreach (var listItem in hrefSeperated)
                        {
                            HtmlAttribute htmlAttribute = listItem.Attributes["href"];

                            if (htmlAttribute.Value.Contains("a") && htmlAttribute.Value.StartsWith(inPut.Url))
                            {
                                _InitialUrls.Add(htmlAttribute.Value);
                            }
                        }

                        // Getting the Status of the specified URL
                        var request = HttpWebRequest.Create(_InitialUrls[i]);
                        var response = request.GetResponse() as HttpWebResponse;

                        StatusURL statusURL = new StatusURL(); // Creating StatusURL Object for storing URL & Status in the List
                        statusURL.Url = _InitialUrls[i];
                        statusURL.Status = (Int32)response.StatusCode;
                        _UrlsWithStatus.Add(statusURL);
                    }
                    else
                    {
                        continue;
                    }

                    if (!_InitialUrls[i].StartsWith(inPut.Url))
                    {
                        if (_InitialUrls[i].StartsWith("/"))
                        {
                            _InitialUrls[i] = inPut.Url + _InitialUrls[i].Substring(1, _InitialUrls[i].Length);
                            _InitialUrls.Add(_InitialUrls[i]);
                            continue;
                        }
                        //else
                        //{
                        //    inPut.Url.Substring(0, inPut.Url.Length - 1);
                        //}
                    }

                    _InitialUrls = _InitialUrls.Distinct().ToList();
                    
                }

                return View(_UrlsWithStatus);
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using WebsiteCrawler.Models;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;
using System;
using Microsoft.Build.Framework;
using BusinessLogics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authentication;

namespace WebsiteCrawler.Controllers
{
    [Authorize]
    public class CrawlerController : Controller
    {
        List<string> _InitialUrls = new List<string>();
        //List<string> _UniqueURLs = new List<string>();
        //List<string> _AllUrls = new List<string>();
        HashSet<StatusURL> _UrlsWithStatus = new HashSet<StatusURL>();

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

                for (int i = 0; i < _InitialUrls.Count; i++)
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
                    _InitialUrls = _InitialUrls.Distinct().ToList();

                    if (!_InitialUrls[i].StartsWith(inPut.Url))
                    {
                        string completeUrl = inPut.Url.Substring(0, inPut.Url.Length - 1) + _InitialUrls[i];
                        _InitialUrls.Add(completeUrl);
                        continue;

                        //else
                        //{
                        //    inPut.Url.Substring(0, inPut.Url.Length - 1);
                        //}
                    }
                }
                return View(_UrlsWithStatus);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (404) Not Found." || ex.Message == "null")
                {
                    return View(_UrlsWithStatus);
                }
            }
            return View(_UrlsWithStatus);
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }

    }
}

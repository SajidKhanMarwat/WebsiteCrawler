﻿using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using WebsiteCrawler.Models;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Components.Forms;
using System.Security.Policy;
using System;

namespace WebsiteCrawler.Controllers
{
    public class CrawlerController : Controller
    {
        HashSet<string> _InitialUrls = new HashSet<string>();
        HashSet<string> _NewURLs = new HashSet<string>();
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


                //// Removing the Trailing Slash from the list of _InitialUrls
                //string str = inPut.Url;
                //string seperatedURL;
                //if (inPut.Url.Last() == '/')
                //{
                //    str = str.Substring(0, str.Length - 1);
                //}

                // Deep checker (Crawl more & more urls)
                foreach (var url in _InitialUrls)
                {
                    if (url.StartsWith(inPut.Url))
                    {
                        HtmlWeb htmlWeb = new HtmlWeb();
                        var htmlLoad = htmlWeb.Load(url); // Loading the HTML of the particular WebPages

                        var hrefSeperated = htmlLoad.DocumentNode.SelectNodes("//a[@href]");

                        foreach (var listItem in hrefSeperated)
                        {
                            HtmlAttribute htmlAttribute = listItem.Attributes["href"];

                            if (htmlAttribute.Value.Contains("a") && htmlAttribute.Value.StartsWith(inPut.Url))
                            {
                                _NewURLs.Add(htmlAttribute.Value);
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

                    if (!url.StartsWith(inPut.Url))
                    {
                        if (url.StartsWith("/"))
                        {
                            //url.Substring(1, url.Length);
                            //url = null;
                            //url = inPut.Url + url;

                        }
                        else
                        {
                            inPut.Url.Substring(0, inPut.Url.Length - 1);
                        }
                    }
                    _InitialUrls.Add(_NewURLs.FirstOrDefault());
                    continue;
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

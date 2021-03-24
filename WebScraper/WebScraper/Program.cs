using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            
            // Stuff
            var bookIdRegex = new Regex(@"\d+");
            var url = "https://allitbooks.net";
            var web = new HtmlWeb().Load(url);

            
            // Get all categories.
            var categories = web.DocumentNode
                .SelectNodes("//ul/a")
                .Select(node => node.Attributes["href"].Value);

            
            // Create full url.
            var categoriesUrls = categories.Select(category => string.Concat(url, category));
            
            
            // Create  HtmlWeb object for each url.
            var categoriesObjects = categoriesUrls.Select(new HtmlWeb().Load);

            
            // 115 sec
            // var bookIds = categoriesObjects
            //     .Select(obj => obj.DocumentNode.SelectNodes("//div/a[contains(@title, 'Download')]"))
            //     .Select(nodeCollection => nodeCollection.Select(n => n.Attributes["href"].Value)
            //         .Where(u => u.StartsWith(".."))
            //         .Select(u => bookIdRegex.Match(u).Groups[0].Value))
            //     .Aggregate((current, ids) => current.Concat(ids));

            
            // Old version. Find elements by XPath and get id from href.
            // 117 sec
            var bookIds = Enumerable.Empty<string>();

            foreach (var obj in categoriesObjects)
            {
                var nodeCollection = obj.DocumentNode.SelectNodes("//div/a[contains(@title, 'Download')]");
                
                var ids = nodeCollection.Select(n => n.Attributes["href"].Value)
                    .Where(u => u.StartsWith(".."))
                    .Select(u => bookIdRegex.Match(u).Groups[0].Value);
                
                bookIds = bookIds.Concat(ids);
            }
            
            // Download books.
            // var webClient = new WebClient();
            // bookIds
            //     .AsParallel()
            //     .ForAll(id => webClient.DownloadFile($"https://allitbooks.net/download-file-{id}.html", $"C:/{id}.pdf"));
            

            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds / 1000} sec.");
        }
    }
}
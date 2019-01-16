using MVCLogin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace MVCLogin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
           
        }
        public ActionResult ShowResults()
        {
            string searchQuery = Request["search"];
            string cx = "003494863199344963141:95jxtrcqqay";
            string apiKey = "AIzaSyBowxJWfGnzBERwDAm-YvuARTQrLk2bHXQ";
            var request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?key=" + apiKey + "&cx=" + cx + "&q=" + searchQuery);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseString = reader.ReadToEnd();
            dynamic jasonData = JsonConvert.DeserializeObject(responseString);
            jasonData.Works = true;

            var results = new List<ResultR>();
            if (results != null)
            {
                foreach (var item in jasonData.items)

                {
                    results.Add(new ResultR
                    {
                        Title = item.title,
                        Link = item.link,
                        Snippet = item.snippet,

                    });
                }
            }

            return View(results.ToList());

        }
    }
}
        

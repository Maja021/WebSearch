using MVCLogin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        private static List<User> users = new List<User>();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Autherize(MVCLogin.Models.User userModel)
        {
            using (LoginDataBaseEntities db = SingletonDB.Instance.GetDBConnection())
            {               
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                //if is wrong user name or password
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong user name or password.";
                    return View("Index", userModel);
                }
                //if is right, save that id
                else
                {

                    List<User> data = new List<User>();
                     data.Add(new User()
                {
                       UserName = userDetails.UserName,
                       Password = userDetails.Password
                });

                    string json = JsonConvert.SerializeObject(data.ToArray());

                    string path = @"E:\MyTestJson.txt";
                    if (!System.IO.File.Exists(path))
                    {
                        using (StreamWriter file = System.IO.File.CreateText(path))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            //serialize object directly into file stream
                            serializer.Serialize(file, data);
                        }
                    }
                    Session["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;             
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        //reloading Session after 30 minutes(from Web.config file) - Session timeout - redirected to Index page
        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
    }
}
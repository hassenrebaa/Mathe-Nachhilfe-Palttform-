using Limilabs.Client.Authentication.Google;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace plattform.Controllers
{
    public class LoginController : Controller
    {
       

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        
         public ActionResult loginn (user_tbl usermodel)
        {
            
            using (HassenDataBaseEntities7 db = new HassenDataBaseEntities7())
            {
                var userDetails = db.user_tbl.Where(x => x.email == usermodel.email).FirstOrDefault();
                if (userDetails != null)
                {
                    if(string.Compare(Crypto.Hash(usermodel.password),userDetails.password)== 0)
                    {
                        int timeout = usermodel.RemembreMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(usermodel.email, usermodel.RemembreMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        return View("~/Views/User/afterLogin.cshtml", usermodel);


                    }
                   


                }
                else
                {
                    usermodel.LoginErrorMessage = "Falsch Username oder Password";

                    return View("Index", usermodel);

                }

                return View("Index", usermodel);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

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
         public ActionResult loginn(users_tbl usermodel)
        {
            using (HassenDataBaseEntities7 db = new HassenDataBaseEntities7())
            {
                var userDetails = db.user_tbl.Where(x => x.email == usermodel.email && x.password == usermodel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    
                    usermodel.LoginErrorMessage = "Falsch Username oder Password";

                    return View("Index", usermodel);
                }
                else
                    return View("~/Views/User/afterLogin.cshtml", usermodel);
            }
        }
    }
}
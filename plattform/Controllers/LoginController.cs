using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Autherize(users_tbl usermodel)
        {
            using(HassenDataBaseEntities db= new HassenDataBaseEntities())
            {
                var userDetails = db.users_tbl.Where(x => x.email == usermodel.email && x.passeword == usermodel.passeword).FirstOrDefault();
                if (userDetails == null)
                {

                    return View("Index", usermodel);
                }
                else
                    return View("~/Views/User/UserList.cshtml" ,usermodel);
            }
        }
    }
}
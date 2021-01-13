using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plattform.Controllers
{
    public class UsertestEncryptController : Controller
    {
        // GET: UsertestEncrypt
        HassenDataBaseEntities5 _db;
        public UsertestEncryptController()
        {
            _db = new HassenDataBaseEntities5();
        }
        public ActionResult List()
        {
            var test = _db.users_tbl.ToList();
            return View(test);
        }
        // GET: User
        public ActionResult Index()
        {
            var test = _db.users_tbl.ToList();
            return View(test);
        }

#pragma warning disable format
        public ActionResult Createent(int id=0)
#pragma warning restore format
        {
            users_tbl usermodel = new users_tbl();
            return View(usermodel);
        }
        // Login  funktion
        [HttpPost]
        public ActionResult Createent(users_tbl usermodel)
        {
            using (HassenDataBaseEntities5 model = new HassenDataBaseEntities5())
            {
                model.users_tbl.Add(usermodel);
                model.SaveChanges();


            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("Createent", new users_tbl());
        }
        //GET 
        public ActionResult Update()
        {
            users_tbl usermodel = new users_tbl();
            return View(usermodel);

        }
        [HttpPost]
        public ActionResult update(int id, users_tbl usermodel)
        {
            using (HassenDataBaseEntities5 entities = new HassenDataBaseEntities5())
            {
                var neumodel = entities.users_tbl.Where(x => x.id == id).FirstOrDefault();
                neumodel.vorname = usermodel.vorname;
                neumodel.nachname = usermodel.nachname;
                neumodel.id = usermodel.id;
                neumodel.mobile = usermodel.mobile;
                neumodel.email = usermodel.email;
                neumodel.adresse = usermodel.adresse;
                neumodel.password = usermodel.password;
                entities.SaveChanges();
                return View("Update", new users_tbl());
            }
        }
    }
}
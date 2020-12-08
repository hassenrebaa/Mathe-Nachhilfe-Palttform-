using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plattform.Controllers
{
    public class UserController : Controller
    {
        HassenDataBaseEntities1 _db;
        public UserController()
        {
            _db = new HassenDataBaseEntities1();
        }
        public ActionResult List()
        {
            var test = _db.users_tbl.ToList();
            return View(test);
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddOrEdit(int id=0)
        {
            users_tbl usermodel = new users_tbl();
            return View(usermodel);
        }
        // Login 
        [HttpPost]
        public ActionResult AddOrEdit (users_tbl usermodel)
        {
            using(HassenDataBaseEntities1 model =new HassenDataBaseEntities1())
            {
                model.users_tbl.Add(usermodel);
                model.SaveChanges();
              

            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddOrEdit", new users_tbl());
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
            using (HassenDataBaseEntities1 entities=new HassenDataBaseEntities1())
            {
                var neumodel = entities.users_tbl.Where(x => x.id == id).FirstOrDefault();
                neumodel.vorname = usermodel.vorname;
                neumodel.nachname = usermodel.nachname;
                neumodel.id = usermodel.id;
                neumodel.mobile = usermodel.mobile;
                neumodel.email = usermodel.email;
                neumodel.adresse = usermodel.adresse;
                neumodel.passeword = usermodel.passeword;
                entities.SaveChanges();
                return View("Update", new users_tbl());
            }
        }
    }
}
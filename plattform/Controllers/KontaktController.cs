using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plattform.Controllers
{
    public class KontaktController : Controller
    {
        HassenDataBaseEntitieskontakt  _db;
        public KontaktController()
        {
            _db = new HassenDataBaseEntitieskontakt();
        }
        // GET: Kontakt
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
#pragma warning restore format
        {
            Kontakt_tbl kontakt = new Kontakt_tbl();
            return View();
        }
        // Login  funktion
        [HttpPost]
        public ActionResult Create (Kontakt_tbl kontakt)
        {
            using (HassenDataBaseEntitieskontakt model = new HassenDataBaseEntitieskontakt())
            {
                model.Kontakt_tbl.Add(kontakt);
                model.SaveChanges();


            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("Home/Index");
        }
    }
}
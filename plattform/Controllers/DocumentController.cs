using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plattform.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        DocEntities _db; 
        public DocumentController()
        {
            _db = new DocEntities(); 
        }
        public ActionResult Index()
        {
            var list = _db.Dokuments_tbl.ToList(); 
            return View(list);
        }
        // Get:Document
        public ActionResult CreateDoc()
        {
            Dokuments_tbl model = new Dokuments_tbl();
            return View(model); 
        }
        [HttpPost]
        public ActionResult CreateDoc(Dokuments_tbl modelDoc)
        {
            using(DocEntities entities = new DocEntities())
            {
                entities.Dokuments_tbl.Add(modelDoc);
                entities.SaveChanges(); 
            }
            
            return View("CreateDoc", new Dokuments_tbl()); 
        }

    }
}
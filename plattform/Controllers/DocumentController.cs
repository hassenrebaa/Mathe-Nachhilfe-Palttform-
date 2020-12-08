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
        HassenDataBaseEntities3 _db; 
        public DocumentController()
        {
            _db = new HassenDataBaseEntities3(); 
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
            using(HassenDataBaseEntities3 entities = new HassenDataBaseEntities3())
            {
                entities.Dokuments_tbl.Add(modelDoc);
                entities.SaveChanges(); 
            }
            
            return View("CreateDoc", new Dokuments_tbl()); 
        }
        //GET 
        public  ActionResult Update()
        {
            Dokuments_tbl model = new Dokuments_tbl();
            return View(model);
        }
        [HttpPost]

        public ActionResult Update (int id , Dokuments_tbl model )
        {
            using( HassenDataBaseEntities3 entities = new HassenDataBaseEntities3())
            {
                var neumodel = entities.Dokuments_tbl.Where(x => x.id == id).FirstOrDefault();
                {
                    neumodel.id = model.id;
                    neumodel.title = model.title;
                    neumodel.Datei = model.Datei;
                    neumodel.author = model.author;
                    entities.SaveChanges();
                    return View("Update", new Dokuments_tbl());

                        

                }
            }
        }
        //GET 
        public ActionResult Delete()
        {
            Dokuments_tbl model = new Dokuments_tbl();
            return View(model);
        }

        [HttpPost]

        public ActionResult Delete(int id, Dokuments_tbl model)
        {
            using (HassenDataBaseEntities3 entities = new HassenDataBaseEntities3())
            {
              
                {
                    var data = (from x in entities.Dokuments_tbl where x.id == id select x).FirstOrDefault();
                    entities.Dokuments_tbl.Remove(data);
                    entities.SaveChanges();
                           
                }
                return View("Delete", new Dokuments_tbl());
            }
        }


    }
}
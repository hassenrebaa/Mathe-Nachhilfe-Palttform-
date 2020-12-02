using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Mathe_Nachhilfe_Plattform.Models;
using Mathe_Nachhilfe_Plattform.Models.Respositories;
using Mathe_Nachhilfe_Plattform.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mathe_Nachhilfe_Plattform.Controllers
{
    public class DokumentController : Controller
    {
        private readonly IDokumentstoreRepository<dokument> DokumentRespository;
        private readonly IDokumentstoreRepository<user> UserRepository;
        private readonly IHostingEnvironment hosting;

        public DokumentController(IDokumentstoreRepository<dokument> DokumentRespository,
            IDokumentstoreRepository<user> UserRepository,
            IHostingEnvironment hosting)
        {
            
            this.DokumentRespository = DokumentRespository;
            this.UserRepository = UserRepository;
            this.hosting = hosting;
        }
        // GET: DokumentController
        public ActionResult Index()
        {
            var Dokuments = DokumentRespository.List();
            return View(Dokuments);
        }

        // GET: DokumentController/Details/5
        public ActionResult Details(int id)
        {
            var Dokument = DokumentRespository.Find(id);
            return View(Dokument);
        }

        // GET: DokumentController/Create
        public ActionResult Create()
        {
            var model = new DokumentUserViewModel
            {
                Users = FillSelectList()

            };
            return View(model);
        }

        // POST: DokumentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DokumentUserViewModel model)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    string fileName = UploadFile(model.File) ?? string.Empty;
                   
                   
                    if (model.UserID == -1)
                    {
                        ViewBag.Message = " Please select an author from the List!";
                       

                        return View(GetAllUsers());
                    }
                    var user = UserRepository.Find(model.UserID);
                    dokument dokument = new dokument
                    {
                        id = model.DokumentID,
                        title = model.Title,
                        description = model.Description,
                        user = user,
                        DokumentUrl=fileName


                    };
                    DokumentRespository.Add(dokument);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            var vmodel = new DokumentUserViewModel
            {
                Users =FillSelectList()
            };
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View(GetAllUsers() );
        }

        // GET: DokumentController/Edit/5
        public ActionResult Edit(int id)
        {
            var dokument = DokumentRespository.Find(id);
            var UserID= dokument.user == null ? dokument.user.id =0 :dokument.user.id;
            var viewModel = new DokumentUserViewModel
            {
                DokumentID = dokument.id,
                Title = dokument.title,
                Description= dokument.description,
                UserID = dokument.user.id,
                Users= UserRepository.List().ToList(),
                DokumentUrl =dokument.DokumentUrl

            };
            return View(viewModel);
        }

        // POST: DokumentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DokumentUserViewModel viewModel)
        {
            try
            {
                string fileName = UploadFile(viewModel.File,viewModel.DokumentUrl);

               
                var user = UserRepository.Find(viewModel.UserID);
                dokument dokument = new dokument
                {
                    id=viewModel.DokumentID,
                    title = viewModel.Title,
                    description = viewModel.Description,
                    user = user,
                    DokumentUrl= fileName

                };
                DokumentRespository.Update(viewModel.DokumentID,dokument); 
                return RedirectToAction(nameof(Index));
            }
            catch( Exception ex)
           
            {
                return View();
            }
        }

        // GET: DokumentController/Delete/5
        public ActionResult Delete(int id)
        {
            var dokument = DokumentRespository.Find(id);

            return View(dokument);
        }

        // POST: DokumentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                DokumentRespository.Delete(id);
                    return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<user> FillSelectList()
        {
            var users = UserRepository.List().ToList();
            users.Insert(0, new user { id = -1, nachname = "---Please select an user ----" });

            return users;
        }

        DokumentUserViewModel GetAllUsers()
        {
            var vmodel = new DokumentUserViewModel
            {
                Users = FillSelectList()
            };
            return vmodel;
        }

        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }
            return null;
        }
        string UploadFile(IFormFile file,string DokumetUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
               
                string newPath = Path.Combine(uploads,file.FileName);

              
                string oldPath = Path.Combine(uploads,DokumetUrl);
                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);

                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;
            }
            return DokumetUrl;
        }

        public ActionResult Search(string term )
        {
            var result = DokumentRespository.Search(term);
            return View("Index",result);
        }
    }
}

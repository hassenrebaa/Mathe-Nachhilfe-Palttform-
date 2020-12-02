 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mathe_Nachhilfe_Plattform.Models;
using Mathe_Nachhilfe_Plattform.Models.Respositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mathe_Nachhilfe_Plattform.Controllers
{
    public class userController : Controller
    {
        private readonly IDokumentstoreRepository<user> UserRepository;
        public userController(IDokumentstoreRepository<user> UserRepository)
        {
            this.UserRepository = UserRepository;
        }
        // GET: userController
        public ActionResult Index()
        {
            var Users = UserRepository.List();
            return View(Users);
        }
       
        // GET: userController/Details/5
        public ActionResult Details(int id)
        {
            var user = UserRepository.Find(id);
            return View(user);
        }

        // GET: userController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: userController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(user test)
        {
            try
            {
                UserRepository.Add(test);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: userController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: userController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: userController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: userController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                UserRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

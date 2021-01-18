using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace plattform.Controllers
{
    public class UserController : Controller
    {
        HassenDataBaseEntities7 _db;
        public UserController()
        {
            _db = new HassenDataBaseEntities7();
        }
        public ActionResult List()
        {
            var test = _db.user_tbl.ToList();
            return View(test);
        }
        // GET: User

        public ActionResult Index()
        { 
            return View();
        }
        //fetch Data From Database to show in Datatable
        public ActionResult GetData()
        {
            using(HassenDataBaseEntities7 db = new HassenDataBaseEntities7())
            {
                List<user_tbl> UserList = db.user_tbl.ToList<user_tbl>();
                return Json(new { data = UserList }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult StoreOrEdit (int id =0)
        {
            if (id == 0)
                return View(new user_tbl());
            else
            {
                using(HassenDataBaseEntities7 db =new HassenDataBaseEntities7())
                {
                    return View(db.user_tbl.Where(x => x.id == id).FirstOrDefault<user_tbl>());
                }
            }
        }
        [HttpPost]
        public ActionResult StoreOrEdit (user_tbl usermodel)
        {
            using(HassenDataBaseEntities7 db =new HassenDataBaseEntities7())
            {
                if (usermodel.id == 0)
                {
                    db.user_tbl.Add(usermodel);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully", JsonRequestBehavior.AllowGet });

                }
                else
                {
                    db.Entry(usermodel).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { sucess = true, message = "Updated Successfully", JsonRequestBehavior.AllowGet });
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id )
        {
            using(HassenDataBaseEntities7 db =new HassenDataBaseEntities7())
            {
                user_tbl emp = db.user_tbl.Where(x => x.id == id).FirstOrDefault<user_tbl>();

                db.user_tbl.Remove(emp);
                db.SaveChanges();
                return Json(new { sucess = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });

            }
        }


        [HttpGet]
        public async Task<ActionResult> Index(string searchString) 
        {
            ViewData["Getdetails"] = searchString;
            var modelquery = from x in _db.user_tbl select x;
            if (!string.IsNullOrEmpty(searchString))
            {
                modelquery = modelquery.Where(x => x.email.Contains(searchString));
            }
            return View(await modelquery.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
#pragma warning restore format
        {
            user_tbl usermodel = new user_tbl();
            return View(usermodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddOrEdit([Bind(Exclude = "IsEmailVerified, ActivationCode")] user_tbl users)
        {
            bool Status = false;
            string message = "";
           //Model Validation
           if(ModelState.IsValid)
            {
                //email is alerdy Exist
                var isExist = IsEmailExist(users.email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email bereit raus");
                    return View(users);
                }

                users.ActivationCode = Guid.NewGuid();
                users.password = Crypto.Hash(users.password);
                users.IsEmailverified = false;
                using (HassenDataBaseEntities7 _db= new HassenDataBaseEntities7())
                {
                    _db.user_tbl.Add(users);
                    _db.SaveChanges();
                    SendVerificationLinkEmail(users.email, users.ActivationCode.ToString());
                    message = "Registration erfolgreich Konto Aktiviation Link " + "Email wurd an Ihre Email breits gesendet :" + users.email;
                    Status = true;
                }
            }
           else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return View();
        }
        [HttpGet]

        // VerifyAccount
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using(HassenDataBaseEntities7 dc =new HassenDataBaseEntities7())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;  // this Line I Have added here to avoid 
                var v = dc.user_tbl.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if(v != null)
                {
                    v.IsEmailverified = true;
                    dc.SaveChanges();
                    Status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request";
                }
            }
            ViewBag.Status = true;
            return View();
        }

        [NonAction]
        public bool IsEmailExist(string email)
        {
            using(HassenDataBaseEntities7 user =new HassenDataBaseEntities7())
            {
                var v = user.user_tbl.Where( a=> a.email == email).FirstOrDefault();
                return v != null;
            }
        }
        [NonAction]
        
        public void SendVerificationLinkEmail (string email ,string activiationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activiationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("hassenrebaa9@gmail.com", "Mathe-Nachhilfe-Plattform");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "*************"; //Replace with actual password
            string subject = "Your account is successfully created";

            string body = "<br/><br/>We are excited to tell you that your Mathe-Nachhilfe-Account account is" +
                   " successfully created. Please click on the below link to verify your account" +
                   " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using(var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                    smtp.Send(message);
        }

#pragma warning disable format
     
        // Login  funktion
        [HttpPost]
        public ActionResult alteAddOrEdit (user_tbl usermodel)
        {
            using(HassenDataBaseEntities7 model =new HassenDataBaseEntities7())
            {
                model.user_tbl.Add(usermodel);
                model.SaveChanges();
              

            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("AddOrEdit", new user_tbl());
        }
        //GET 
        public ActionResult Update()
        {
            user_tbl usermodel = new user_tbl();
            return View(usermodel);

        }
        [HttpPost]
        public ActionResult update(int id, user_tbl usermodel)
        {
            
            using (HassenDataBaseEntities7 entities = new HassenDataBaseEntities7())
            {
                var neumodel = entities.user_tbl.Where(x => x.id == id).FirstOrDefault();
                neumodel.vorname = usermodel.vorname;
                neumodel.nachname = usermodel.nachname;
                neumodel.id = usermodel.id;
                neumodel.mobile = usermodel.mobile;
                neumodel.email = usermodel.email;
                neumodel.adresse = usermodel.adresse;
              
                usermodel.password = Crypto.Hash(neumodel.password);
                entities.SaveChanges();
                return View("Update", new user_tbl());
            }
        }
    }
}
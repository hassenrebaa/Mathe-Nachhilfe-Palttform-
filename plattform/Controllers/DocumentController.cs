using AngleSharp.Dom;
using Dapper;
using Intercom.Core;
using plattform.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Volo.Abp.Data;

namespace  plattform.Controllers
{
    public class DocumentController : Controller
    {
        
        // GET: Document
        HassenDataBaseEntitiesDokument _db; 
        public DocumentController()
        {
            _db = new HassenDataBaseEntitiesDokument(); 

           
        }
        public ActionResult Index()
        {
            var list = _db.Doc_tbl.ToList();
            return View(list);
        }
        
        
        



        
        
             


                



       

       
    
    [HttpGet]
    public async Task<ActionResult> Index(string searchString)
        {
            ViewData["Getdetails"] = searchString;
            var modelquery = from x in _db.Doc_tbl select x;
            if (!string.IsNullOrEmpty(searchString))
            {
                modelquery = modelquery.Where(x => x.author.Contains(searchString) || x.titel.Contains(searchString)|| x.SerieNr.Contains(searchString)||x.bezeichnung.Contains(searchString));
            }
            return View(await modelquery.AsNoTracking().ToListAsync());
        }
        // Get:Document
      [HttpGet] 
       public ActionResult CreateDokument()
        {
            return View();

        }

        [HttpPost]
        public ActionResult CreateDokument(Doc_tbl modelDocc, HttpPostedFileBase files)
        {
            using (HassenDataBaseEntitiesDokument entities = new HassenDataBaseEntitiesDokument())
            {
                string FileExt = Path.GetExtension(files.FileName).ToUpper();
                if (FileExt == ".PDF")
                {
                    Stream str = files.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                    DcModel Fd = new Models.DcModel();
                    Fd.title = modelDocc.titel;
                    Fd.author = modelDocc.author;
                    Fd.bezeichnung = modelDocc.bezeichnung;
                    Fd.SerieNr = modelDocc.SerieNr;
                    Fd.FileName = files.FileName;
                   
                    Fd.FileContent = FileDet;
                    SaveFileDetails(Fd);
                    return RedirectToAction("CreateDokument");
                }
                else
                {
                    ViewBag.FileStatus = "Invalid file format.";
                    return View();
                }

            }
        }
            private void SaveFileDetails(DcModel objDet)
            {
                DynamicParameters Parm = new DynamicParameters();
                Parm.Add("@titel", objDet.title);
                Parm.Add("@author", objDet.author);
                Parm.Add("@bezeichnung", objDet.bezeichnung);
                Parm.Add("@SerieNr", objDet.SerieNr);
                Parm.Add("@FileName", objDet.FileName);
               
                
               
                Parm.Add("@FileContent", objDet.FileContent);
                Database();





                con.Execute("AddDoco", Parm, commandType: System.Data.CommandType.StoredProcedure);

                con.Close();


            }
        SqlConnection con = new SqlConnection();
        public void Database()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "DESKTOP-R2VCHQM\\MSSQLSERVER01";
            builder.InitialCatalog = "HassenDataBase";
            builder.UserID = "DESKTOP-R2VCHQM\\Dell E7450";


            builder.IntegratedSecurity = true;
            //TCP/IP            

            con = new SqlConnection(builder.ToString());
            con.Open();
        }
        public void Open()
        {
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Close()
        {
            con.Close();
        }








        [HttpGet]
        public FileResult DownLoadFile(int id)
        {
            List<Doc_tbl> ObjFiles = _db.Doc_tbl.ToList();
            var FileById = (from x in ObjFiles where x.id.Equals(id) select new { x.titel, x.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "appliction/pdf", FileById.titel);
        }
        [HttpGet]
        protected void DownloadF(object sender ,EventArgs e)
        {
            int id =int.Parse( (sender as LinkButton).CommandArgument);
            byte[] bytes;
            string Filename, ContentType;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using( SqlConnection con =new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "Select Name, Data, Content Type form tblFiles where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        ContentType = sdr["ContentType"].ToString();
                        Filename = sdr["Name"].ToString();
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment ; filename=" + Filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
        //GET 
        public  ActionResult Update()
        {
            Doc_tbl model = new Doc_tbl();
            return View(model);
        }
        [HttpPost]

        public ActionResult Update (int id , Doc_tbl model )
        {
            using( HassenDataBaseEntitiesDokument entities = new HassenDataBaseEntitiesDokument())
            {
                var neumodel = entities.Doc_tbl.Where(x => x.id == id).FirstOrDefault();
                {
                 
                    neumodel.titel = model.titel;
                    
                    neumodel.author = model.author;
                    neumodel.bezeichnung = model.bezeichnung;
                    neumodel.SerieNr= model.SerieNr;

                    entities.SaveChanges();
                    return View("Update", new Doc_tbl());

                        

                }
            }
        }
        //GET 
        public ActionResult Delete()
        {
            Doc_tbl model = new Doc_tbl();
            return View(model);
        }

        [HttpPost]

        public ActionResult Delete(int id, Doc_tbl model)
        {
            using (HassenDataBaseEntitiesDokument entities = new HassenDataBaseEntitiesDokument())
            {
              
                {
                    var data = (from x in entities.Doc_tbl where x.id == id select x).FirstOrDefault();
                    entities.Doc_tbl.Remove(data);
                    entities.SaveChanges();
                           
                }
                return View("Delete", new Doc_tbl());
            }
        }


    }
}
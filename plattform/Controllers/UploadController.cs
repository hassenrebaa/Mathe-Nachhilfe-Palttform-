using Dapper;
using MySql.Data.MySqlClient;
using plattform.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plattform.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult FileUpload()
        {
            return View();
        }


        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase files)
        {

            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                EmModel Fd = new Models.EmModel();
                Fd.FileName = files.FileName;
                Fd.FileContent = FileDet;
                SaveFileDetails(Fd);
                return RedirectToAction("FileUpload");
            }
            else
            {

                ViewBag.FileStatus = "Invalid file format.";
                return View();

            }

        }

        private void SaveFileDetails(EmModel objDet)
        {

            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            Database();
           
           
                 


            con.Execute("AddFileDetailss", Parm, commandType: System.Data.CommandType.StoredProcedure) ;

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
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public void Close()
        {
            con.Close();
        }
       
    }

}
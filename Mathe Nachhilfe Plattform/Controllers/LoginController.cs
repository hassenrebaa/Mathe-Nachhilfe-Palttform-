
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mathe_Nachhilfe_Plattform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.SqlClient;
using Mathe_Nachhilfe_Plattform.Migrations;
using Microsoft.AspNetCore.Builder;



namespace Mathe_Nachhilfe_Plattform.Controllers
{
    public class LoginController : Controller
    {





        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        void connectionString()
        {
            // con.ConnectionString = "data source=MSSQLLocalDB(SQL Server 13.0.4001 - DESKTOP-R2VCHQM); database=MyMathenachhilfeplattformDB; integrated security =SSPI;";
        }
        






    }
}

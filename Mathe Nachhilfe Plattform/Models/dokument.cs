using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Mathe_Nachhilfe_Plattform.Models
{
    public class dokument
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public  string DokumentUrl { get; set; }
        public user user { get; set; }
     
       
     

       
    }
}

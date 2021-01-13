using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace plattform.Models
{
    public class DcModel
    {
        public int id { get; set; }
        
        public string title { get; set; }
        
        public string author { get; set; }

        public string bezeichnung { get; set; }
       
        public string SerieNr { get; set; }

        public string FileName { get; set; }
       
        public byte[] FileContent { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]

        public HttpPostedFileBase files { get; set; }
    }
}
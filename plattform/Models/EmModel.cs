using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace plattform.Models
{
    public class EmModel
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name ="Select File")]
        public HttpPostedFileBase files { get; set; }
    }
}
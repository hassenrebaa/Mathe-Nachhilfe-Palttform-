using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.Models
{
    public class user
    {
        public int id { get; set; }
       
        public string vorname  { get; set; }
        public string nachname { get; set; }
        public string adresse{ get; set; }
       

       
        [Required]
        [EmailAddress]
        public string email { get; set; }
       
        public string mobile  { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}

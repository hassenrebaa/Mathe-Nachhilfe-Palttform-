using Mathe_Nachhilfe_Plattform.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mathe_Nachhilfe_Plattform.ViewModels
{
    public class DokumentUserViewModel
    {
        public int DokumentID { get; set;  }

        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(120,MinimumLength = 5)]
        public string Description { get; set;  }

        public int UserID { get; set;  }

        public List<user> Users { get; set; }

        public IFormFile File { get; set; }

        public string DokumentUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Docflow.Models
{
    public class DocumentEditViewModel
    {
        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public Folder Folder { get; set; }

        [Display(Name = "Файл")]
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}
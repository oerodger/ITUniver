using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Docflow.Models
{
    public class FolderEditViewModel
    {
        public Folder ParentFolder { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }
    }
}
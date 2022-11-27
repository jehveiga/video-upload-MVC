using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Video_Upload_MVC.Models
{
    public class Upload
    {
        [Display(Name = "Upload Video Files:")]
        public string Vname { get; set; }
        public string Vpath { get; set; }
    }
}
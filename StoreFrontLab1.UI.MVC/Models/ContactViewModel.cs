using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreFrontLab1.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Name is requiured")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "* Please check the format on your email")]
        [Required(ErrorMessage = "* Email is rewuired")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        [Required(ErrorMessage = "* Message is required")]
        public string Message { get; set; }
    }
}
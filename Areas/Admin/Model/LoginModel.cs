using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Electronic_Store.Areas.Admin.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Admin name required")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "Admin password required")]
        public string Password { get; set; }
        public string RememberMe { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Electronic_Store.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Adminstrator
    {
        public int AdminID { get; set; }
        [Required]
        public string AdminName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

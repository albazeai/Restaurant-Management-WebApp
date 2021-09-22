using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "First Name ")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Last Name ")]
        public string LastName { get; set; }

        [Display(Name = "Birth Date")]
        //[Column(TypeName = "date")]   // need to run migration
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Gender { get; set; }

        [Display(Name = "Street Number ")]
        public int StreetNumber { get; set; }

        [Column(TypeName = "varchar(7)")]
        [Display(Name = "Postal Code ")]
        public string PostalCode { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Address { get; set; }
        [Column(TypeName = "varchar(100)")]

        public string City { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Province { get; set; }

        public string Notes { get; set; }

        [Display(Name = "Hired Date ")]
        public DateTime DateCreated { get; set; }
    }
}

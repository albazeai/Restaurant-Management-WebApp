using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Drink
    {
        [Key]
        public int DrinkId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Name")]
        public string DrinkName { get; set; }
        [Required]
        public double Price { get; set; }

        [Display(Name = "Image")]
        public byte[] DrinkImage { get; set; }


        [ForeignKey("CategoryId")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

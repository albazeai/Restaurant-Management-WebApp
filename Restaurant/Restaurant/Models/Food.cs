using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Name")]
        public string FoodName { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Recipes { get; set; }

        [Display(Name = "Image")]
        public byte[] FoodImage { get; set; }

        [ForeignKey("CategoryId")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}

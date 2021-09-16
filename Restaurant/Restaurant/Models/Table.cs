using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Table Number")]

        public int TableId { get; set; }
        [Display(Name = "Items")]

        public string TableItems { get; set; }

        public double Total { get; set; }
    }
}

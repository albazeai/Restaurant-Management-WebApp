using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        [Display(Name = "Table Number")]
        public int TableId { get; set; }
        public string Items { get; set; }
        public string Notes { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
    }
}

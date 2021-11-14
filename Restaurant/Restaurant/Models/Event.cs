using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Reservation Required")]
        public bool ReservationRequired { get; set; }

        [Display(Name = "Image")]
        public byte[] EventImage { get; set; }
        [Required]
        [Display(Name = "Event Seats")]
        public int ReservationSeats { get; set; }

        public int Reserved { get; set; }
    }
}

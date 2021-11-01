using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Reservation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int People { get; set; }

        public bool Attendance { get; set; }

        [Required]
        [Display(Name = "Confirmation Name")]
        public string ConfirmationName { get; set; }

        [Required]
        [Display(Name = "Confirmation Phone")]
        public string ConfirmationPhone { get; set; }

        [Required]
        [ForeignKey("EventId")]
        [Display(Name = "Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITuto.Models
{
    public partial class FlightSet
    {
        public FlightSet()
        {
            BookingSet = new HashSet<BookingSet>();
        }

        [Key]
        public int FlightNo { get; set; }
        [StringLength(50)]
        public string Departure { get; set; }
        [StringLength(50)]
        public string Destination { get; set; }
        public DateTime Date { get; set; }
        public short Seats { get; set; }
        public int PilotId { get; set; }

        [ForeignKey(nameof(PilotId))]
        [InverseProperty(nameof(PilotSet.FlightSet))]
        public virtual PilotSet Pilot { get; set; }
        [InverseProperty("FlightNoNavigation")]
        public virtual ICollection<BookingSet> BookingSet { get; set; }
    }
}

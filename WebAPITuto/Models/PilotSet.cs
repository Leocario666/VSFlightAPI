using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITuto.Models
{
    public partial class PilotSet
    {
        public PilotSet()
        {
            FlightSet = new HashSet<FlightSet>();
        }

        [Key]
        [Column("PersonID")]
        public int PersonId { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public float Salary { get; set; }
        public int? FlightHours { get; set; }

        [InverseProperty("Pilot")]
        public virtual ICollection<FlightSet> FlightSet { get; set; }
    }
}

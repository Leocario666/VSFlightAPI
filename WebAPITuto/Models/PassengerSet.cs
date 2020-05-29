using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITuto.Models
{
    public partial class PassengerSet
    {
        public PassengerSet()
        {
            BookingSet = new HashSet<BookingSet>();
        }

        [Key]
        [Column("PersonID")]
        public int PersonId { get; set; }
        public string Surname { get; set; }
        public string GivenName { get; set; }
        public int Weight { get; set; }

        [InverseProperty("Passenger")]
        public virtual ICollection<BookingSet> BookingSet { get; set; }
    }
}

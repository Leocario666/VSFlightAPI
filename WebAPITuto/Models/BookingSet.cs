using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPITuto.Models
{
    public partial class BookingSet
    {
        [Key]
        public int BookingId { get; set; }
        
        public int FlightNo { get; set; }
 
        public int PassengerId { get; set; }
        public int Price { get; set; }

        [ForeignKey(nameof(FlightNo))]
        [InverseProperty(nameof(FlightSet.BookingSet))]
        public virtual FlightSet FlightNoNavigation { get; set; }
        [ForeignKey(nameof(PassengerId))]
        [InverseProperty(nameof(PassengerSet.BookingSet))]
        public virtual PassengerSet Passenger { get; set; }
    }
}

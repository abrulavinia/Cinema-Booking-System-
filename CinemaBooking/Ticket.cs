using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CinemaBooking
{
    internal class Ticket
    {

        [JsonPropertyName("id")]
        public long Id { get; set; }


        [JsonPropertyName("screening")]
        public Screening Screening { get; set; } = new();


        [JsonPropertyName("customerEmail")] 
        public string CustomerEmail { get; set; } = "";
       
        
        
        [JsonPropertyName("purchasedAt")] 
        public DateTime PurchasedAt { get; set; }


        [JsonPropertyName("status")]
        public string Status { get; set; } = "";
    }
}

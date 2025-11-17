using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CinemaBooking
{
    internal class Screening
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }


        [JsonPropertyName("movie")]
        public Movie Movie { get; set; } = new();


        [JsonPropertyName("hall")]
        public string Hall { get; set; } = "";


        [JsonPropertyName("time")]
        public DateTime Time { get; set; }


        [JsonPropertyName("seats_total")] 
        public int SeatsTotal { get; set; }


        [JsonPropertyName("seats_sold")] 
        public int SeatsSold { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }
    }
}

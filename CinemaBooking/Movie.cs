using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CinemaBooking
{
    internal class Movie
    {
        [JsonPropertyName("id")]
        public long id { get; set; }


        [JsonPropertyName("title")]
        public string title { get; set; } = "";


        [JsonPropertyName("genre")]
        public string genre { get; set; } = "";


        [JsonPropertyName("duration")]
        public int duration {get; set; }


        [JsonPropertyName("status")]
        public string? status { get; set; }
     
    }
}

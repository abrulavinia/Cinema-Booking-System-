using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CinemaBooking
{
    internal class PackageServices
    {
        static HttpClient client = new HttpClient();
        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public List<Movie> GetMovies()
        {
            HttpResponseMessage response = client.GetAsync("api/movies").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Movie>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Movie GetMovieById(long id)
        {
            HttpResponseMessage response = client.GetAsync($"api/movies/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Movie>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Movie CreateMovie(Movie m)
        {
            string payload = JsonSerializer.Serialize(m);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/movies", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Movie>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Movie UpdateMovie(long id, Movie m)
        {
            string payload = JsonSerializer.Serialize(m);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"api/movies/{id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Movie>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public bool DeleteMovie(long id)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/movies/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        public List<Movie> FindMoviesByGenre(string genre)
        {
            HttpResponseMessage response = client.GetAsync($"api/movies/by-genre/{Uri.EscapeDataString(genre)}").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Movie>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        // ===================== SCREENINGS =====================

        public List<Screening> GetScreenings()
        {
            HttpResponseMessage response = client.GetAsync("api/screenings").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Screening>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Screening GetScreeningById(long id)
        {
            HttpResponseMessage response = client.GetAsync($"api/screenings/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Screening>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Screening CreateScreening(Screening s)
        {
            string payload = JsonSerializer.Serialize(s);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/screenings", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Screening>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Screening UpdateScreening(long id, Screening s)
        {
            string payload = JsonSerializer.Serialize(s);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync($"api/screenings/{id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Screening>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public bool DeleteScreening(long id)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/screenings/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        // search cu parametri opționali (movieId/from/to)
        public List<Screening> SearchScreenings(long? movieId, DateTime? from, DateTime? to)
        {
            var qs = new StringBuilder("api/screenings/search");
            var first = true;

            if (movieId.HasValue)
            {
                qs.Append(first ? "?" : "&").Append("movieId=").Append(movieId.Value);
                first = false;
            }
            if (from.HasValue)
            {
                qs.Append(first ? "?" : "&").Append("from=").Append(from.Value.ToString("yyyy-MM-ddTHH:mm:ss"));
                first = false;
            }
            if (to.HasValue)
            {
                qs.Append(first ? "?" : "&").Append("to=").Append(to.Value.ToString("yyyy-MM-ddTHH:mm:ss"));
            }

            HttpResponseMessage response = client.GetAsync(qs.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Screening>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public List<Screening> GetMostPopularScreenings(int top = 5)
        {
            HttpResponseMessage response = client.GetAsync($"api/screenings/popular?top={top}").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Screening>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        // ===================== TICKETS =====================

        public List<Ticket> GetTickets()
        {
            HttpResponseMessage response = client.GetAsync("api/tickets").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Ticket>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public Ticket BuyTicket(long screeningId, string email)
        {
            HttpResponseMessage response = client.PostAsync($"api/tickets/buy/{screeningId}?email={Uri.EscapeDataString(email)}", null).Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<Ticket>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }

        public bool CancelTicket(long ticketId)
        {
            HttpResponseMessage response = client.DeleteAsync($"api/tickets/{ticketId}").Result;
            return response.IsSuccessStatusCode;
        }

        // /api/tickets/sold-per-movie – poate întoarce fie [{title, soldTickets}], fie [["Title",123],...]
        public List<TicketSoldPerMovie> GetTicketsSoldPerMovie()
        {
            HttpResponseMessage response = client.GetAsync("api/tickets/sold-per-movie").Result;
            if (!response.IsSuccessStatusCode) return null;

            string json = response.Content.ReadAsStringAsync().Result;

            // încercăm direct mapping pe obiect
            try
            {
                var direct = JsonSerializer.Deserialize<List<TicketSoldPerMovie>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (direct != null) return direct;
            }
            catch { /* fallback jos */ }

            // fallback pentru List<object[]>
            var list = new List<TicketSoldPerMovie>();
            using (var doc = JsonDocument.Parse(json))
            {
                foreach (var el in doc.RootElement.EnumerateArray())
                {
                    string title = el[0].GetString();
                    long sold = el[1].GetInt64();
                    list.Add(new TicketSoldPerMovie { Title = title, SoldTickets = sold });
                }
            }
            return list;
        }

        // ===================== RECOMMENDATIONS =====================

        public List<Screening> GetRecommendations(string email)
        {
            HttpResponseMessage response = client.GetAsync($"api/recommendations?email={Uri.EscapeDataString(email)}").Result;
            if (response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<List<Screening>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            return null;
        }
    }
}


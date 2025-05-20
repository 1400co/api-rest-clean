using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class CreateNumberingRangeResponseDto
    {
        // Assuming the API returns the created object, possibly with an ID
        [JsonPropertyName("id")]
        public int Id { get; set; } // Example: if the API returns an ID for the created range

        [JsonPropertyName("document")]
        public int Document { get; set; }

        [JsonPropertyName("prefix")]
        public string Prefix { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("current")]
        public int Current { get; set; }

        [JsonPropertyName("resolution_number")]
        public string ResolutionNumber { get; set; }

        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("technical_key")]
        public string TechnicalKey { get; set; }

        // Add any other fields returned by the API, e.g., created_at, updated_at
    }
} 
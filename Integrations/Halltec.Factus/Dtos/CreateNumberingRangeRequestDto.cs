using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class CreateNumberingRangeRequestDto
    {
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
        public string StartDate { get; set; } // Expected format: "YYYY-MM-DD"

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; } // Expected format: "YYYY-MM-DD"

        [JsonPropertyName("technical_key")]
        public string TechnicalKey { get; set; }
    }
} 
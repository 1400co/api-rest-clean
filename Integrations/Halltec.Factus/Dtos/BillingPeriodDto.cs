using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class BillingPeriodDto
    {
        [JsonPropertyName("start_date")]
        public string StartDate { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("end_date")]
        public string EndDate { get; set; }

        [JsonPropertyName("end_time")]
        public string EndTime { get; set; }
    }
} 
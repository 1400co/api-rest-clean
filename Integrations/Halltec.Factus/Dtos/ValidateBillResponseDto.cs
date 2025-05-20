using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class ValidateBillResponseDto
    {
        // Example properties - adjust based on actual API response
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } 

        // You might have more complex error objects or other data
        // For example, if the API returns the validated bill data:
        // [JsonPropertyName("data")]
        // public BillRequestDto Data { get; set; } 
    }
} 
using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class CustomerDto
    {
        [JsonPropertyName("identification")]
        public string Identification { get; set; }

        [JsonPropertyName("dv")]
        public string Dv { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("trade_name")]
        public string TradeName { get; set; }

        [JsonPropertyName("names")]
        public string Names { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("legal_organization_id")]
        public string LegalOrganizationId { get; set; }

        [JsonPropertyName("tribute_id")]
        public string TributeId { get; set; }

        [JsonPropertyName("identification_document_id")]
        public string IdentificationDocumentId { get; set; }

        [JsonPropertyName("municipality_id")]
        public string MunicipalityId { get; set; }
    }
} 
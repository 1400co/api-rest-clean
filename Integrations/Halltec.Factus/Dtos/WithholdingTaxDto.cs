using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class WithholdingTaxDto
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("withholding_tax_rate")]
        public string WithholdingTaxRate { get; set; }
    }
} 
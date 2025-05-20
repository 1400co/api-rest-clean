using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class ItemDto
    {
        [JsonPropertyName("code_reference")]
        public string CodeReference { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("discount_rate")]
        public int DiscountRate { get; set; } // Assuming int based on example, could be decimal

        [JsonPropertyName("price")]
        public decimal Price { get; set; } // Assuming decimal for price

        [JsonPropertyName("tax_rate")]
        public string TaxRate { get; set; }

        [JsonPropertyName("unit_measure_id")]
        public int UnitMeasureId { get; set; }

        [JsonPropertyName("standard_code_id")]
        public int StandardCodeId { get; set; }

        [JsonPropertyName("is_excluded")]
        public int IsExcluded { get; set; } // 0 or 1, so int is fine

        [JsonPropertyName("tribute_id")]
        public int TributeId { get; set; }

        [JsonPropertyName("withholding_taxes")]
        public List<WithholdingTaxDto> WithholdingTaxes { get; set; }
    }
} 
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos
{
    public class BillRequestDto
    {
        [JsonPropertyName("numbering_range_id")]
        public int NumberingRangeId { get; set; }

        [JsonPropertyName("reference_code")]
        public string ReferenceCode { get; set; }

        [JsonPropertyName("observation")]
        public string Observation { get; set; }

        [JsonPropertyName("payment_form")]
        public string PaymentForm { get; set; }

        [JsonPropertyName("payment_due_date")]
        public string PaymentDueDate { get; set; }

        [JsonPropertyName("payment_method_code")]
        public string PaymentMethodCode { get; set; }

        [JsonPropertyName("billing_period")]
        public BillingPeriodDto BillingPeriod { get; set; }

        [JsonPropertyName("customer")]
        public CustomerDto Customer { get; set; }

        [JsonPropertyName("items")]
        public List<ItemDto> Items { get; set; }
    }
} 
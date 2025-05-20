using System.Text.Json.Serialization;
using System.Collections.Generic;
using ApiRestClean.Integrations.Halltec.Factus.Dtos.SerializationUtils;

namespace ApiRestClean.Integrations.Halltec.Factus.Dtos.ValidationResponse
{
    public class ValidateBillResponseDto
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public ValidateBillDataDto? Data { get; set; }
    }

    public class ValidateBillDataDto
    {
        [JsonPropertyName("company")]
        public CompanyDataDto? Company { get; set; }

        [JsonPropertyName("customer")]
        public CustomerDataDto? Customer { get; set; }

        [JsonPropertyName("numbering_range")]
        public NumberingRangeDataDto? NumberingRange { get; set; }

        [JsonPropertyName("billing_period")]
        [JsonConverter(typeof(EmptyObjectToListConverter<BillingPeriod>))]
        public List<BillingPeriod>? BillingPeriod { get; set; }

        [JsonPropertyName("bill")]
        public BillDataDto? Bill { get; set; }

     

        [JsonPropertyName("items")]
        public List<ValidatedItemDto>? Items { get; set; }

        [JsonPropertyName("withholding_taxes")]
        public List<OverallWithholdingTaxDto>? WithholdingTaxes { get; set; }
        
      
    }

    public class BillingPeriod
    {
        public string start_date { get; set; }
        public string start_time { get; set; }
        public string end_date { get; set; }
        public string end_time { get; set; }
    }

    public class CompanyDataDto
    {
        [JsonPropertyName("url_logo")]
        public string? UrlLogo { get; set; }

        [JsonPropertyName("nit")]
        public string? Nit { get; set; }

        [JsonPropertyName("dv")]
        public string? Dv { get; set; }

        [JsonPropertyName("company")]
        public string? CompanyName { get; set; } // Renamed to avoid conflict with class name if used as type

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("graphic_representation_name")]
        public string? GraphicRepresentationName { get; set; }

        [JsonPropertyName("registration_code")]
        public string? RegistrationCode { get; set; }

        [JsonPropertyName("economic_activity")]
        public string? EconomicActivity { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("direction")]
        public string? Direction { get; set; }

        [JsonPropertyName("municipality")]
        public string? Municipality { get; set; } // As per example, this is a string for company
    }

    public class CustomerDataDto
    {
        [JsonPropertyName("identification")]
        public string? Identification { get; set; }

        [JsonPropertyName("dv")]
        public string? Dv { get; set; }

        [JsonPropertyName("graphic_representation_name")]
        public string? GraphicRepresentationName { get; set; }

        [JsonPropertyName("trade_name")]
        public string? TradeName { get; set; }
        
        [JsonPropertyName("company")]
        public string? CompanyName { get; set; } // Renamed to avoid conflict

        [JsonPropertyName("names")]
        public string? Names { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("legal_organization")]
        public LegalOrganizationDto? LegalOrganization { get; set; }

        [JsonPropertyName("tribute")]
        public CustomerTributeDto? Tribute { get; set; }

        [JsonPropertyName("municipality")]
        public MunicipalityDto? Municipality { get; set; }
    }

    public class LegalOrganizationDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class CustomerTributeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class MunicipalityDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class NumberingRangeDataDto
    {
        [JsonPropertyName("prefix")]
        public string? Prefix { get; set; }

        [JsonPropertyName("from")]
        public long From { get; set; }

        [JsonPropertyName("to")]
        public long To { get; set; }

        [JsonPropertyName("resolution_number")]
        public string? ResolutionNumber { get; set; }

        [JsonPropertyName("start_date")]
        public string? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string? EndDate { get; set; }

        [JsonPropertyName("months")]
        public int Months { get; set; }
    }

    public class BillDataDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("document")]
        public BillDocumentDto? Document { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("reference_code")]
        public string? ReferenceCode { get; set; }

        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("send_email")]
        public int SendEmail { get; set; } // 0 or 1

        [JsonPropertyName("qr")]
        public string? Qr { get; set; }

        [JsonPropertyName("cufe")]
        public string? Cufe { get; set; }

        [JsonPropertyName("validated")]
        public string? Validated { get; set; }

        [JsonPropertyName("discount_rate")]
        public string? DiscountRate { get; set; }

        [JsonPropertyName("discount")]
        public string? Discount { get; set; }

        [JsonPropertyName("gross_value")]
        public string? GrossValue { get; set; }

        [JsonPropertyName("taxable_amount")]
        public string? TaxableAmount { get; set; }

        [JsonPropertyName("tax_amount")]
        public string? TaxAmount { get; set; }

        [JsonPropertyName("total")]
        public string? Total { get; set; }

        [JsonPropertyName("observation")]
        public string? Observation { get; set; }

        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("payment_due_date")]
        public string? PaymentDueDate { get; set; }
        
        [JsonPropertyName("qr_image")]
        public string? QrImage { get; set; }

        [JsonPropertyName("has_claim")]
        public int HasClaim { get; set; } // 0 or 1

        [JsonPropertyName("is_negotiable_instrument")]
        public int IsNegotiableInstrument { get; set; } // 0 or 1

        [JsonPropertyName("payment_form")]
        public BillPaymentFormDto? PaymentForm { get; set; }

        [JsonPropertyName("payment_method")]
        public BillPaymentMethodDto? PaymentMethod { get; set; }
    }

    public class BillDocumentDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class BillPaymentFormDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class BillPaymentMethodDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class ValidatedItemDto
    {
        [JsonPropertyName("code_reference")]
        public string? CodeReference { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; } // Changed to decimal for precision if needed

        [JsonPropertyName("discount_rate")]
        public string? DiscountRate { get; set; }

        [JsonPropertyName("discount")]
        public string? Discount { get; set; }

        [JsonPropertyName("gross_value")]
        public string? GrossValue { get; set; }

        [JsonPropertyName("tax_rate")]
        public string? TaxRate { get; set; }

        [JsonPropertyName("taxable_amount")]
        public string? TaxableAmount { get; set; }

        [JsonPropertyName("tax_amount")]
        public string? TaxAmount { get; set; }

        [JsonPropertyName("price")]
        public string? Price { get; set; }

        [JsonPropertyName("is_excluded")]
        public int IsExcluded { get; set; } // 0 or 1

        [JsonPropertyName("unit_measure")]
        public ValidatedUnitMeasureDto? UnitMeasure { get; set; }

        [JsonPropertyName("standard_code")]
        public ValidatedStandardCodeDto? StandardCode { get; set; }

        [JsonPropertyName("tribute")]
        public ValidatedItemTributeDto? Tribute { get; set; }
        
        [JsonPropertyName("total")]
        public decimal Total { get; set; } // Changed to decimal

        [JsonPropertyName("withholding_taxes")]
        public List<ValidatedItemWithholdingTaxDto>? WithholdingTaxes { get; set; }
    }

    public class ValidatedUnitMeasureDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class ValidatedStandardCodeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class ValidatedItemTributeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class ValidatedItemWithholdingTaxDto
    {
        [JsonPropertyName("tribute_code")]
        public string? TributeCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("rates")]
        public List<ValidatedWithholdingTaxRateDto>? Rates { get; set; }
    }

    public class ValidatedWithholdingTaxRateDto
    {
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("rate")]
        public string? Rate { get; set; }
    }

    public class OverallWithholdingTaxDto
    {
        [JsonPropertyName("tribute_code")]
        public string? TributeCode { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
} 
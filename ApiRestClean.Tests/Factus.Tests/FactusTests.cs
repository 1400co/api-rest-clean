using ApiRestClean.Integrations.Halltec.Factus;
using ApiRestClean.Integrations.Halltec.Factus.Dtos;
using ApiRestClean.Integrations.Halltec.Factus.Settings;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ApiRestClean.Tests.Factus.Tests
{
    public class FactusTests : IDisposable
    {
        private readonly FactusApiSettings _factusApiSettings;
        private readonly FactusService _factusService;

        public FactusTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _factusApiSettings = new FactusApiSettings();
            configuration.GetSection("FactusApi").Bind(_factusApiSettings);

            if (string.IsNullOrWhiteSpace(_factusApiSettings.BaseUrl) ||
                string.IsNullOrWhiteSpace(_factusApiSettings.Email) ||
                string.IsNullOrWhiteSpace(_factusApiSettings.Password) ||
                string.IsNullOrWhiteSpace(_factusApiSettings.ClientId) ||
                string.IsNullOrWhiteSpace(_factusApiSettings.ClientSecret))
            {
                throw new InvalidOperationException("Una o más configuraciones de FactusApi están faltando en appsettings.json. " +
                                                    "Asegúrate de que 'appsettings.json' exista, esté configurado para copiarse al directorio de salida y contenga todas las claves necesarias bajo 'FactusApi'.");
            }

            _factusService = new FactusService(_factusApiSettings);
        }

        public void Dispose()
        {
            _factusService?.Dispose();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task GetAuthTokenAsync_WithValidSandboxCredentials_ShouldReturnToken()
        {
            TokenResponse? token = null;
            try
            {
                token = await _factusService.GetAuthTokenAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception during GetAuthTokenAsync: {ex.ToString()}");
                Assert.Fail($"GetAuthTokenAsync threw an exception: {ex.Message}");
            }

            Assert.NotNull(token);
            Assert.False(string.IsNullOrWhiteSpace(token.AccessToken), "El token no debería ser nulo o vacío.");
            System.Diagnostics.Debug.WriteLine($"Token obtenido: {token.AccessToken}");
        }

        [Fact]
        public async Task RefreshTokenAsync_WithValidRefreshToken_ShouldReturnNewToken()
        {
            TokenResponse? initialTokenResponse = null;
            try
            {
                initialTokenResponse = await _factusService.GetAuthTokenAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail($"Fallo al obtener el token inicial para la prueba de refresh: {ex.Message}");
            }

            Assert.NotNull(initialTokenResponse);
            Assert.False(string.IsNullOrWhiteSpace(initialTokenResponse.AccessToken), "El token de acceso inicial no debería ser nulo o vacío.");
            Assert.False(string.IsNullOrWhiteSpace(initialTokenResponse.RefreshToken), "El token de refresco inicial no debería ser nulo o vacío.");

            System.Diagnostics.Debug.WriteLine($"Token inicial obtenido. AccessToken: {initialTokenResponse.AccessToken}, RefreshToken: {initialTokenResponse.RefreshToken}");

            await Task.Delay(1000); 

            TokenResponse? refreshedTokenResponse = null;
            try
            {
                refreshedTokenResponse = await _factusService.RefreshTokenAsync(initialTokenResponse.RefreshToken!);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Fallo al refrescar el token: {ex.Message}");
            }

            Assert.NotNull(refreshedTokenResponse);
            Assert.False(string.IsNullOrWhiteSpace(refreshedTokenResponse.AccessToken), "El token de acceso refrescado no debería ser nulo o vacío.");
            System.Diagnostics.Debug.WriteLine($"Token refrescado obtenido. AccessToken: {refreshedTokenResponse.AccessToken}, RefreshToken: {refreshedTokenResponse.RefreshToken}");

            Assert.NotEqual(initialTokenResponse.AccessToken, refreshedTokenResponse.AccessToken);
            Assert.False(string.IsNullOrWhiteSpace(refreshedTokenResponse.RefreshToken), "El nuevo token de refresco no debería ser nulo o vacío.");
        }

        [Fact]
        public async Task ValidateBillAsync_WithValidData_ShouldReturnSuccess()
        {
     

            var billRequest = new BillRequestDto
            {
                ReferenceCode = $"I3-VALIDATE-{DateTime.Now:HHmmssfff}", // Unique reference code
                Observation = "Test observation from automated ValidateBillAsync test",
                PaymentForm = "1", // Contado
                PaymentDueDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"),
                PaymentMethodCode = "10", // Efectivo
                BillingPeriod = new BillingPeriodDto // Optional, include if your API uses/requires it
                {
                    StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    StartTime = "00:00:00",
                    EndDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd"),
                    EndTime = "23:59:59"
                },
                Customer = new CustomerDto
                {
                    Identification = "123456789", // Use a valid test customer ID/NIT
                    Dv = "3", // Corresponding DV if applicable
                    Company = "Test Customer Inc.",
                    TradeName = "Test Customer",
                    Names = "Alan Mathison Turing", // Full name
                    Address = "Bletchley Park, Milton Keynes",
                    Email = $"validate.customer.{DateTime.Now:HHmmssfff}@example.com", // Unique email
                    Phone = "01908640404",
                    LegalOrganizationId = "2", // Persona Natural (adjust if different)
                    TributeId = "21", // No aplica / ZZ (or a valid one for your setup)
                    IdentificationDocumentId = "3", // Cédula de Ciudadanía (adjust if different)
                    MunicipalityId = "980" // San Gil (ensure this is a valid municipality ID)
                },
                Items = new List<ItemDto>
                {
                    new ItemDto
                    {
                        CodeReference = "PRODVAL001",
                        Name = "Validated Product Alpha",
                        Quantity = 1,
                        DiscountRate = 0,
                        Price = 12000, 
                        TaxRate = "19.00",
                        UnitMeasureId = 70, // unidad
                        StandardCodeId = 1, // Estándar de adopción del contribuyente
                        IsExcluded = 0,
                        TributeId = 1, // IVA
                        WithholdingTaxes = new List<WithholdingTaxDto>() // Empty or add if needed
                    },
                    new ItemDto
                    {
                        CodeReference = "PRODVAL002",
                        Name = "Validated Product Beta",
                        Quantity = 2,
                        DiscountRate = 10, // 10% discount
                        Price = 7500,  
                        TaxRate = "5.00", // Different tax rate
                        UnitMeasureId = 70,
                        StandardCodeId = 1,
                        IsExcluded = 0,
                        TributeId = 1, // IVA
                        WithholdingTaxes = new List<WithholdingTaxDto>()
                    }
                }
            };

            Integrations.Halltec.Factus.Dtos.ValidationResponse.ValidateBillResponseDto? validationResponse = null;
            try
            {
                // Act
                validationResponse = await _factusService.ValidateBillAsync(billRequest);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exception during ValidateBillAsync: {ex.ToString()}");
                // If the response content is needed on failure, it might be in ex.Data or require specific handling in FactusService
                Assert.Fail($"ValidateBillAsync threw an exception: {ex.Message}");
            }

            // Assert
            Assert.NotNull(validationResponse);
            Assert.Equal("Created", validationResponse.Status, ignoreCase: true); // Or the expected success status
            Assert.False(string.IsNullOrWhiteSpace(validationResponse.Message), "Validation message should not be empty.");
            
            Assert.NotNull(validationResponse.Data);
            var responseData = validationResponse.Data;

            // Company Assertions (check a few key fields)
            Assert.NotNull(responseData.Company);
            Assert.False(string.IsNullOrWhiteSpace(responseData.Company.Nit), "Company NIT should be present.");
            Assert.False(string.IsNullOrWhiteSpace(responseData.Company.CompanyName), "Company name should be present.");

            // Customer Assertions (check data sent in request is reflected)
            Assert.NotNull(responseData.Customer);
            Assert.Equal(billRequest.Customer.Identification, responseData.Customer.Identification);
            Assert.Equal(billRequest.Customer.Names, responseData.Customer.GraphicRepresentationName); // Assuming GraphicRepresentationName maps to Names
            Assert.Equal(billRequest.Customer.Email, responseData.Customer.Email);

            // Bill Assertions
            Assert.NotNull(responseData.Bill);
            Assert.True(responseData.Bill.Id > 0, "Validated bill should have an ID.");
            Assert.False(string.IsNullOrWhiteSpace(responseData.Bill.Number), "Bill number should be generated and present.");
            Assert.False(string.IsNullOrWhiteSpace(responseData.Bill.Cufe), "Bill CUFE should be generated and present.");
            Assert.Equal(billRequest.ReferenceCode, responseData.Bill.ReferenceCode);
            
            // Check if the bill number starts with the expected prefix (if known)
            if (!string.IsNullOrEmpty(responseData.NumberingRange.Prefix) && !string.IsNullOrEmpty(responseData.Bill.Number))
            {
                Assert.StartsWith(responseData.NumberingRange.Prefix, responseData.Bill.Number);
            }

            // Items Assertions
            Assert.NotNull(responseData.Items);
            Assert.Equal(billRequest.Items.Count, responseData.Items.Count);
            // Check a few details of the first item
            if (responseData.Items.Any())
            {
                var firstRequestItem = billRequest.Items.First();
                var firstResponseItem = responseData.Items.First();
                Assert.Equal(firstRequestItem.CodeReference, firstResponseItem.CodeReference);
                Assert.Equal(firstRequestItem.Name, firstResponseItem.Name);
                Assert.Equal(firstRequestItem.Quantity, firstResponseItem.Quantity);
                // Note: Price, discount, tax might be recalculated, so direct equality might not always hold.
                // Assert.Equal(firstRequestItem.Price, firstResponseItem.Price); // This might need adjustment based on API logic
            }

            // Withholding Taxes (Overall) - if any are expected
            Assert.NotNull(responseData.WithholdingTaxes); // Should be a list, even if empty

            System.Diagnostics.Debug.WriteLine($"Bill validation successful: Status='{validationResponse.Status}', Message='{validationResponse.Message}'");
            System.Diagnostics.Debug.WriteLine($"Validated Bill ID: {responseData.Bill.Id}, Number: {responseData.Bill.Number}, CUFE: {responseData.Bill.Cufe?.Substring(0, Math.Min(30, responseData.Bill.Cufe?.Length ?? 0))}...");

            // Log any errors/notifications from the validation process itself (e.g., DIAN rules)
            if (responseData.Bill.Errors != null && responseData.Bill.Errors.Count > 0)
            {
                System.Diagnostics.Debug.WriteLine($"Validation warnings/errors reported in response: {string.Join("; ", responseData.Bill.Errors)}");
                // Depending on test strictness, you might assert this list is empty if no warnings are expected for valid data.
                // Assert.Empty(responseData.Bill.Errors); 
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No validation warnings/errors reported in Data.Bill.Errors list.");
            }
        }
       
    }
}

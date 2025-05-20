using ApiRestClean.Integrations.Halltec.Factus.Dtos;
using ApiRestClean.Integrations.Halltec.Factus.Settings;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiRestClean.Integrations.Halltec.Factus
{
    public class FactusService :  IDisposable
    {
        private readonly HttpClient _httpClient; 
        private readonly FactusApiSettings _settings;
        private bool _disposed = false; 

        public FactusService(
            FactusApiSettings factusApiSettingsOptions)
        {
            _settings = factusApiSettingsOptions;
            _httpClient = new HttpClient(); 

            // Validar configuración esencial
            if (string.IsNullOrWhiteSpace(_settings.BaseUrl))
                throw new ArgumentException("La URL base no puede estar vacía.", nameof(_settings.BaseUrl));
            if (!Uri.TryCreate(_settings.BaseUrl, UriKind.Absolute, out Uri? baseAddressUri))
                throw new ArgumentException("La URL base no es válida o no es una URI absoluta.", nameof(_settings.BaseUrl));
            
            _httpClient.BaseAddress = baseAddressUri;

            // TokenEndpointUrl ahora es una ruta relativa y se elimina de la configuración
            // Ya no se valida _settings.TokenEndpointUrl aquí

            if (string.IsNullOrWhiteSpace(_settings.Email))
                throw new ArgumentException("El Email no puede estar vacío.", nameof(_settings.Email));
            if (string.IsNullOrWhiteSpace(_settings.Password))
                throw new ArgumentException("La Contraseña no puede estar vacía.", nameof(_settings.Password));
            if (string.IsNullOrWhiteSpace(_settings.ClientId))
                throw new ArgumentException("El Client ID no puede estar vacío.", nameof(_settings.ClientId));
            if (string.IsNullOrWhiteSpace(_settings.ClientSecret))
                throw new ArgumentException("El Client Secret no puede estar vacío.", nameof(_settings.ClientSecret));
        }

        public async Task<TokenResponse> GetAuthTokenAsync()
        {
            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", _settings.Email },
                { "password", _settings.Password },
                { "client_id", _settings.ClientId },
                { "client_secret", _settings.ClientSecret }
            };
            
            var content = new FormUrlEncodedContent(requestData);

            var request = new HttpRequestMessage(HttpMethod.Post, "oauth/token") 
            {
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                
                response.EnsureSuccessStatusCode(); 

                string responseBody = await response.Content.ReadAsStringAsync();
                
                System.Diagnostics.Debug.WriteLine($"Respuesta del token: {responseBody}");

                TokenResponse? tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseBody);

                if (tokenResponse == null)
                {
                    System.Diagnostics.Debug.WriteLine("La respuesta del token no pudo ser deserializada o fue nula.");
                    throw new InvalidOperationException("La respuesta del token no pudo ser deserializada correctamente.");
                }

                if (string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    System.Diagnostics.Debug.WriteLine("El 'access_token' en la respuesta deserializada es nulo o vacío.");
                    throw new InvalidOperationException("El token de acceso recibido es nulo o vacío.");
                }

                return tokenResponse;
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine($"Error en la solicitud HTTP al obtener token: {e.Message}");
                throw new HttpRequestException($"Error al intentar obtener el token de autenticación: {e.Message}", e);
            }
            catch (JsonException e)
            {
                System.Diagnostics.Debug.WriteLine($"Error al deserializar la respuesta del token: {e.Message}. Respuesta recibida: {e.Data["responseBody"]?.ToString() ?? "No disponible"}", e); // Intenta loguear el cuerpo si es posible
                throw new JsonException($"La respuesta del token no es un JSON válido o no coincide con el modelo esperado: {e.Message}", e);
            }
            catch (InvalidOperationException e)
            {
                System.Diagnostics.Debug.WriteLine($"Error al procesar la respuesta del token: {e.Message}");
                throw; 
            }
        }

        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new ArgumentException("El token de refresco no puede estar vacío.", nameof(refreshToken));
            }

            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "client_id", _settings.ClientId },
                { "client_secret", _settings.ClientSecret },
                { "refresh_token", refreshToken }
            };

            var content = new FormUrlEncodedContent(requestData);

            var request = new HttpRequestMessage(HttpMethod.Post, "oauth/token") 
            {
                Content = content
            };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                string responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Respuesta del refresh token (raw): {responseBody}");

                response.EnsureSuccessStatusCode();

                TokenResponse? tokenResponse = JsonSerializer.Deserialize<TokenResponse>(responseBody);

                if (tokenResponse == null)
                {
                    System.Diagnostics.Debug.WriteLine("La respuesta del refresh token no pudo ser deserializada o fue nula.");
                    throw new InvalidOperationException("La respuesta del refresh token no pudo ser deserializada correctamente.");
                }

                if (string.IsNullOrEmpty(tokenResponse.AccessToken))
                {
                    System.Diagnostics.Debug.WriteLine("El 'access_token' en la respuesta del refresh token deserializada es nulo o vacío.");
                    throw new InvalidOperationException("El token de acceso (refrescado) recibido es nulo o vacío.");
                }
                
                if (string.IsNullOrEmpty(tokenResponse.RefreshToken))
                {
                    System.Diagnostics.Debug.WriteLine("El 'refresh_token' en la respuesta del refresh token deserializada es nulo o vacío. Esto podría ser inesperado.");
                    
                }

                return tokenResponse;
            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine($"Error en la solicitud HTTP al refrescar token: {e.Message}");
                throw new HttpRequestException($"Error al intentar refrescar el token de autenticación: {e.Message}", e);
            }
            catch (JsonException e)
            {
                // El cuerpo de la respuesta ya se logueó arriba.
                System.Diagnostics.Debug.WriteLine($"Error al deserializar la respuesta del refresh token: {e.Message}.");
                throw new JsonException($"La respuesta del refresh token no es un JSON válido o no coincide con el modelo esperado: {e.Message}", e);
            }
            catch (InvalidOperationException e) // Captura las excepciones lanzadas por nosotros
            {
                System.Diagnostics.Debug.WriteLine($"Error al procesar la respuesta del refresh token: {e.Message}");
                throw;
            }
        }

        public async Task<Dtos.ValidationResponse.ValidateBillResponseDto> ValidateBillAsync(BillRequestDto billRequest)
        {
            if (billRequest == null)
            {
                throw new ArgumentNullException(nameof(billRequest));
            }

            // Obtener el token de autenticación
            TokenResponse tokenResponse = await GetAuthTokenAsync();
            if (string.IsNullOrWhiteSpace(tokenResponse?.AccessToken))
            {
                // GetAuthTokenAsync ya lanza excepciones si el token es nulo/vacío o si hay errores.
                System.Diagnostics.Debug.WriteLine("No se pudo obtener un token de acceso válido para validar la factura.");
                throw new InvalidOperationException("No se pudo obtener un token de acceso válido para validar la factura.");
            }
            string accessToken = tokenResponse.AccessToken;

            string jsonRequestBody = JsonSerializer.Serialize(billRequest, new JsonSerializerOptions
            {
                // Ensure snake_case is used if not handled by JsonPropertyName attributes
                // PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower 
                // However, JsonPropertyName is more explicit and already used in DTOs.
            });
            var content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "v1/bills/validate")
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await _httpClient.SendAsync(request);
                string responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Respuesta de validación de factura (raw): {responseBody}");

                response.EnsureSuccessStatusCode();

                // Deserialize to the new response DTO
                Dtos.ValidationResponse.ValidateBillResponseDto? validationResponse = JsonSerializer.Deserialize<Dtos.ValidationResponse.ValidateBillResponseDto>(responseBody);

                if (validationResponse == null)
                {
                    System.Diagnostics.Debug.WriteLine("La respuesta de validación de factura no pudo ser deserializada o fue nula.");
                    throw new InvalidOperationException("La respuesta de validación de factura no pudo ser deserializada correctamente.");
                }
                
                // Additional checks based on the new structure if necessary, e.g., validationResponse.Data
                if (validationResponse.Data == null && validationResponse.Status != "Created") // Example check
                {
                     System.Diagnostics.Debug.WriteLine($"Validación de factura no exitosa o Data es nula. Status: {validationResponse.Status}, Message: {validationResponse.Message}");
                     // Depending on how you want to handle non-"Created" statuses that are still 2xx
                     // you might throw or just return the response.
                     // For now, we assume EnsureSuccessStatusCode covers critical HTTP errors.
                }

                return validationResponse;
            }
            catch (HttpRequestException e)
            {
                // responseBody (captured above) will contain the error response if EnsureSuccessStatusCode threw.
                // The raw responseBody is already logged just before EnsureSuccessStatusCode.
                // We can include it in the exception message for more context if it's not too large.
                string exceptionMessage = $"Error al intentar validar la factura: {e.Message}";
                // The responseBody is already logged prior to EnsureSuccessStatusCode.
                // If you want to include part of it in the exception:
                // string detail = responseBody; // responseBody is from the outer scope
                // if (!string.IsNullOrEmpty(detail))
                // {
                //    exceptionMessage += $". Respuesta del servidor: {(detail.Length > 500 ? detail.Substring(0, 500) + "..." : detail)}";
                // }
                // For now, the primary log of responseBody is sufficient.
                System.Diagnostics.Debug.WriteLine($"Error en la solicitud HTTP al validar factura: {e.Message}. El cuerpo de la respuesta ya fue logueado.");
                throw new HttpRequestException(exceptionMessage, e, e.StatusCode);
            }
            catch (JsonException e)
            {
                // responseBody is logged above
                System.Diagnostics.Debug.WriteLine($"Error al deserializar la respuesta de validación de factura: {e.Message}.");
                throw new JsonException($"La respuesta de validación de factura no es un JSON válido o no coincide con el modelo esperado: {e.Message}", e);
            }
            catch (InvalidOperationException e) 
            {
                System.Diagnostics.Debug.WriteLine($"Error al procesar la respuesta de validación de factura: {e.Message}");
                throw;
            }
        }

      
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Desechar recursos administrados (como HttpClient)
                    _httpClient?.Dispose();
                }
                // Desechar recursos no administrados (si los hubiera)
                _disposed = true;
            }
        }
    }
}   


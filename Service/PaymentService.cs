using Newtonsoft.Json;
using PaymentPlaceTest.Dto;
using System;
using System.Net.Http;
using System.Net.Mime;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace PaymentPlaceTest.Service
{
    public class PaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiUrl;
        private readonly string _merchantId;
        private readonly string _encryptedData;
        private readonly string _isProduction;
        private readonly string _callbackUrl;

        public PaymentService(IConfiguration configuration)
        {
            _configuration = configuration;
            var apiSettings = _configuration.GetSection("ApiSettings");

            _apiUrl = apiSettings["BaseUrl"];
            _merchantId = apiSettings["MerchantId"];
            _encryptedData = apiSettings["EncryptedData"];
            _isProduction = apiSettings["IsProduction"];
            _callbackUrl = apiSettings["CallbackUrl"];
        }
        public async Task<ResponsDto> InitializePaymentAsync(string amount, string email)
        {
            var requestData = new
            {
                request = new
                {
                    amount = amount,
                    reference = ReferenceGen.GenerateRandomString(),
                    email = email,
                    callback_url = _callbackUrl,
                    user_Production_Credentails = false
                }
            };

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("merchantId", _merchantId);
                client.DefaultRequestHeaders.Add("encrypted-data", _encryptedData);
                client.DefaultRequestHeaders.Add("isProduction", _isProduction);
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var serializedData = JsonConvert.SerializeObject(requestData);
                Console.WriteLine("Serialized Request Data: " + serializedData);
                var jsonRequest = new StringContent(serializedData,encoding:null, "application/json");
               


                var response = await client.PostAsync(_apiUrl, jsonRequest);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the response content into ResponseDto
                    var responseObject = JsonConvert.DeserializeObject<ResponsDto>(responseContent);
                    return responseObject;
                }
                else
                {
                    var messg = response.RequestMessage?.ToString();
                    var d = response.Content.ReadFromJsonAsync<ErrorResponsedto>();
                    
                    var errorResponseContent = await response.Content.ReadAsStringAsync();
                    //var errorDetails = JsonConvert.DeserializeObject<ErrorResponsedto>(errorResponseContent);

                    // If you want to capture the error message in the response
                    var errorMessage = $"Payment initialization failed with status code: {(int)response.StatusCode}. Error message: {errorResponseContent}";

                    // Throw an exception with the detailed error message
                    throw new Exception(errorMessage);
                }
            }
        }
    }
}

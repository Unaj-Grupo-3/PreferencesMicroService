using Application.Interfaces;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.UseCases
{
    public class UserApiService : IUserApiService
    {
        private string? _message;
        private string _response;
        private int _statusCode;
        private string _url;
        private HttpClient _httpClient;

        public UserApiService()
        {
            _url = "https://localhost:7020/api/v1/User";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            _httpClient = new HttpClient(handler);
        }

        public async Task<bool> ValidateUser(int UserId)
        {
            try
            {
                var content = JsonContent.Create(UserId);
                var responseApi = await _httpClient.GetAsync(_url + "/" + UserId);
                var responseStatusCode = responseApi.IsSuccessStatusCode;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseContent = await responseApi.Content.ReadAsStringAsync();
                    var responseObject = JsonDocument.Parse(responseContent).RootElement;
                    _message = "Se validó el usuario correctamente";
                    _response = responseObject.ToString();
                    _statusCode = 200;
                }
                else
                {
                    _message = "El usuario no existe";
                    _statusCode = (int)responseApi.StatusCode;
                }

                return responseApi.IsSuccessStatusCode;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _message = "Error en el microservicio de usuarios";
                _statusCode = 500;
                return false;
            }            
        }

        public string GetMessage()
        {
            return _message;
        }

        public JsonDocument GetResponse()
        {
            if (_response == null)
            {
                return JsonDocument.Parse("{}");
            }

            return JsonDocument.Parse(_response);
        }

        public int GetStatusCode()
        {
            return _statusCode;
        }
    }
}

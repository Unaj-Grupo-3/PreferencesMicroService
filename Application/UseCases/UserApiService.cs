using Application.Interfaces;
using Application.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Application.UseCases
{
    public class UserApiService : IUserApiService
    {
        private string? _message;
        private string _response;
        private int _statusCode;
        private string _urlUser;
        private string _urlGender;
        private HttpClient _httpClient;

        public UserApiService()
        {
            _urlUser = "https://localhost:7020/api/v1/User";
            _urlGender = "https://localhost:7020/api/v1/Gender";
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            _httpClient = new HttpClient(handler);
        }

        public async Task<bool> ValidateUser(int userId)
        {
            try
            {
                var content = JsonContent.Create(userId);
                //_httpClient.DefaultRequestHeaders.Authorization
                //         = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxIiwiQXV0aElkIjoiMjk0MGI2MDgtYzVlYS00ODE3LTFmMTctMDhkYjQ5ZDhlMzgxIiwiZXhwIjoxNjgyOTA1NDg0fQ.31tXsuFtAoMCSl0NmZxS2DJJv04I0U0U-ZCMY8Xh6dSt6D923fDRJ7003w-7rbdqkxflFG1LGAE8qMmemIgXRg");
                var responseApi = await _httpClient.GetAsync(_urlUser);
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

                return true;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _message = "Error en el microservicio de usuarios";
                _statusCode = 500;
                return false;
            }            
        }

        public async Task<int> ValidateUserToken(string token)
        {
            int userId = 0;
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseApi = await _httpClient.GetAsync(_urlUser);
                var responseStatusCode = responseApi.IsSuccessStatusCode;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseContent = await responseApi.Content.ReadAsStringAsync();
                    var responseObject = JsonDocument.Parse(responseContent).RootElement;
                    var responseJson = JsonConvert.DeserializeObject<UserPreferencesResponse>(responseContent);
                    userId = responseJson.UserId;
                    _message = "Se validó el usuario correctamente";
                    _response = responseObject.ToString();
                    _statusCode = 200;
                }
                else
                {
                    _message = "El usuario no existe";
                    _statusCode = (int)responseApi.StatusCode;
                }

                return userId;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _message = "Error en el microservicio de usuarios";
                _statusCode = 500;
                return userId;
            }
        }

        public async Task<IEnumerable<GenderResponse>> GetAllGenders()
        {
            try
            {
                var responseApi = await _httpClient.GetAsync(_urlGender);
                var responseStatusCode = responseApi.IsSuccessStatusCode;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseContent = await responseApi.Content.ReadAsStringAsync();
                    var responseObject = JsonDocument.Parse(responseContent).RootElement;
                    var responseJson = JsonConvert.DeserializeObject<List<GenderResponse>>(responseContent);
                    _message = "Consulta de géneros realizada correctamente";
                    _response = responseJson.ToString();
                    _statusCode = 200;
                    return responseJson;
                }
                else
                {
                    _message = "No se pudo obtener la lista de géneros";
                    _statusCode = (int)responseApi.StatusCode;
                    return new List<GenderResponse>();
                }


            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _message = "Error en el microservicio de usuarios";
                _statusCode = 500;
                return new List<GenderResponse>();
            }
        }

        public async Task<bool> ValidateGender(int genderId)
        {
            try
            {
                var content = JsonContent.Create(genderId);
                var responseApi = await _httpClient.GetAsync(_urlGender + "/" + genderId);
                var responseStatusCode = responseApi.IsSuccessStatusCode;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseContent = await responseApi.Content.ReadAsStringAsync();
                    var responseObject = JsonDocument.Parse(responseContent).RootElement;
                    _message = "Se validó el género correctamente";
                    _response = responseObject.ToString();
                    _statusCode = 200;
                }
                else
                {
                    _message = "El género no existe";
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

        public async Task<GenderResponse> GetGenderById(int genderId)
        {
            try
            {
                var content = JsonContent.Create(genderId);
                var responseApi = await _httpClient.GetAsync(_urlGender + "/" + genderId);
                var responseStatusCode = responseApi.IsSuccessStatusCode;

                if (responseApi.IsSuccessStatusCode)
                {
                    var responseContent = await responseApi.Content.ReadAsStringAsync();
                    var responseObject = JsonDocument.Parse(responseContent).RootElement;
                    var responseJson = JsonConvert.DeserializeObject<GenderResponse>(responseContent);
                    _message = "Se obtuvo el género correctamente";
                    _response = responseObject.ToString();
                    _statusCode = 200;

                    return responseJson;
                }

                return null;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                _message = "Error en el microservicio de usuarios";
                _statusCode = 500;
                return null;
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

using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenderPreferenceController : ControllerBase
    {
        private readonly IUserApiService _userService;
        private readonly IGenderPreferenceService _service;
        private readonly IConfiguration _configuration;

        public GenderPreferenceController(IUserApiService userService, IGenderPreferenceService service, IConfiguration configuration)
        {
            _userService = userService;
            _service = service;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllByToken()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = int.Parse(identity.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var message = _userService.GetMessage();
                var statusCode = _userService.GetStatusCode();
                var urluser = _configuration.GetSection("urluser").Get<string>();

                if (userId > 0)
                {
                    var response = await _service.GetAllByUserId(urluser, userId);
                    return Ok(response);
                }
                return new JsonResult(new { Message = message }) { StatusCode = statusCode };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetAllByUserId(int UserId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, UserId, token);
                var message = _userService.GetMessage();
                var statusCode = _userService.GetStatusCode();

                if (userValid)
                {
                    var response = await _service.GetAllByUserId(urluser, UserId);
                    return Ok(response);
                }
                return new JsonResult(new { Message = message }) { StatusCode = statusCode };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(GenderPreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var urluser = _configuration.GetSection("urluser").Get<string>();                
                bool userValid = await _userService.ValidateUser(urluser, request.UserId, token);
                if (userValid)
                {
                    var response = await _service.Insert(urluser, request);

                    if (response == null)
                    {
                        return new JsonResult(new { Message = "Se produjo un error al insertar la preferencia", Response = response }) { StatusCode = 400 };
                    }
                    return new JsonResult(new { Message = "Se ha actualizado la preferencia exitosamente.", Response = response }) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(new { Message = "Usuario inexistente" }) { StatusCode = 404 };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(GenderPreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, request.UserId, token);                
                if (userValid)
                {
                    var response = await _service.Delete(urluser, request);

                    if (response == null)
                    {
                        return new JsonResult(new { Message = "Se produjo un error al eliminar la preferencia. El género seleccionado no existe", Response = response }) { StatusCode = 400 };
                    }
                    return new JsonResult(new { Message = "Se ha eliminado la preferencia exitosamente.", Response = response }) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(new { Message = "Usuario inexistente" }) { StatusCode = 404 };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }
    }
}

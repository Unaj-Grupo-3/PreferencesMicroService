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
        private readonly ITokenServices _tokenServices;

        public GenderPreferenceController(IUserApiService userService, IGenderPreferenceService service, IConfiguration configuration, ITokenServices tokenServices)
        {
            _userService = userService;
            _service = service;
            _configuration = configuration;
            _tokenServices = tokenServices;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllByToken()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Insert(GenderPreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, token);


                if (userValid)
                {
                    var response = await _service.Insert(urluser, request, userId);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(GenderPreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId  = _tokenServices.GetUserId(identity);
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, token); 
                
                if (userValid)
                {
                    var response = await _service.Delete(urluser, request, userId);

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

        // A este endpoint solo lo dejamos para ver las preferencias de genero del propio usuario
        //[HttpGet("{UserId}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //public async Task<IActionResult> GetAllByUserId(int UserId)
        //{
        //    try
        //    {
        //        var identity = HttpContext.User.Identity as ClaimsIdentity;
        //        var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
        //        var urluser = _configuration.GetSection("urluser").Get<string>();
        //        bool userValid = await _userService.ValidateUser(urluser, token);
        //        var message = _userService.GetMessage();
        //        var statusCode = _userService.GetStatusCode();

        //        if (userValid)
        //        {
        //            var response = await _service.GetAllByUserId(urluser, UserId);
        //            return Ok(response);
        //        }
        //        return new JsonResult(new { Message = message }) { StatusCode = statusCode };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
        //    }
        //}
    }
}

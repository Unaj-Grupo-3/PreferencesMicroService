using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly IUserApiService _userService;
        private readonly IPreferenceService _service;
        private readonly IConfiguration _configuration;
        private readonly ITokenServices _tokenServices;

        public PreferenceController(IUserApiService userService, IPreferenceService service, IConfiguration configuration, ITokenServices tokenServices)
        {
            _userService = userService;
            _service = service;
            _configuration = configuration;
            _tokenServices = tokenServices;
        }


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllByUserId()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);

                var response = await _service.GetAllByUserId(userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Insert(PreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                int userId = _tokenServices.GetUserId(identity);

                var response = await _service.Insert(request, userId);

                if (response == null)
                {
                    return new JsonResult(new { Message = "Se produjo un error. La preferencia ya fue ingresada o el interés seleccionado no existe", Response = response }) { StatusCode = 400 };
                }
                return new JsonResult(new { Message = "Se ha actualizado la preferencia exitosamente.", Response = response }) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(PreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                int userId = _tokenServices.GetUserId(identity);

                var response = await _service.Update(request, userId);
                if (response != null)
                {
                    return new JsonResult(new { Message = "Se ha actualizado el interés exitosamente.", Response = response }) { StatusCode = 200 };
                }
                else
                {
                    return new JsonResult(new { Message = "La preferencia ingresada no existe" }) { StatusCode = 400 };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(PreferenceReqId request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);

                var response = await _service.Delete(request, userId);

                if (response != null)
                {
                    return Ok(new { Message = "Se ha eliminado la preferencia exitosamente.", Response = response });
                }

                return new JsonResult(new { Message = "No se encuentra la preferencia ingresada" }) { StatusCode = 404 };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
        }

        // Quizas no es necesario
        //[HttpGet("All")]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        //await _userService.GetAllGenders();
        //        var response = await _service.GetAll();
        //        return Ok(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
        //    }
        //}

    }
}

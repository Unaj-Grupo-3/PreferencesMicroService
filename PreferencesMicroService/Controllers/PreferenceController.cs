using Application.Interfaces;
using Application.Models;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly IUserApiService _userService;
        private readonly IPreferenceService _service;

        public PreferenceController(IUserApiService userService, IPreferenceService service)
        {
            _userService = userService;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                //await _userService.GetAllGenders();
                var response = await _service.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetAllByUserId(Guid UserId)
        {
            try
            {
                bool userValid = await _userService.ValidateUser(UserId);
                var message = _userService.GetMessage();
                var statusCode = _userService.GetStatusCode();

                if (userValid)
                {
                    var response = await _service.GetAllByUserId(UserId);
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
        public async Task<IActionResult> Insert(PreferenceReq request)
        {
            try
            {
                bool userValid = await _userService.ValidateUser(request.UserId);
                if (userValid)
                {
                    var response = await _service.Insert(request);

                    if (response == null)
                    {
                        return new JsonResult(new { Message = "Se produjo un error. La preferencia ya fue ingresada o el interés seleccionado no existe", Response = response }) { StatusCode = 400 };
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

        [HttpPut]
        public async Task<IActionResult> Update(PreferenceReq request)
        {
            try
            {                
                var response = await _service.Update(request);
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
        public async Task<IActionResult> Delete(PreferenceReqId request)
        {
            try
            {
                var response = await _service.Delete(request);

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
    }
}

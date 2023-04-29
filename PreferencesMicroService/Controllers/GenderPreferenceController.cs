using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GenderPreferenceController : ControllerBase
    {
        private readonly IUserApiService _userService;
        private readonly IGenderPreferenceService _service;

        public GenderPreferenceController(IUserApiService userService, IGenderPreferenceService service)
        {
            _userService = userService;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(GenderPreferenceReq request)
        {
            try
            {
                bool userValid = await _userService.ValidateUser(request.UserId);
                if (userValid)
                {
                    var response = await _service.Insert(request);

                    if (response == null)
                    {
                        return new JsonResult(new { Message = "Se produjo un error al insertar la preferencia. El interés seleccionado no existe", Response = response }) { StatusCode = 400 };
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
    }
}

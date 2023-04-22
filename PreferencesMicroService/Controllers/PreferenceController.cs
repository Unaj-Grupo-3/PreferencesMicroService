using Application.Interfaces;
using Application.Models;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            bool userValid = await _userService.ValidateUser(id);
            if (!userValid)
            {
                return new JsonResult(new { Message = _userService.GetMessage(), Response = _userService.GetResponse() }) { StatusCode = _userService.GetStatusCode() };
            }

            return new JsonResult(new { Message = "Se ha validado el usuario exitosamente.", Response = _userService.GetResponse() }) { StatusCode = 201 };
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
                        return new JsonResult(new { Message = "Se produjo un error al insertar la preferencia. El interés seleccionado no existe", Response = response }) { StatusCode = 400 };
                    }
                    return new JsonResult(new { Message = "Se ha creado la preferencia exitosamente.", Response = response }) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(new { Message = "Usuario inexistente" }) { StatusCode = 404 };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
        }
    }
}

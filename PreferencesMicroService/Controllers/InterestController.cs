using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IInterestService _service;

        public InterestController(IInterestService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InterestReq request)
        {
            try
            {
                if (request.Description != "")
                {
                    var response = await _service.Insert(request);
                    if (response != null)
                    {
                        return new JsonResult(new { Message = "Se ha creado el interés exitosamente.", Response = response }) { StatusCode = 201 };
                    }
                    else
                    {
                        return new JsonResult(new { Message = "La categoría ingresada no existe" }) { StatusCode = 400 };
                    }
                }
                else
                {
                    return new JsonResult(new { Message = "La descripción no puede estar vacía" }) { StatusCode = 400 };
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
        }
    }
}

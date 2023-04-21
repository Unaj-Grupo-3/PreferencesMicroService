using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InterestCategoryController : ControllerBase
    {
        private readonly IInterestCategoryService _service;

        public InterestCategoryController(IInterestCategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(InterestCategoryReq request)
        {
            try
            {
                if (request.Description != "")
                {
                    var response = await _service.Insert(request);
                    return new JsonResult(new { Message = "Se ha creado la categoria exitosamente.", Response = response }) { StatusCode = 201 };
                }
                else
                {
                    return new JsonResult(new { Message = "La descripcion no puede estar vacía" }) { StatusCode = 400 };
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

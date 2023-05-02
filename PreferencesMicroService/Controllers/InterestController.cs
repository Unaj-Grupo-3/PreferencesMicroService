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

        [HttpGet("{InterestCategoryId}")]
        public async Task<IActionResult> GetAllByCategory(int InterestCategoryId)
        {
            try
            {
                var response = await _service.GetAllByCategory(InterestCategoryId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(InterestReq request, int id)
        {
            try
            {
                if (request.Description != "")
                {
                    var response = await _service.Update(request, id);
                    if (response != null)
                    {
                        return new JsonResult(new { Message = "Se ha actualizado el interés exitosamente.", Response = response }) { StatusCode = 200 };
                    }
                    else
                    {
                        return new JsonResult(new { Message = "El interés o categoría ingresada no existen" }) { StatusCode = 400 };
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _service.Delete(id);

                if (response != null)
                {
                    return Ok(new { Message = "Se ha eliminado el interés exitosamente.", Response = response });
                }

                return new JsonResult(new { Message = "No se encuentra el interés" }) { StatusCode = 404 };
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor." }) { StatusCode = 500 };
            }
        }
    }
}

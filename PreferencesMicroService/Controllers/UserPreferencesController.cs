using Application.Interfaces;
using Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUserPreferencesService _userPreferencesService;
        private readonly IConfiguration _configuration;

        public UserPreferencesController(IUserPreferencesService userPreferencesService, IConfiguration configuration)
        {
            _userPreferencesService = userPreferencesService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var urluser = _configuration.GetSection("urluser").Get<string>();
                var response = await _userPreferencesService.GetAll(urluser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetByUserId(int UserId)
        {
            try
            {

                var urluser = _configuration.GetSection("urluser").Get<string>();
                var response = await _userPreferencesService.GetByUserId(urluser, UserId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }
    }
}

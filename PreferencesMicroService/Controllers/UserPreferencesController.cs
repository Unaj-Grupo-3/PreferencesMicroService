using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreferencesMicroService.Authorization;
using System.Security.Claims;

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
        [Authorize(AuthenticationSchemes = ApiKeySchemeOptions.Scheme)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var apikey = _configuration.GetSection("ApiKey").Get<string>();
                var key = HttpContext.User.Identity.Name;

                if (key != null && key != apikey)
                {
                    return new JsonResult(new { Message = "No se puede acceder. La Key es inválida" }) { StatusCode = 401 };
                }
                var urluser = _configuration.GetSection("urluser").Get<string>();
                var response = await _userPreferencesService.GetAll(urluser);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpGet("Ids")]
        [Authorize(AuthenticationSchemes = ApiKeySchemeOptions.Scheme)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetUserByListId([FromQuery] List<int> userIds, bool fullResponse)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var apikey = _configuration.GetSection("ApiKey").Get<string>();
                var key = HttpContext.User.Identity.Name;

                if (key != null && key != apikey)
                {
                    return new JsonResult(new { Message = "No se puede acceder. La Key es inválida" }) { StatusCode = 401 };
                }

                if (fullResponse)
                {
                    var urluser = _configuration.GetSection("urluser").Get<string>();
                    var response = await _userPreferencesService.GetFullByListId(urluser, userIds);
                    return Ok(response);
                }
                else
                {
                    var urluser = _configuration.GetSection("urluser").Get<string>();
                    var response = await _userPreferencesService.GetSimpleByListId(urluser, userIds);
                    return Ok(response);
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }
    }
}

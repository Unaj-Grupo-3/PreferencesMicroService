using Application.Interfaces;
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

        [HttpGet("Ids")]
        public async Task<IActionResult> GetUserByListId([FromQuery] List<int> userIds, bool fullResponse)
        {
            try
            {
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

﻿using Application.Interfaces;
using Application.Models;
using Azure.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace PreferencesMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OverallPreferenceController : ControllerBase
    {
        private readonly IUserApiService _userService;
        private readonly IOverallPreferenceService _service;
        private readonly IConfiguration _configuration;
        private readonly ITokenServices _tokenServices;

        public OverallPreferenceController(IUserApiService userService, IOverallPreferenceService service, IConfiguration configuration, ITokenServices tokenServices)
        {
            _userService = userService;
            _service = service;
            _configuration = configuration;
            _tokenServices = tokenServices;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetByToken()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, token);
                var message = _userService.GetMessage();
                var statusCode = _userService.GetStatusCode();

                if (userValid)
                {
                    var response = await _service.GetByUserId(userId);

                    if (response == null)
                    {
                        return new JsonResult(new { Message = "La preferencia ingresada no existe", Response = new { } }) { StatusCode = 200 };
                    }

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Insert(OverallPreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);
                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, token);

                if (userValid)
                {
                    var response = await _service.Insert(request, userId);

                    if (response == null)
                    {
                        return new JsonResult(new { Message = "Se produjo un error. La preferencia ya fue ingresada previamente", Response = response }) { StatusCode = 400 };
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
                return new JsonResult(new { Message = "Se ha producido un error interno en el servidor. " + ex.Message }) { StatusCode = 500 };
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(OverallPreferenceReq request)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);

                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, token);

                if (!userValid)
                {
                    return new JsonResult(new { Message = "Usuario inexistente" }) { StatusCode = 404 };
                }

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
        public async Task<IActionResult> Delete()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int userId = _tokenServices.GetUserId(identity);

                var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
                var urluser = _configuration.GetSection("urluser").Get<string>();
                bool userValid = await _userService.ValidateUser(urluser, token);

                if (!userValid)
                {
                    return new JsonResult(new { Message = "Usuario inexistente" }) { StatusCode = 404 };
                }

                var response = await _service.Delete(userId);

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

        // No hace falta un getAll de overall
        //[HttpGet("All")]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
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

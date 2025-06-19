using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Models.Dtos;
using ApiTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;
        public AuthController(IUsuarioService userService)
        {
            _usuarioService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logDto)
        {
            try
            {
                var result = await _usuarioService.Login(logDto);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result.Data);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }
    }

}


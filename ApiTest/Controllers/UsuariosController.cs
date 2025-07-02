using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Models.Dtos;
using ApiTest.Services.Implementaciones;
using ApiTest.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _UsuarioService;

        public UsuariosController(IUsuarioService UsuarioService)
        {
            _UsuarioService = UsuarioService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Guardar([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.InsertUser(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("getUser")]
        public async Task<IActionResult> Obtener([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.GetUser(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Eliminar([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.DeleteUser(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Modificar([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.UpdateUser(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _UsuarioService.GetUsers();

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }
        [HttpPost]
        [Route("sendCode")]
        public async Task<IActionResult> RecoverPsw([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.RecoverPsw(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpPost]
        [Route("validCode")]
        public async Task<IActionResult> CodeValid([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.CodeValid(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
                }
                else
                    return BadRequest(result.Msg);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpPost]
        [Route("resetPsw")]
        public async Task<IActionResult> ResetPsw([FromBody] UsuarioDto user)
        {
            try
            {
                var result = await _UsuarioService.ResetPsw(user);

                if (result.Status == HttpStatusCode.OK.ToString())
                {
                    return StatusCode(StatusCodes.Status200OK, result);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTest.Models;
using ApiTest.Commons;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using ApiTest.Services.Interfaces;

namespace ApiTest.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]    
    public class ProductoController : ControllerBase
    {
        public readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {            

            try
            {
                var lista = await _productoService.ListarProductos();
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse("ok", lista));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto}")]
        public async Task<IActionResult> Obtener(int idProducto)
        {
           
            try
            {
                var producto = await _productoService.ObtenerProducto(idProducto);

                if (producto == null)
                {
                    return BadRequest("Producto no encontrado");
                }

                return StatusCode(StatusCodes.Status200OK, new DefaultResponse("ok", producto));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Producto prod)
        {
            try
            {
                await _productoService.GuardarProducto(prod);

                return StatusCode(StatusCodes.Status200OK, new DefaultResponse("ok", prod));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new DefaultResponse(ex.Message));
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<DataResponseDto<Producto>> Editar([FromBody] Producto prod)
        {         
            try
            {
                var result = await _productoService.EditarProducto(prod);

                return result;
            }
            catch (Exception ex)
            {
                return new DataResponseDto<Producto>()
                {
                    Data = null,
                    Msg = ex.Message,
                    Status = StatusCodes.Status400BadRequest.ToString(),
                };
            }
        }

        [HttpDelete]
        [Route("Eliminar/{idProducto}")]
        public async Task<DataResponseDto<Producto>> Eliminar(int idProducto)
        {          

            try
            {
                var result = await _productoService.EliminarProducto(idProducto);

                return result;

            }
            catch (Exception ex)
            {
                return new DataResponseDto<Producto>()
                {
                    Data = null,
                    Msg = ex.Message,
                    Status = StatusCodes.Status400BadRequest.ToString(),
                };
            }
        }




    }
}

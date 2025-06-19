using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services.Interfaces;

namespace ApiTest.Services.Implementaciones
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repo;

        public ProductoService(IProductoRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Producto>> ListarProductos() => await _repo.ListarProductos();

        public async Task<Producto?> ObtenerProducto(int id) => await _repo.ObtenerProducto(id);

        public async Task GuardarProducto(Producto prod) => await _repo.GuardarProducto(prod);

        public async Task<DataResponseDto<Producto>> EditarProducto(Producto prod)
        {
            try
            {
                //comment de prueba
                var producto = await _repo.ObtenerProducto(prod.IdProducto);

                if (producto == null)
                {
                    return new DataResponseDto<Producto>()
                    {
                        Status = StatusCodes.Status404NotFound.ToString(),
                        Msg = "Producto no encontrado",
                        Data = null,
                    };
                }

                producto.CodigoBarra = prod.CodigoBarra ?? producto.CodigoBarra;
                producto.Descripcion = prod.Descripcion ?? producto.Descripcion;
                producto.Marca = prod.Marca ?? producto.Marca;
                producto.IdCategoria = prod.IdCategoria ?? producto.IdCategoria;
                producto.Precio = prod.Precio ?? producto.Precio;

                await _repo.GuardarProductoAsync();

                return new DataResponseDto<Producto>()
                {
                    Status = StatusCodes.Status200OK.ToString(),
                    Msg = "Producto Editado con Éxito",
                    Data = producto,
                };

            }
            catch (Exception ex) 
            {
                return new DataResponseDto<Producto>()
                {
                    Status = StatusCodes.Status400BadRequest.ToString(),
                    Msg = ex.ToString(),
                    Data = null,
                };
            }
           
        }

        public async Task<DataResponseDto<Producto>> EliminarProducto(int id) 
        {
            try
            {
                var producto = await _repo.ObtenerProducto(id);

                if (producto == null)
                {
                    return new DataResponseDto<Producto>()
                    {
                        Status = StatusCodes.Status404NotFound.ToString(),
                        Msg = "Producto no encontrado",
                        Data = null,
                    };
                }

                await _repo.EliminarProducto(producto);

                return new DataResponseDto<Producto>()
                {
                    Status = StatusCodes.Status200OK.ToString(),
                    Msg = "Producto Eliminado con Éxito",
                    Data = producto,
                };         
                

            }
            catch (Exception ex)
            {
                return new DataResponseDto<Producto>()
                {
                    Status = StatusCodes.Status400BadRequest.ToString(),
                    Msg = ex.ToString(),
                    Data = null,
                };
            }
        }
    }
}

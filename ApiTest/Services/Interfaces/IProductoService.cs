using ApiTest.Commons;
using ApiTest.Models;

namespace ApiTest.Services.Interfaces
{
    public interface IProductoService
    {
        Task<List<Producto>> ListarProductos();
        Task<Producto?> ObtenerProducto(int id);
        Task GuardarProducto(Producto prod);
        Task<DataResponseDto<Producto>> EditarProducto(Producto prod);
        Task<DataResponseDto<Producto>> EliminarProducto(int id);
    }
}

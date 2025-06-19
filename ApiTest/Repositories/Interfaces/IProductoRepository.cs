using ApiTest.Models;

namespace ApiTest.Repositories.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> ListarProductos();
        Task<Producto?> ObtenerProducto(int id);
        Task GuardarProducto(Producto prod);        
        Task EliminarProducto(Producto prod);
        Task GuardarProductoAsync();
    }
}

using ApiTest.Models;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repositories.Implementaciones
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly TESTAPIContext _context;

        public ProductoRepository(TESTAPIContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> ListarProductos()
        {
            return await _context.Productos.Include(p => p.oCategoria).ToListAsync();
        }

        public async Task<Producto?> ObtenerProducto(int id)
        {
            return await _context.Productos.Include(p => p.oCategoria).FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task GuardarProducto(Producto prod)
        {
            _context.Productos.Add(prod);
            await _context.SaveChangesAsync();
        }

        public async Task GuardarProductoAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task EliminarProducto(Producto prod)
        {           
            _context.Productos.Remove(prod);
            await _context.SaveChangesAsync();
        }

    }
}

using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Models.Dtos;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repositories.Implementaciones
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly TESTAPIContext _context;

        public UsuarioRepository(TESTAPIContext context)
        {
            _context = context;
        }

        public async Task InsertUser(Usuario usuario) 
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> GetUser(string user)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(p =>  p.User == user);
        }

        public async Task GuardarUsuarioAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(Usuario user)
        {
            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
        }

    }
}

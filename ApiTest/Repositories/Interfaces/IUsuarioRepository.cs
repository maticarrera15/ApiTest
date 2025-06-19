using ApiTest.Models;

namespace ApiTest.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task InsertUser(Usuario user);

        Task<Usuario> GetUser(string? user);

        Task GuardarUsuarioAsync();

        Task EliminarUsuario(Usuario user);
    }
}

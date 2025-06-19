using ApiTest.Models;

namespace ApiTest.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerarToken(Usuario? user);
    }
}

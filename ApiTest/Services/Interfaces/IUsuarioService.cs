using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Models.Dtos;

namespace ApiTest.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<DataResponseDto<Usuario>> UpdateUser(UsuarioDto user);

        Task<DataResponseDto<Usuario>> GetUser(UsuarioDto user);

        Task<DataResponseDto<Usuario>> InsertUser(UsuarioDto user);

        Task<DataResponseDto<string>> Login(LoginDto logDto);

        Task<DataResponseDto<Usuario>> DeleteUser(UsuarioDto user);

        Task<DataResponseDto<List<Usuario>>> GetUsers();

        Task<DataResponseDto<Usuario>> RecoverPsw(UsuarioDto userDto);


    }
}

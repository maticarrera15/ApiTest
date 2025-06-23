using ApiTest.Commons;
using ApiTest.Models;
using ApiTest.Models.Dtos;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services.Interfaces;
using System.Net;

namespace ApiTest.Services.Implementaciones
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _userRepo;
        private readonly IJwtService _jwtService;

        public UsuarioService(IUsuarioRepository userRepo, IJwtService jwtService) 
        {
            _userRepo = userRepo;       
            _jwtService = jwtService;
        }

        public async Task<DataResponseDto<Usuario>> GetUser(UsuarioDto userDto) 
        {
            var usuario = new Usuario
            {
                Email = userDto.Email,
                User = userDto.Usuario
            };

            var user = await _userRepo.GetUser(usuario.User);

            if (user == null) 
            {
                return new DataResponseDto<Usuario>
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Msg = "Usuario no existe en base",
                    Data = user,
                    exist = false,
                };
            }

            return new DataResponseDto<Usuario>
            {
                Status = HttpStatusCode.OK.ToString(),
                Msg = "Usuario existente en base.",
                Data = user,
                exist = true,
            };
        }


        public async Task<DataResponseDto<Usuario>> InsertUser(UsuarioDto user)
        {

            var resp = await this.GetUser(user);

            if (resp.exist == true)
            {
                return resp;
            }

            var usuario = new Usuario
            {
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                User = user.Usuario,
                Documento = Convert.ToInt32(user.Documento),
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                FechaAlta = DateTime.Now
            };

            await _userRepo.InsertUser(usuario);

            return new DataResponseDto<Usuario>
            {
                Status = HttpStatusCode.OK.ToString(),
                Msg = "Usuario registrado con éxito",
                Data = usuario
            };
        }

        public async Task<DataResponseDto<string>> Login(LoginDto logDto)
        {
            var usuario = new UsuarioDto
            {
                Usuario = logDto.Usuario
            };

            var user = await this.GetUser(usuario);

            if (user.Data == null)
            {
                return new DataResponseDto<string>
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Msg = "Usuario incorrecto",
                };
            }

            bool isPswValid = BCrypt.Net.BCrypt.Verify(logDto.Password, user.Data?.Password);

            if (!isPswValid)
            {
                return new DataResponseDto<string>
                {
                    Status = HttpStatusCode.Conflict.ToString(),
                    Msg = "Contraseña incorrecta",
                };
            }

            string token = _jwtService.GenerarToken(user.Data);

            return new DataResponseDto<string>
            {
                Status = HttpStatusCode.OK.ToString(),
                Msg = "Login exitoso",
                Data = token
            };

        }

        public async Task<DataResponseDto<Usuario>> DeleteUser(UsuarioDto user)
        {
            var usuario = await this.GetUser(user);

            if (usuario.exist == false)
            {
                return new DataResponseDto<Usuario>()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Msg = "Usuario No existe.",
                    Data = null,
                };
            }

            await _userRepo.EliminarUsuario(usuario.Data!);

            return new DataResponseDto<Usuario>()
            {
                Status = HttpStatusCode.OK.ToString(),
                Msg = "Usuario Eliminado con Éxito",
                Data = usuario.Data,
            };



        }

        public async Task<DataResponseDto<Usuario>> UpdateUser(UsuarioDto userDto)
        {
            var user = await this.GetUser(userDto);

            if (user.exist == false)
            {
                return new DataResponseDto<Usuario>()
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Msg = "Usuario No existe.",
                    Data = null,
                };
            }

            user.Data!.Nombre = userDto.Nombre ?? user.Data?.Nombre;
            user.Data!.Apellido = userDto.Apellido ?? user.Data?.Apellido;
            user.Data!.User = userDto.Usuario ?? user.Data?.User;
            user.Data!.Documento = userDto.Documento ?? user.Data?.Documento;
            user.Data!.Email = userDto.Email ?? user.Data?.Email;

            await _userRepo.GuardarUsuarioAsync();

            return new DataResponseDto<Usuario>()
            {
                Status = HttpStatusCode.OK.ToString(),
                Msg = "Usuario Editado con Éxito",
                Data = user.Data,
            };

        }

        public async Task<DataResponseDto<List<Usuario>>> GetUsers()
        {
            var resp = await _userRepo.GetUsers();

            return new DataResponseDto<List<Usuario>>
            {
                Status = HttpStatusCode.OK.ToString(),
                Msg = "Listado de usuarios",
                Data = resp,
                exist = true,
            };
        }
    }
}

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
        private readonly IEmailService _emailService;

        public UsuarioService(IUsuarioRepository userRepo, IJwtService jwtService, IEmailService emailService) 
        {
            _userRepo = userRepo;       
            _jwtService = jwtService;
            _emailService = emailService;
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

        public async Task<DataResponseDto<Usuario>> RecoverPsw(UsuarioDto userDto)
        {
            var resp = await this.GetUser(userDto);

            if (resp.Data == null)
            {
                return new DataResponseDto<Usuario>
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Msg = "Usuario Incorrecto",
                    exist = false,
                };
            }

            else 
            {
                var codigo = new Random().Next(100000, 999999).ToString();
                var fechaLimite = DateTime.Now.AddHours(1);

                resp.Data.Token = codigo;
                resp.Data.DateCodeLimit = fechaLimite;
                await _userRepo.GuardarUsuarioAsync();


                await _emailService.SendEmailCodePsw(resp.Data.Email, codigo);

                return new DataResponseDto<Usuario>
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Msg = "Se ha enviado un código a tu email",
                    exist = true,
                    Data = resp.Data,
                };
            }            
            
        }

        public async Task<DataResponseDto<Usuario>> CodeValid(UsuarioDto userDto)
        {
            var resp = await this.GetUser(userDto);

            if (resp.Data == null)
            {
                return resp;                
            }

            else if (resp.Data.DateCodeLimit != null && resp.Data.DateCodeLimit >= DateTime.Now)
            {
                if (resp.Data.Token == userDto.codeValidation)
                {
                    return new DataResponseDto<Usuario>
                    {
                        Status = HttpStatusCode.OK.ToString(),
                        Msg = "Codigo válido.",
                        Data = resp.Data,
                        exist = true,
                    };
                }
                return new DataResponseDto<Usuario>
                {
                    Status = HttpStatusCode.Forbidden.ToString(),
                    Msg = "Codigo incorrecto.",
                    Data = resp.Data,
                    exist = false,
                };

            }

            else
            {
                return new DataResponseDto<Usuario>
                {
                    Status = HttpStatusCode.NotFound.ToString(),
                    Msg = "Codigo expirado.",
                    Data = resp.Data,
                    exist = false,
                };
            }
               
        }

        public async Task<DataResponseDto<Usuario>> ResetPsw(UsuarioDto userDto)
        {
            var resp = await this.GetUser(userDto);

            if (resp.Data == null)
            {
                return resp;
            }
            else
            {
                resp.Data.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                await _userRepo.GuardarUsuarioAsync();

                return new DataResponseDto<Usuario>
                {
                    Status = HttpStatusCode.OK.ToString(),
                    Msg = "Contraseña actualizada con éxito.",
                    Data = resp.Data,
                    exist = true,
                };
            }
                  

        }
    }
}

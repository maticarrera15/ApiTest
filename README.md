# ApiTest
ApiTest es un proyecto en el que me encuentro trabajando y agregando nueva funcionalidad.
Como tecnología Core está basado en .Net Core 6.0 y toda la lógica de base de datos está desarrollada en Sql Server.
Consta de 3 controladores iniciales que son "Producto", "Usuario" y "Auth".
El controlador de Productos contiene servicios para realizar un crud de Productos genéricos cargados en la base.
Usuarios y Auth, contiene servicios para realizar ABM se usuarios, y un test inicial de Login, el cual implementa un JwtService, y se obtiene un token JWT si el login es correcto.

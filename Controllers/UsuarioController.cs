using ApiCRMprueba.Models;
using ApiCRMprueba.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ApiCRMprueba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioController(IUsuarioRepository repository)
        {
            _repository = repository;         
        }

        [HttpGet]
        [Route("list")]
        public IActionResult ListUsuarios()
        {
            var usuarios = _repository.Get();

            if (usuarios == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Error al obtener usuarios",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de usuarios",
                result = usuarios
            });

        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddUsuario(Usuario usuario)
        {
            // Verificar si el nombre de usuario no contiene signos
            if (Regex.IsMatch(usuario.UserName, @"[^\w\d]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario no puede contener signos.",
                    result = ""
                });
            }

            var usuarios = _repository.Get();

            // Verificar si el nombre de usuario está duplicado
            if (usuarios.Any(u => u.UserName == usuario.UserName))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario ya está en uso.",
                    result = ""
                });
            }

            // Verificar si el nombre de usuario contiene al menos un número
            if (!Regex.IsMatch(usuario.UserName, @"\d"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario debe contener al menos un número.",
                    result = ""
                });
            }

            // Verificar si el nombre de usuario contiene al menos una letra mayúscula
            if (!Regex.IsMatch(usuario.UserName, @"[A-Z]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario debe contener al menos una letra mayúscula.",
                    result = ""
                });
            }

            // Verificar la longitud del nombre de usuario
            if (usuario.UserName.Length < 8 || usuario.UserName.Length > 20)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "El nombre de usuario debe tener entre 8 y 20 caracteres.",
                    result = ""
                });
            }

            // Verificar si la contraseña tiene al menos 8 caracteres
            if (usuario.Password.Length < 8)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña debe tener al menos 8 caracteres.",
                    result = ""
                });
            }

            // Verificar si la contraseña contiene al menos una letra mayúscula
            if (!Regex.IsMatch(usuario.Password, @"[A-Z]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña debe contener al menos una letra mayúscula.",
                    result = ""
                });
            }

            // Verificar si la contraseña no contiene espacios
            if (usuario.Password.Contains(" "))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña no debe contener espacios.",
                    result = ""
                });
            }

            // Verificar si la contraseña contiene al menos un signo
            if (!Regex.IsMatch(usuario.Password, @"[\W]"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La contraseña debe contener al menos un signo.",
                    result = ""
                });
            }

            bool isAdd = _repository.Add(usuario);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Error al agregar usuario",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Usuario agregado con éxito!",
                result = ""
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _repository.GetById(id);

            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Error al obtener usuario",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Usuario",
                result = usuario
            });
        }

        [HttpPatch]
        [Route("update/{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            var isUpdate = _repository.Update(id, usuario);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Error usuario no encontrado",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Usuario actualizado con éxito",
                result = ""
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var isDelete = _repository.Delete(id);

            if (!isDelete)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Error usuario no encontrado",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Usuario eliminado con éxito",
                result = ""
            });
        }
    }
}

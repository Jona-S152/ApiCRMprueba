using ApiCRMprueba.Models;
using ApiCRMprueba.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace ApiCRMprueba.Controllers
{
    [Route("api/persona")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddPersona(Persona persona)
        {

            // Verificar si la identificación tiene exactamente 10 dígitos
            if (persona.Identificacion.Length != 10)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La identificación debe tener 10 dígitos.",
                    result = ""
                });
            }

            // Verificar si la identificación solo contiene números
            if (!Regex.IsMatch(persona.Identificacion, @"^\d+$"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La identificación solo debe contener números.",
                    result = ""
                });
            }

            // Verificar si la identificación no tiene más de 3 dígitos repetidos consecutivamente
            if (Regex.IsMatch(persona.Identificacion, @"(\d)\1{3}"))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "La identificación no debe tener más de 3 dígitos repetidos consecutivamente.",
                    result = ""
                });
            }

            bool isAdd = _personaRepository.Add(persona);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Error al agregar persona",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Persona agregada con éxito!",
                result = ""
            });
        }

        [HttpGet]
        [Route("list")]
        public IActionResult GetPersonas()
        {
            var personas = _personaRepository.Get();

            if (personas == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Error al obtener personas",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de personas",
                result = personas
            });
        }
        
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPersonaById(int id)
        {
            var persona = _personaRepository.GetById(id);

            if (persona == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Error al obtener persona",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Persona",
                result = persona
            });
        }

        [HttpPatch]
        [Route("update/{id}")]
        public IActionResult UpdatePersona(int id, Persona persona)
        {

            var isUpdate = _personaRepository.Update(id, persona);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Error persona no encontrada",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Persona actualizada con éxito",
                result = ""
            });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeletePersona(int id)
        {
            var isDelete = _personaRepository.Delete(id);

            if (!isDelete)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Error persona no encontrada",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Persona eliminada con éxito",
                result = ""
            });
        }
    }
}

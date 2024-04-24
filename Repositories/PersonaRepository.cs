using ApiCRMprueba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Util;

namespace ApiCRMprueba.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {

        private readonly Bteflnnvxgfztjiyphq0Context _context;

        public PersonaRepository(Bteflnnvxgfztjiyphq0Context context)
        {
            _context = context;
        }

        public bool Add(Persona persona)
        {
            try
            {
                _context.Personas.Add(persona);
                _context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var persona = _context.Personas.FirstOrDefault(p => p.IdPersona == id);

            if (persona != null)
            {
                _context.Remove(persona);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Persona> Get()
        {
            var personas = _context.Personas.Include(p => p.Usuarios).ToList();
            return personas;
        }

        public Persona GetById(int id)
        {
            var persona = _context.Personas.Include(p => p.Usuarios).FirstOrDefault(p => p.IdPersona == id);
            return persona;
        }

        public bool Update(int id, Persona persona)
        {
            try
            {
                var person = _context.Personas.FirstOrDefault(p => p.IdPersona == id);

                if (person != null)
                {
                    person.Nombre = persona.Nombre;
                    person.Apellidos = persona.Apellidos;
                    person.Identificacion = persona.Identificacion;
                    person.FechaNacimiento = persona.FechaNacimiento;

                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }catch (Exception)
            {
                return false;
            }
        }
    }
}

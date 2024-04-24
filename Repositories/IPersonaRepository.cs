using ApiCRMprueba.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCRMprueba.Repositories
{
    public interface IPersonaRepository
    {
        public List<Persona> Get();
        public Persona GetById(int id);
        public bool Add(Persona persona);
        public bool Update(int id, Persona persona);
        public bool Delete(int id);
    }
}

using ApiCRMprueba.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiCRMprueba.Repositories
{
    public interface IUsuarioRepository
    {
        public List<Usuario> Get();
        public Usuario GetById(int id);
        public bool Add(Usuario usuario);
        public bool Update(int id, Usuario usuario);
        public bool Delete(int id);
    }
}

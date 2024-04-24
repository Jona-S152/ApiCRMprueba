using ApiCRMprueba.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCRMprueba.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly Bteflnnvxgfztjiyphq0Context _context;

        public UsuarioRepository(Bteflnnvxgfztjiyphq0Context context)
        {
            _context = context;
        }

        public bool Add(Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return true;
            }catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (usuario != null)
            {
                _context.Remove(usuario);
                _context.SaveChanges();
                return true;
            }

            return false;

        }

        public List<Usuario> Get()
        {
            var usuarios = _context.Usuarios.Include(u => u.oPersona).ToList();

            return usuarios;
        }

        public Usuario GetById(int id)
        {
            var usuario = _context.Usuarios.Include(u => u.oPersona).FirstOrDefault(u => u.IdUsuario == id);

            return usuario;
        }

        public bool Update(int id, Usuario usuario)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

            if (user == null) return false;

            user.UserName = usuario.UserName;
            user.Password = usuario.Password;
            user.Mail = usuario.Mail;
            user.SessionActive = usuario.SessionActive;
            user.PersonaIdPersona = usuario.PersonaIdPersona;
            user.Status = usuario.Status;

            _context.SaveChanges();

            return true;
        }
    }
}

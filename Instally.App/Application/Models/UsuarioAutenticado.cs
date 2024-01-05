using Instally.App.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instally.App.Application.Models
{
    public class UsuarioAutenticado : BaseEntity
    {
        public string? Email { get; private set; }
        public string? Login { get; private set; }

        public UsuarioAutenticado(string email, string login)
        {
            Email = email;
            Login = login;
        }

        private void Dispose()
        {
            Email = null;
            Login = null;
        }

        public void Deslogar() => Dispose();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPerfiles.Models
{
    public class Aplicacion
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Nombre { get; set; }
        [StringLength(3)]
        public string Acronimo { get; set; }
        public string ClaveSeguridad { get; set; }
        [StringLength(125)]
        public string Img { get; set; }
        public bool Activo { get; set; }

    }

    public class Role
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Nombre { get; set; }
        public int AplicacionId { get; set; }
        public bool Activo { get; set; }
        public Aplicacion Aplicacion { get; set; } //nav
        public List<UsuarioRole> UsuarioRoles { get; set; } //
    }

    public class Modulo
    {
        public int Id { get; set; }
        public int AplicacionId { get; set; }
        [StringLength(20)]
        public string Nombre { get; set; }
        [StringLength(3)]
        public string Acronimo { get; set; }
        public Aplicacion Aplicacion { get; set; } // nav
        public List<UsuarioModulo> UsuarioModulos { get; set; }

    }

    public class Usuario
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        public string Password { get; set; }
        [StringLength(20)]
        public string Nombres { get; set; }
        [StringLength(20)]
        public string Apaterno { get; set; }
        [StringLength(20)]
        public string  Amaterno { get; set; }
        [StringLength(30)]
        public string Correo { get; set; }
        [StringLength(30)]
        public string Departamento { get; set; }
        public int NSandista { get; set; }
        public bool ActiveDirectory { get; set; }
        public bool Estatus { get; set; }
        public DateTime FechaReg { get; set; }
        public int UsuarioReg { get; set; }
        public DateTime FechaMod { get; set; }
        public int UsuarioMod { get; set; }
        public DateTime FechaBaja { get; set; }
        public int UsuarioBaja { get; set; }
        public List<UsuarioRole> UsuarioRoles  { get; set; }
        public List<UsuarioModulo> UsuarioModulos { get; set; }

    }

    public class UsuarioRole
    {
        public int UsuarioId { get; set; }
        public int RoleId { get; set; }
        public Usuario Usuario { get; set; } // nav
        public Role Role { get; set; } // nav

    }

    public class UsuarioModulo
    {
        public int UsuarioId { get; set; }
        public int ModuloId { get; set; }
        public string Accion { get; set; }
        public Usuario Usuario { get; set; } // nav
        public Modulo Modulo { get; set; } // nav
    }

    public class RoleModuloDefault
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Modulos { get; set; }
        public Role Role { get; set; } // nav
    }


}

using ApiPerfiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPerfiles.Extensions
{
    public static class Extenciones
    {
        public static void Map(this Aplicacion itemDb, Aplicacion itemNuevo)
        {
            itemDb.Nombre = itemNuevo.Nombre;
            itemDb.Acronimo = itemNuevo.Acronimo;
            itemDb.ClaveSeguridad = itemNuevo.ClaveSeguridad;
            itemDb.Img = itemNuevo.Img;

        }

        public static void Map(this Role itemDb, Role itemNuevo)
        {
            itemDb.Nombre = itemNuevo.Nombre;
            itemDb.AplicacionId = itemNuevo.AplicacionId;            
        }

        public static void Map(this Modulo itemDb, Modulo itemNuevo)
        {
            itemDb.AplicacionId = itemNuevo.AplicacionId;
            itemDb.Nombre = itemNuevo.Nombre;
            itemDb.Acronimo = itemNuevo.Acronimo;
            
        }
        public static void Map(this Usuario itemDb, Usuario itemNuevo)
        {
            itemDb.UserName = itemNuevo.UserName;     
            //No se pasará el password

            itemDb.Nombres = itemNuevo.Nombres;
            itemDb.Apaterno = itemNuevo.Apaterno;
            itemDb.Amaterno = itemNuevo.Amaterno;
            itemDb.Correo = itemNuevo.Correo;
            itemDb.Departamento = itemNuevo.Departamento;
            itemDb.NSandista = itemNuevo.NSandista;
            itemDb.ActiveDirectory = itemNuevo.ActiveDirectory;
            itemDb.Estatus = itemNuevo.Estatus;
            //itemDb.FechaReg = itemNuevo.FechaReg;
            //itemDb.UsuarioReg = itemNuevo.UsuarioReg;
            //itemDb.FechaMod = itemNuevo.FechaMod;
            //itemDb.UsuarioMod = itemNuevo.UsuarioMod;
            //itemDb.FechaBaja = itemNuevo.FechaBaja;
            //itemDb.UsuarioBaja = itemNuevo.UsuarioBaja;
        }

        public static void Map(this UsuarioRole itemDb, UsuarioRole itemNuevo)
        {
            itemDb.UsuarioId = itemNuevo.UsuarioId;
            itemDb.RoleId = itemNuevo.RoleId;
        }

        public static void Map(this RoleModuloDefault itemDb, RoleModuloDefault itemNuevo)
        {
            itemDb.RoleId = itemNuevo.RoleId;
            itemDb.Modulos = itemNuevo.Modulos;

        }
    }
}

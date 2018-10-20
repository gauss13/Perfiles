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
          
        }
    }
}

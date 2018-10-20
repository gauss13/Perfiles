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
    }
}

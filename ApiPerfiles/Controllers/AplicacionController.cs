using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPerfiles.Extensions;
using ApiPerfiles.Models;
using ApiPerfiles.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPerfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AplicacionController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }
        public AplicacionController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await this.Repositorio.Aplicaciones.GetByIdAsync(id);

            return Ok(new
            {
                ok = true,
                Aplicacion = item
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await this.Repositorio.Aplicaciones.GetAllAsync();

            // BAD REQUEST
            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = "No se encontrarón aplicaciones",
                    errors = ""
                };

                BadRequest(objB);

            }

            // OK
            var obj = new
            {
                ok = true,
                total = lista.Count(),
                Aplicaciones = lista
            };

            return Ok(obj);
        }
        
        [HttpPost]
        public IActionResult Crear(Aplicacion item)
        {
            var r = this.Repositorio.Aplicaciones.AddAsync(item);
            this.Repositorio.CompleteAsync();

            var obj = new
            {
                ok = true,
                Aplicacion = r
            };

            return Created("", obj);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(Aplicacion itemN, int id)
        {
            itemN.Id = id;

            //buscar la aplicacion 
            var itemEncontrado = await this.Repositorio.Aplicaciones.GetByIdAsync(id);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = "No se encontró la aplicacion", erros = "" });
            }
            
            itemEncontrado.Map(itemN);

            var r = this.Repositorio.Aplicaciones.Update(itemEncontrado);

            var obj = new
            {
                ok = true,
                Aplicacion = itemEncontrado
            };

            return Created("", obj);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            //buscar la aplicacion 
            var itemEncontrado = await this.Repositorio.Aplicaciones.GetByIdAsync(id);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = "No se encontró la aplicacion", erros = "" });
            }

            itemEncontrado.Activo = false;
            var r = this.Repositorio.Aplicaciones.Update(itemEncontrado);
            
            var obj = new
            {
                ok = true,
                Aplicacion = itemEncontrado
            };

            return Ok(obj);

        }

    }
}
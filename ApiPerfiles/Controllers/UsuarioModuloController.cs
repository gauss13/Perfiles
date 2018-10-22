using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPerfiles.Models;
using ApiPerfiles.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPerfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioModuloController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }
        public UsuarioModuloController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }


        // ->> ACTIONS

        [HttpGet("{idu}/{ida}")]
        public async Task<IActionResult> GetByIdUsuario(int idu,int ida)
        {
            // var lista = await this.Repositorio.UsuarioModulos.FindAsync(x => x.UsuarioId == idu && x.i);

            var lista =  this.Repositorio.UsuarioModulos.GetModuloByUsuario(idu, ida);

            var arrayMod = lista.Select(x => new { x.ModuloId, x.Accion, x.Modulo.Nombre, x.Modulo.Acronimo }).ToList();

            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = $"No se encontró Modulos para el usuario con id {idu}",
                    errors = ""
                };

                return BadRequest(objB);
            }

            return Ok(new
            {
                ok = true,
                UsuarioModulo = arrayMod
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await this.Repositorio.UsuarioModulos.GetAllAsync();

            // BAD REQUEST
            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = "No se encontrarón UsuarioModulos",
                    errors = ""
                };
                return BadRequest(objB);
            }

            // OK
            var obj = new
            {
                ok = true,
                total = lista.Count(),
                UsuarioModulos = lista
            };

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioModulo item)
        {
            try
            {

                var r = await this.Repositorio.UsuarioModulos.AddAsync(item);
                await this.Repositorio.CompleteAsync();

                var obj = new
                {
                    ok = true,
                    UsuarioModulo = r
                };

                return Created("", obj);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al crear la UsuarioModulo",
                    errors = new { mensaje = ex.Message }

                });

            }


        }


        [HttpDelete()]
        public async Task<IActionResult> Eliminar([FromBody] UsuarioModulo itemN)
        {
            //buscar la UsuarioModulo 
            var itemEncontrado = await this.Repositorio.UsuarioModulos.FindAsync(x => x.UsuarioId == itemN.UsuarioId && x.ModuloId == itemN.ModuloId);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = $"No se encontró el UsuarioModulo con Id {itemN.ModuloId}", erros = "" });
            }

            // borrado fisico

            this.Repositorio.UsuarioModulos.RemoveRange(itemEncontrado);

            await this.Repositorio.CompleteAsync();

            var obj = new
            {
                ok = true,
                mensaje = $"Se Borró el UsuarioModulo {itemN.ModuloId}, correctamente",
                UsuarioModulo = itemEncontrado
            };

            return Ok(obj);

        }


        // <<- ACTIONS

    }
}
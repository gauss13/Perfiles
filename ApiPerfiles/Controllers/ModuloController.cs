using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPerfiles.Models;
using ApiPerfiles.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiPerfiles.Extensions;


namespace ApiPerfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuloController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }
        public ModuloController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }
        
        // ->> ACTIONS

        [HttpGet("{ida}/{idm}")]
        public async Task<IActionResult> GetById(int ida, int idm)
        {
            var item = await this.Repositorio.Modulos.FindAsync(x => x.AplicacionId == ida && x.Id == idm);

            if (item == null)
            {
                var objB = new
                {
                    ok = false,
                    mensaje = $"No se encontró la Modulo con id {idm}",
                    errors = ""
                };

                return BadRequest(objB);
            }

            return Ok(new
            {
                ok = true,
                Modulo = item
            });
        }

        [HttpGet("{ida}")]
        public async Task<IActionResult> GetAll(int ida)
        {
            var lista = await this.Repositorio.Modulos.FindAsync(x=> x.AplicacionId == ida);

            // BAD REQUEST
            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = "No se encontrarón Modulos",
                    errors = ""
                };
                return BadRequest(objB);
            }

            // OK
            var obj = new
            {
                ok = true,
                total = lista.Count(),
                Modulos = lista
            };

            return Ok(obj);
        }

        //Validar acceso al modulo
        [HttpGet("{ida}/{idm}/{idu}")]
        public async Task<IActionResult> ValidarAcceso(int ida, int idm, int idu)
        {
            var item = await this.Repositorio.Modulos.FindAsync(x => x.AplicacionId == ida && x.Id == idm);

            if (!item.Any())
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = $"El modulo {idm} no se encuentra en la aplicacion {ida}"
                });
            }

            var itemUm = await this.Repositorio.UsuarioModulos.FindAsync(x => x.ModuloId == idm && x.UsuarioId == idu);

            if (!itemUm.Any())
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = $"El modulo no esta asignado al usuario {idu}"
                });
            }

            return Ok(new { ok = true });

        }



        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Modulo item)
        {
            try
            {

                var r = await this.Repositorio.Modulos.AddAsync(item);
                await this.Repositorio.CompleteAsync();

                var obj = new
                {
                    ok = true,
                    Modulo = r
                };

                return Created("", obj);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al crear la Modulo",
                    errors = new { mensaje = ex.Message }

                });

            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Modulo itemN, int id)
        {

            try
            {
                itemN.Id = id;

                //buscar la Modulo 
                var itemEncontrado = await this.Repositorio.Modulos.GetByIdAsync(id);

                if (itemEncontrado == null)
                {
                    return BadRequest(new { ok = false, mensaje = "No se encontró la Modulo", erros = "" });
                }

                itemEncontrado.Map(itemN);

                var r = this.Repositorio.Modulos.Update(itemEncontrado);
                await this.Repositorio.CompleteAsync();

                var obj = new
                {
                    ok = true,
                    Modulo = itemEncontrado
                };

                return Created("", obj);


            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al Actualizar los datos de la Modulo",
                    errors = new { mensaje = ex.Message }

                });

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            //buscar la Modulo 
            var itemEncontrado = await this.Repositorio.Modulos.GetByIdAsync(id);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = $"No se encontró el Modulo con Id {id}", erros = "" });
            }

            // borrado fisico
          
            this.Repositorio.Modulos.Remove(itemEncontrado);
            await this.Repositorio.CompleteAsync();

            var obj = new
            {
                ok = true,
                mensaje = $"Se Borró el Modulo {id}, correctamente",
                Modulo = itemEncontrado
            };

            return Ok(obj);

        }
        
        // <<- ACTIONS

       
    }
}
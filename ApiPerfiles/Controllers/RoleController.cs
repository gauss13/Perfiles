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
    public class RoleController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }

        public RoleController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }


        // ->> ACTIONS

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await this.Repositorio.Roles.GetByIdAsync(id);

            if (item == null)
            {
                var objB = new
                {
                    ok = false,
                    mensaje = $"No se encontró la Role con id {id}",
                    errors = ""
                };

                return BadRequest(objB);
            }

            return Ok(new
            {
                ok = true,
                Role = item
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await this.Repositorio.Roles.GetAllAsync();

            // BAD REQUEST
            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = "No se encontrarón Roles",
                    errors = ""
                };
                return BadRequest(objB);
            }

            // OK
            var obj = new
            {
                ok = true,
                total = lista.Count(),
                Roles = lista
            };

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Role item)
        {
            try
            {

                var r = await this.Repositorio.Roles.AddAsync(item);
                await this.Repositorio.CompleteAsync();

                var obj = new
                {
                    ok = true,
                    Role = r
                };

                return Created("", obj);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al crear la Role",
                    errors = new { mensaje = ex.Message }

                });

            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Role itemN, int id)
        {

            try
            {
                itemN.Id = id;

                //buscar la Role 
                var itemEncontrado = await this.Repositorio.Roles.GetByIdAsync(id);

                if (itemEncontrado == null)
                {
                    return BadRequest(new { ok = false, mensaje = "No se encontró la Role", erros = "" });
                }

                itemEncontrado.Map(itemN);

                var r = this.Repositorio.Roles.Update(itemEncontrado);
                await this.Repositorio.CompleteAsync();

                var obj = new
                {
                    ok = true,
                    Role = itemEncontrado
                };

                return Created("", obj);


            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al Actualizar los datos de la Role",
                    errors = new { mensaje = ex.Message }

                });

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            //buscar la Role 
            var itemEncontrado = await this.Repositorio.Roles.GetByIdAsync(id);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = $"No se encontró el Role con Id {id}", erros = "" });
            }

            // no se borra fisicamente el registro, solo se cambia de estatus
            itemEncontrado.Activo = false;
            var r = this.Repositorio.Roles.Update(itemEncontrado);
            await this.Repositorio.CompleteAsync();

            var obj = new
            {
                ok = true,
                mensaje = $"Se Desactivo el Role {id}, correctamente",
                Role = itemEncontrado
            };

            return Ok(obj);

        }


        // <<- ACTIONS

    }
}
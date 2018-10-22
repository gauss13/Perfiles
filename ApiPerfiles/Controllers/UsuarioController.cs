using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPerfiles.Models;
using ApiPerfiles.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiPerfiles.Extensions;
using CryptoHelper;
using ApiPerfiles.ViewModel;

namespace ApiPerfiles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }

        public UsuarioController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }


        // ->> ACTIONS

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await this.Repositorio.Usuarios.GetByIdAsync(id);

            item.Password = ":)";

            if (item == null)
            {
                var objB = new
                {
                    ok = false,
                    mensaje = $"No se encontró la Usuario con id {id}",
                    errors = ""
                };

                return BadRequest(objB);
            }

            return Ok(new
            {
                ok = true,
                Usuario = item
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await this.Repositorio.Usuarios.GetAllAsync();

            foreach (var item in lista)
            {
                item.Password = ":)";
            }

            // BAD REQUEST
            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = "No se encontrarón Usuarios",
                    errors = ""
                };
                return BadRequest(objB);
            }

            // OK
            var obj = new
            {
                ok = true,
                total = lista.Count(),
                Usuarios = lista
            };

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Usuario item)
        {
            try
            {
                item.FechaReg = DateTime.Now;

                var hashPass = Crypto.HashPassword(item.Password);
                item.Password = hashPass;

                var r = await this.Repositorio.Usuarios.AddAsync(item);
                await this.Repositorio.CompleteAsync();

                r.Password = ":)";

                var obj = new
                {
                    ok = true,
                    Usuario = r
                };

                return Created("", obj);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al crear la Usuario",
                    errors = new { mensaje = ex.Message }

                });

            }


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Usuario itemN, int id)
        {

            try
            {
                itemN.Id = id;

                //buscar la Usuario 
                var itemEncontrado = await this.Repositorio.Usuarios.GetByIdAsync(id);

                if (itemEncontrado == null)
                {
                    return BadRequest(new { ok = false, mensaje = "No se encontró la Usuario", erros = "" });
                }

                itemEncontrado.Map(itemN);

                itemEncontrado.FechaMod = DateTime.Now;
                var r = this.Repositorio.Usuarios.Update(itemEncontrado);
                await this.Repositorio.CompleteAsync();

                itemEncontrado.Password = ":)";
                var obj = new
                {
                    ok = true,
                    Usuario = itemEncontrado
                };

                return Created("", obj);


            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al Actualizar los datos de la Usuario",
                    errors = new { mensaje = ex.Message }

                });

            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            //buscar la Usuario 
            var itemEncontrado = await this.Repositorio.Usuarios.GetByIdAsync(id);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = $"No se encontró el Usuario con Id {id}", erros = "" });
            }

            // no se borra fisicamente el registro, solo se cambia de estatus
            itemEncontrado.Estatus = false;
            itemEncontrado.FechaBaja = DateTime.Now;

            var r = this.Repositorio.Usuarios.Update(itemEncontrado);
            await this.Repositorio.CompleteAsync();

            var obj = new
            {
                ok = true,
                mensaje = $"Se Desactivo el Usuario {id}, correctamente",
                Usuario = itemEncontrado
            };

            return Ok(obj);

        }


        [HttpPost("[action]/{id:int}")]
        public async Task<IActionResult> ActualizarPassword([FromBody] PasswordVM vmodel, int id)
        {
            // validar password vacios
            if (vmodel.PasswordActual.Trim() == string.Empty || vmodel.PasswordNuevo.Trim() == string.Empty)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { ok = false, mensaje = "No debe enviar password vacios" });
            }

            // Es el mismo password ?
            if (vmodel.PasswordActual.Trim() == vmodel.PasswordNuevo.Trim())
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { ok = false, mensaje = "Los password son iguales" });
            }


            //buscar la Usuario 
            var itemEncontrado = await this.Repositorio.Usuarios.GetByIdAsync(id);


            // Verfivar password actual
            var valid = Crypto.VerifyHashedPassword(itemEncontrado.Password, vmodel.PasswordActual.Trim());

            if (!valid)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable, new { ok = false, mensaje = "El password actual no es correcto." });
            }


            //Hacer el cambio de password
            var hashNuevoPassword = Crypto.HashPassword(vmodel.PasswordNuevo);

            //Actualizar en BD
            itemEncontrado.FechaMod = DateTime.Now;
            itemEncontrado.Password = hashNuevoPassword;
            var r = this.Repositorio.Usuarios.Update(itemEncontrado);
            await this.Repositorio.CompleteAsync();

            return Ok(new { ok = true, mensaje = "El password se cambio correctamente." });


        }


        // <<- ACTIONS

    }
}
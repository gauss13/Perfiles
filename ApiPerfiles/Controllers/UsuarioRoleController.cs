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
    public class UsuarioRoleController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }

        public UsuarioRoleController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }



        // ->> ACTIONS

        [HttpGet("{idu}")]
        public async Task<IActionResult> GetByIdUsuario(int idu)
        {
            var item = await this.Repositorio.UsuarioRoles.FindAsync(x => x.UsuarioId == idu);

            if (!item.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = $"No se encontró UsuarioRoles para el usuario con id {idu}",
                    errors = ""
                };

                return BadRequest(objB);
            }

            return Ok(new
            {
                ok = true,
                UsuarioRole = item
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lista = await this.Repositorio.UsuarioRoles.GetAllAsync();

            // BAD REQUEST
            if (!lista.Any())
            {
                var objB = new
                {
                    ok = false,
                    mensaje = "No se encontrarón UsuarioRoles",
                    errors = ""
                };
                return BadRequest(objB);
            }

            // OK
            var obj = new
            {
                ok = true,
                total = lista.Count(),
                UsuarioRoles = lista
            };

            return Ok(obj);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioRole item)
        {
            try
            {

                var r = await this.Repositorio.UsuarioRoles.AddAsync(item);
                await this.Repositorio.CompleteAsync();

                #region ASIGNAR MODULOS DEFAULT

             
                //Asignar los modulos default del role
                var listaModulos = await this.Repositorio.RoleModulosDefault.FindAsync(x => x.RoleId == item.RoleId);

                if(listaModulos.Any())
                {
                    var arrayMod = listaModulos.FirstOrDefault().Modulos.Split(',');

                    if (arrayMod.Length > 0)
                    {
                        List<UsuarioModulo> listaUM = new List<UsuarioModulo>();
                        foreach (var key in arrayMod)
                        {
                            UsuarioModulo itum = new UsuarioModulo()
                            {
                                UsuarioId = item.UsuarioId,
                                ModuloId = Int32.Parse(key)
                            };

                            listaUM.Add(itum);
                        }

                        if(listaUM.Any())
                        {
                           await this.Repositorio.UsuarioModulos.AddRangeAsync(listaUM);
                            await this.Repositorio.CompleteAsync();
                        }

                    }
                }

                #endregion


                var obj = new
                {
                    ok = true,
                    UsuarioRole = r
                };

                return Created("", obj);

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    ok = false,
                    mensaje = "Se produjo un error al crear la UsuarioRole",
                    errors = new { mensaje = ex.Message }
                });
            }
        }
            

        [HttpDelete()]
        public async Task<IActionResult> Eliminar([FromBody] UsuarioRole itemN)
        {
            //buscar la UsuarioRole 
            var itemEncontrado = await this.Repositorio.UsuarioRoles.FindAsync(x => x.UsuarioId == itemN.UsuarioId && x.RoleId == itemN.RoleId);

            if (itemEncontrado == null)
            {
                return BadRequest(new { ok = false, mensaje = $"No se encontró el UsuarioRole con Id {itemN.RoleId}", erros = "" });
            }

            // borrado fisico

            this.Repositorio.UsuarioRoles.RemoveRange(itemEncontrado);
            await this.Repositorio.CompleteAsync();


            #region REMUEVE MODULOS DEFAULT

            //Asignar los modulos default del role
            var listaModulos = await this.Repositorio.RoleModulosDefault.FindAsync(x => x.RoleId == itemN.RoleId);

            if (listaModulos.Any())
            {
                var arrayMod = listaModulos.FirstOrDefault().Modulos.Split(',');

                if (arrayMod.Length > 0)
                {
                    List<UsuarioModulo> listaUM = new List<UsuarioModulo>();
                    foreach (var key in arrayMod)
                    {
                        UsuarioModulo itum = new UsuarioModulo()
                        {
                            UsuarioId = itemN.UsuarioId,
                            ModuloId = Int32.Parse(key)
                        };
                    }

                    if (listaUM.Any())
                    {
                         this.Repositorio.UsuarioModulos.RemoveRange(listaUM);
                        await this.Repositorio.CompleteAsync();
                    }

                }
            }
            #endregion



            var obj = new
            {
                ok = true,
                mensaje = $"Se Borró el UsuarioRole {itemN.RoleId}, correctamente",
                UsuarioRole = itemEncontrado
            };

            return Ok(obj);

        }


        // <<- ACTIONS


    }
}
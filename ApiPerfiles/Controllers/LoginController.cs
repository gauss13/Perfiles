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
    public class LoginController : ControllerBase
    {
        public IRepositorioWrapper Repositorio { get; }
        public LoginController(IRepositorioWrapper rep)
        {
            Repositorio = rep;
        }


        [HttpPost("{ida}")]
        public async Task<IActionResult> PostLogin([FromBody] Usuario usuario, int ida)
        {

            // Para que un usuario pueda acceder a las aplicaciones,
            // deberá estar activo en el AC y registrado en la BD con estatus activo


            //Validar contra AD
            // pendiente

            Usuario usuarioDb = null;

            try
            {


                //Valirdar contra BD
                var ue = await this.Repositorio.Usuarios.FindAsync(x => x.UserName == usuario.UserName);
                 usuarioDb = ue.FirstOrDefault();

                if (usuarioDb == null)
                {
                    return BadRequest(new { ok = false, mensaje = $"No se encontró el usuario {usuario.UserName}" });
                }

                //Validar el password
                // si el usuario esta activo en AD no se validara el password en la BD
                // solo con que este registrado y activo BD
                var valid = CryptoHelper.Crypto.VerifyHashedPassword(usuarioDb.Password, usuario.Password);

                if (!valid)
                {
                    return BadRequest(new { ok = false, mensaje = $"El password es inconrrecto" });
                }

                //Validar el estatus del usuario
                if (!usuarioDb.Estatus)
                {
                    return BadRequest(new { ok = false, mensaje = $"El usuario esta desactivado" });
                }

                //Validar acceso a la aplicacion 
                var listaRole = this.Repositorio.UsuarioRoles.GetUsuarioRoleConRoles(usuarioDb.Id);

                var ra = listaRole.Where(x => x.Role.AplicacionId == ida).FirstOrDefault();


                if(ra == null)
                {
                    return BadRequest(new { ok = false, mensaje = $"Su usuario no tiene acceso a la aplicacion." });
                }             


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ok = false, mensaje = "Se produjo un error en el servidor, 500", errors = new { mensaje = ex.Message } });

            }


            usuarioDb.Password = ":)";
            return Ok (new { ok = true, usuario = usuarioDb });

        }


    }
}
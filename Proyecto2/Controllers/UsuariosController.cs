using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Proyecto2.DAL;
using Proyecto2.Models;

namespace Proyecto2.Controllers
{
    public class UsuariosController : ApiController
    {
        private GestorTareas db = new GestorTareas();

        [HttpGet]
        public IEnumerable<Usuario> ObtenerUsuarios()
        {
            return db.Usuarios.ToList();
        }
        [HttpPost]
        public IHttpActionResult CrearUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.Usuarios.Add(usuario);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        [HttpPut]
        public IHttpActionResult ActualizarUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != usuario.Id)
            {
                return BadRequest();
            }
            db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpDelete]

        public IHttpActionResult EliminarUsuario(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            db.Usuarios.Remove(usuario);
            db.SaveChanges();
            return Ok(usuario);
        }


    }
}






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
    public class TareasController : ApiController
    {
        private GestorTareas db = new GestorTareas();
        [HttpGet]
        public IEnumerable<Tarea> ObtenerTareas()
        {
            return db.Tareas.Include(t => t.Usuario).ToList();
        }
        [HttpPost]
        public IHttpActionResult CrearTarea(Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tareas.Add(tarea);
            db.SaveChanges();

            // Recargar la tarea desde la base de datos con la relación Usuario
            var tareaConUsuario = db.Tareas
                                    .Include(t => t.Usuario)
                                    .FirstOrDefault(t => t.Id == tarea.Id);

            return CreatedAtRoute("DefaultApi", new { id = tarea.Id }, tareaConUsuario);
        }

        [HttpPut]
        public IHttpActionResult ActualizarTarea(int id, Tarea tarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != tarea.Id)
            {
                return BadRequest();
            }
            db.Entry(tarea).State = EntityState.Modified;
            db.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }
        [HttpDelete]
        public IHttpActionResult EliminarTarea(int id)
        {
            Tarea tarea = db.Tareas.Find(id);
            if (tarea == null)
            {
                return NotFound();
            }
            db.Tareas.Remove(tarea);
            db.SaveChanges();
            return Ok(tarea);
        }
    }
}

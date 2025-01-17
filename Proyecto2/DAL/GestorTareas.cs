    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Entity;
    using Proyecto2.Models;

    namespace Proyecto2.DAL
    {
        public class GestorTareas : DbContext
        {
            public DbSet<Tarea> Tareas { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }

        }
    }




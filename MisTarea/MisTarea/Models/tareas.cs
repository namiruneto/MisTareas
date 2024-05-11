using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MisTarea.Models
{
    public class tareas
    {
        public class NuevaTarea
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string IdTipoTarea { get; set; }
            public string Descripcion { get; set; }
            public string Estado { get; set; }
            public string FecIngreso { get; set; }
            public string FecFinal { get; set; }
            public string Hora { get; set; }
            public bool Repetir { get; set; }
            public string Dias { get; set; }
        }

        public class TAREA
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            [MaxLength(100)]
            public string IdTipoTarea { get; set; }

            [MaxLength(100)]
            public string Nombre { get; set; }

            public string Descripcion { get; set; }

            public string Estado { get; set; }

            public string FecIngreso { get; set; }

            public string FecTerminar { get; set; }

            public string FecFinal { get; set; }           
        }

        public class Categoria
        {
            [PrimaryKey]
            public string Id { get; set; }

            public string Nombre { get; set; }
        }

        public class TareaDiaria
        {
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }

            public string Nombre { get; set; }

            public string Descripcion { get; set; }

            public string IdCategoria { get; set; }

            public string Hora { get; set; }

            public string Dias { get; set; }
            public string FechaUltimaRealizada { get; set; }
        }

        public class TareaPendiente
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string IdCategoria { get; set; }
            public string FechaInicio {  get; set; }
            public string FechaFin {  get; set; }
            public string Hora { get; set; }
            public string Descripcion { get; set; }
            public bool Diaria { get; set; }

        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MisTarea.Models;
using static MisTarea.Models.tareas;

namespace MisTarea.Class
{
    internal class CRUD
    {
        public void Guardar(tareas.NuevaTarea model)
        {
            ConexionSqlite conexionSqlite = new ConexionSqlite("");
            //se valida si va ser una tarea de una sola vez o de varios dias
            if (model.Repetir)
            {
                tareas.TareaDiaria tarea = new tareas.TareaDiaria
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    IdCategoria = model.IdTipoTarea,
                    Hora = model.Hora,
                    Dias = model.Dias,
                    FechaUltimaRealizada = DateTime.Now > DateTime.ParseExact(model.Hora, "HH:mm", CultureInfo.InvariantCulture) ? DateTime.Now.ToString("yyyy-MM-dd") : DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
                };
                conexionSqlite.Guardar(tarea);
            }
            else
            {
                tareas.TAREA tarea = new tareas.TAREA
                {
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    IdTipoTarea = model.IdTipoTarea,
                    Estado = "Pendiente",
                    FecIngreso = model.FecIngreso,
                    FecFinal = model.FecFinal,
                    FecTerminar = ""
                };
                conexionSqlite.Guardar(tarea);
            }
        } 

    }
}

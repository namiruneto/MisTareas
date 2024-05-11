using System.Web;
using System.Data;
using System.IO;
using System;
using SQLite;
using MisTarea.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using static MisTarea.Models.tareas;

namespace MisTarea.Class
{
    internal class ConexionSqlite
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;     
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private DataSet dataSet = new DataSet();
      
        SQLiteAsyncConnection db;

        public ConexionSqlite(string _pmDBFileSqlite)
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.db3");
            if (File.Exists(dbPath))
            {                
                db = new SQLiteAsyncConnection(dbPath);               
            }
            else
            {
                db = new SQLiteAsyncConnection(dbPath);                
                //crear tablas
                db.CreateTableAsync<tareas.TAREA>().Wait();
                db.CreateTableAsync<tareas.Categoria>().Wait();
                db.CreateTableAsync<tareas.TareaDiaria>().Wait();
            }
                         
        }
        public Task<int> Guardar<T>(T tareasDiaria)
        {
            return db.InsertAsync(tareasDiaria);
        }
        public async Task<bool> Actualizar(tareas.TAREA tareaDiaria) 
        {
            try
            {
                // Obtener el objeto original antes de la actualización
                var originalObjeto = await db.FindAsync<tareas.TAREA>(tareaDiaria.Id);

                // Actualizar el registro en la base de datos de forma asincrónica
                int filasAfectadas = await db.UpdateAsync(tareaDiaria);

                if (filasAfectadas > 0)
                {
                    // Comparar cada propiedad para detectar cambios
                    if (!EqualityComparer<string>.Default.Equals(originalObjeto.IdTipoTarea, tareaDiaria.IdTipoTarea))
                    {
                        Console.WriteLine("La propiedad IdTipoTarea ha cambiado de: " + originalObjeto.IdTipoTarea + " a: " + tareaDiaria.IdTipoTarea);
                        // Realizar acciones adicionales según sea necesario
                    }
                    if (!EqualityComparer<string>.Default.Equals(originalObjeto.Nombre, tareaDiaria.Nombre))
                    {
                        Console.WriteLine("La propiedad Nombre ha cambiado de: " + originalObjeto.Nombre + " a: " + tareaDiaria.Nombre);
                        // Realizar acciones adicionales según sea necesario
                    }
                    // Continuar comparando otras propiedades si es necesario

                    Console.WriteLine("La actualización se realizó correctamente.");
                    return true;
                }
                else
                {
                    // No se realizaron cambios en la base de datos
                    Console.WriteLine("No se realizaron cambios en la base de datos.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones aquí, si es necesario
                Console.WriteLine("Error al intentar actualizar el registro: " + ex.Message);
                return false;
            }
        }

        public async Task<List<tareas.TareaPendiente>> TareasPendiente()
        {
            var listaTareasDiaria = db.Table<tareas.TareaDiaria>().ToListAsync();
            var listaTareas = db.Table<tareas.TAREA>().Where(c => c.Estado == "Pendiente").ToListAsync();

            List<TareaDiaria> tareasDiaria = listaTareasDiaria.Result;
            List<TAREA> Tareas = listaTareas.Result;
            // sabemos los datos en general para posteriormente hacer cuales se deben mostrar 
          
            

            List<TareaPendiente> pendientes = new List<TareaPendiente>();
            //agregamos las tareas que elusuario efinio desde un inicio
            foreach (TAREA ta in Tareas)
            {
                pendientes.Add(new TareaPendiente
                {
                    FechaInicio = ta.FecIngreso,
                    FechaFin = ta.FecFinal.Split(' ')[0],
                    Id = ta.Id,
                    Nombre = ta.Nombre,
                    IdCategoria = ta.IdTipoTarea,
                    Hora = ta.FecFinal.Split(' ')[1],
                    Descripcion = ta.Descripcion,
                    Diaria = false
                }); ;
            }
            //se agregan las tareas que tiene pendiente del dia de hoy y si tiene de ayer tambien 
            foreach (TareaDiaria ta in tareasDiaria)
            {
                pendientes.Add(new TareaPendiente
                {
                    FechaFin = ta.FechaUltimaRealizada,
                    Id = ta.Id,
                    Nombre = ta.Nombre,
                    IdCategoria = ta.IdCategoria,
                    Hora = ta.Hora,
                    Descripcion = ta.Descripcion,
                    Diaria = true
                });
            }
            return pendientes;

        }

        public async Task<List<tareas.TareaPendiente>> TareasCompletada()
        {
            var listaTareas = db.Table<tareas.TAREA>().Where(c => c.Estado == "Completada").ToListAsync();

            List<TAREA> Tareas = listaTareas.Result;
            // sabemos los datos en general para posteriormente hacer cuales se deben mostrar 



            List<TareaPendiente> pendientes = new List<TareaPendiente>();
            //agregamos las tareas que elusuario efinio desde un inicio
            foreach (TAREA ta in Tareas)
            {
                pendientes.Add(new TareaPendiente
                {
                    FechaInicio = ta.FecIngreso,
                    FechaFin = ta.FecFinal,
                    Id = ta.Id,
                    Nombre = ta.Nombre,
                    IdCategoria = ta.IdTipoTarea,
                    Hora = ta.FecFinal,
                    Descripcion = ta.Descripcion,
                    Diaria = false
                }); ;
            }
            
            return pendientes;

        }

    }
}

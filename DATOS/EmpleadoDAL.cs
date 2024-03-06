using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATOS
{
    public class EmpleadoDAL
    {
        public void Agregar(Empleado empleado)
        {
            using (var db = new ProyectosContext())
            {
                db.Empleado.Add(empleado);
                db.SaveChanges();
            }
        }

        public List<EmpleadoCE> ListarEmpleados()
        {
            string sql = @"select e.EmpleadoId, e.Nombre, e.Apellido, e.Email, e.Direccion, e.Celular,
	                e.DepartamentoId, d.NombreDepartamento
                 from Empleado e
                inner join Departamento d on e.DepartamentoId = d.DepartamentoId";
            using (var db = new ProyectosContext())
            {
                return db.Database.SqlQuery<EmpleadoCE>(sql).ToList();
            }
        }

        public EmpleadoCE ObtenerEmpleado(int id)
        {
            //genera una busqueda por id
            string sql = @"select e.EmpleadoId, e.Nombre, e.Apellido, e.Email, e.Direccion, e.Celular,
	                e.DepartamentoId, d.NombreDepartamento
                 from Empleado e
                inner join Departamento d on e.DepartamentoId = d.DepartamentoId
                where e.EmpleadoId = @EmpleadoId";
            using (var db = new ProyectosContext())
            {
                //return db.Empleado.Where(p => p.ProyectoId == id).FirstOrDefault();
                //return db.Empleado.Find(id);
                return db.Database.SqlQuery<EmpleadoCE>(sql,
                    new SqlParameter("@EmpleadoId", id)).FirstOrDefault();
            }
        }

        public void Editar(Empleado empleado)
        {
            try
            {
                using (var db = new ProyectosContext())
                {
                    var origen = db.Empleado.Find(empleado.EmpleadoId);

                    // Verificar si el empleado existe en la base de datos
                    if (origen != null)
                    {
                        // Actualizar las propiedades con los valores proporcionados
                        origen.Nombre = empleado.Nombre;
                        origen.Apellido = empleado.Apellido;
                        origen.Email = empleado.Email;
                        origen.Direccion = empleado.Direccion;
                        origen.Celular = empleado.Celular;
                        origen.DepartamentoId = empleado.DepartamentoId;

                        // Guardar los cambios en la base de datos
                        db.SaveChanges();
                    }
                    else
                    {
                        // Manejar el caso donde el empleado no se encuentra en la base de datos
                        Console.WriteLine("El empleado no existe en la base de datos.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la operación de base de datos
                Console.WriteLine($"Error al editar el empleado: {ex.Message}");
            }
        }

        public void Eliminar(int id)
        {
            using (var db = new ProyectosContext())
            {
                var empleado = db.Empleado.Find(id);
                db.Empleado.Remove(empleado);
                db.SaveChanges();
            }
        }
    }

}


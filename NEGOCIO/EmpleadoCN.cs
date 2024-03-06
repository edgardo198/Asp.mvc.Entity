using DATOS;
using ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO
{
    public class EmpleadoCN
    {
        private static EmpleadoDAL obj = new EmpleadoDAL();

        public static void Agregar(Empleado empleado)
        {
            obj.Agregar(empleado);
        }

        public static List<EmpleadoCE> ListarEmpleados()
        {
            return obj.ListarEmpleados();
        }

        public static EmpleadoCE ObtenerEmpleado(int id)
        {
            return obj.ObtenerEmpleado(id);
        }

        public static void Editar(Empleado empleado)
        {
            obj.Editar(empleado);
        }

        public static void Eliminar(int id)
        {
            obj.Eliminar(id);
        }
    }
}

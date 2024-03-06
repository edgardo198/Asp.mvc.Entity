using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATOS;
using ENTIDAD;

namespace NEGOCIO
{
    public class DepartamentoCN
    {
        private static DepaertamentoDALC obj=new DepaertamentoDALC();

        public static void Agregar(Departamento dpto)
        {
            obj.Agregar(dpto);
        }

        public static List<Departamento> ListaDepartamento()
        {
            return obj.ListarDepartamento();
        }

        public static Departamento GetDepartamento(int id) { 
            return obj.GetDepartamento(id);
        }

        public static void Editar(Departamento dpto)
        {
            obj.Editar(dpto);
        }

        public static void Eliminar(int id) 
        {
            obj.Eliminar(id);
        }
    }
}

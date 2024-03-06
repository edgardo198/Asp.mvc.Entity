using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDAD
{
    public class ProyectoEmpleadoCE
    {
        public int ProyectoId { get; set; }
        public string NombreProyecto { get; set; }
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto { get { return $"{Nombre}{Apellido}"; } }
        public DateTime FechaAlta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcialFinal.CapaModelo
{
    public class Cls_Empleado
    {
        public int empleadoID { get; set; }

        public string numeroCarnet { get; set; }
        public string nombre { get; set; }
        public string fechaNacimiento { get; set; }
        public string categoria { get; set; }
        public string salario { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string estadoEmpleado { get; set; }
    }
}
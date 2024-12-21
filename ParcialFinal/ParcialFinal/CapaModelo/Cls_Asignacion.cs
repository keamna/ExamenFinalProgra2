using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcialFinal.CapaModelo
{
    public class Cls_Asignacion
    {
        public int asignacionID { get; set; }

        public int empleadoID { get; set; }
        public int proyectoID { get; set; }
        public string fechaAsignacion { get; set; }
        public string estadoAsignacion { get; set; }
    }
}
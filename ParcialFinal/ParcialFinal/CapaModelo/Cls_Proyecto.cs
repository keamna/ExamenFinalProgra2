using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcialFinal.CapaModelo
{
    public class Cls_Proyecto
    {
        public int proyectoID { get; set; }
        public int codigoProyecto { get; set; }

        public string nombre { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFin { get; set; }

        public string estadoProyecto { get; set; }

    }
}
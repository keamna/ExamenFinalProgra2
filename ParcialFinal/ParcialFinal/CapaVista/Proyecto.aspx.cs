using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParcialFinal.CapaLogica;
using ParcialFinal.CapaModelo;

namespace ParcialFinal.CapaVista
{
    public partial class Proyecto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();

            }
        }

        protected void LlenarGrid()
        {
            List<Cls_Proyecto> proyectos = Bussiness_Proyecto.ObtenerProyecto();

            if (proyectos.Count > 0)
            {
                GridViewProyectos.DataSource = proyectos;
                GridViewProyectos.DataBind();


                foreach (GridViewRow row in GridViewProyectos.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListEstadoProyecto");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay proyectos disponibles");
            }
        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListEstadoProyecto.SelectedItem.Text;

                int codigoProyecto = int.Parse(TcodigoProyecto.Text);

                if (Bussiness_Proyecto.AgregarProyecto(codigoProyecto, TnombreProyecto.Text, TfechaInicio.Text, TfechaFin.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Proyecto ingresado correctamente");
                    LlenarGrid();

                    TcodigoProyecto.Text = "";
                    TfechaInicio.Text = "";
                    TfechaFin.Text = "";
                    DropDownListEstadoProyecto.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar proyecto");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar proyecto: " + ex.Message);
            }
        }

        protected void Bborrar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(TproyectoID.Text);

                bool isDeleted = Bussiness_Proyecto.BorrarProyecto(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Proyecto borrado correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TproyectoID.Text = "";

            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);

                LlenarGrid();
            }
        }
    }
}
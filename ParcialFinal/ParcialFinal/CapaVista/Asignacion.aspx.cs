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
    public partial class Asignacion : System.Web.UI.Page
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
            List<Cls_Asignacion> asignaciones = Bussiness_Asignacion.ObtenerAsignacion();

            if (asignaciones.Count > 0)
            {
                GridViewAsignacion.DataSource = asignaciones;
                GridViewAsignacion.DataBind();


                foreach (GridViewRow row in GridViewAsignacion.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListEstadoAsignacion");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay asignaciones disponibles.");
            }

        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListEstadoAsignacion.SelectedItem.Text;

                int empleadoID = int.Parse(TempleadoID.Text);
                int proyectoID = int.Parse(TproyectoID.Text);

                if (Bussiness_Asignacion.AgregarAsignacion(empleadoID, proyectoID, TfechaAsignacion.Text, estadoSeleccionado) > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Asignacion ingresada correctamente");
                    LlenarGrid();

                    TempleadoID.Text = "";
                    TproyectoID.Text = "";
                    TfechaAsignacion.Text = "";
                    DropDownListEstadoAsignacion.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar asignacion");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar asignacion: " + ex.Message);
            }
        }

        protected void Bborrar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(TasignacionID.Text);

                bool isDeleted = Bussiness_Asignacion.BorrarAsignacion(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Asignacion borrada correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");

                }

                LlenarGrid();
                TasignacionID.Text = "";

            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);

                LlenarGrid();
            }
        }
    }
}
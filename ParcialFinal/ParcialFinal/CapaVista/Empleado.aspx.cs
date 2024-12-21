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
    public partial class Empleados : System.Web.UI.Page
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
            List<Cls_Empleado> empleados = Bussiness_Empleado.ObtenerEmpleado();

            if (empleados.Count > 0)
            {
                GridViewEmpleado.DataSource = empleados;
                GridViewEmpleado.DataBind();

                foreach (GridViewRow row in GridViewEmpleado.Rows)
                {
                    DropDownList ddlEstado = (DropDownList)row.FindControl("DropDownListEstadoEmpleado");
                    if (ddlEstado != null)
                    {
                        string estado = ((Label)row.FindControl("lblEstado")).Text;
                    }

                    DropDownList ddlCategoria = (DropDownList)row.FindControl("DropDownListCategoriaEmpleado");
                    if (ddlCategoria != null)
                    {
                        ddlCategoria.Items.Clear();
                        ddlCategoria.Items.Add(new ListItem("Administrador", "Administrador"));
                        ddlCategoria.Items.Add(new ListItem("Operario", "Operario"));
                        ddlCategoria.Items.Add(new ListItem("Peón", "Peón"));

                        string categoria = ((Label)row.FindControl("lblCategoria")).Text;
                        ListItem item = ddlCategoria.Items.FindByValue(categoria);
                        if (item != null)
                        {
                            ddlCategoria.SelectedValue = categoria;
                        }
                    }
                }
            }
            else
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "No hay empleados disponibles");
            }
        }

        protected void Bagregar_Click(object sender, EventArgs e)
        {
            try
            {
                string estadoSeleccionado = DropDownListEstadoEmpleado.SelectedItem.Text;
                string categoria = DropDownListCategoriaEmpleado.SelectedItem.Text;
                DateTime fechaNacimiento = DateTime.Parse(TfechaNacimiento.Text);  // Convierte la fecha correctamente
                decimal salario = decimal.Parse(Tsalario.Text);  // Convierte el salario a decimal

                // Llamada al método para agregar el empleado
                int resultado = Bussiness_Empleado.AgregarEmpleado(
                    TnumCarnet.Text,
                    TnombreEmpleado.Text,
                    fechaNacimiento,
                    categoria,
                    salario,
                    Tdireccion.Text,
                    Ttelefono.Text,
                    Tcorreo.Text,
                    estadoSeleccionado
                );

                if (resultado > 0)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Empleado ingresado correctamente");
                    LlenarGrid();  // Llama a LlenarGrid para actualizar la lista de empleados

                    // Limpia los campos de entrada
                    TnumCarnet.Text = "";
                    TnombreEmpleado.Text = "";
                    TfechaNacimiento.Text = "";
                    DropDownListCategoriaEmpleado.SelectedIndex = 0;
                    Tsalario.Text = "";
                    Tdireccion.Text = "";
                    Ttelefono.Text = "";
                    Tcorreo.Text = "";
                    DropDownListEstadoEmpleado.SelectedIndex = 0;
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar empleado");
                }
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Error al agregar empleado: " + ex.Message);
            }
        }

        protected void BconsultarFiltro_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica que el código no esté vacío
                if (string.IsNullOrWhiteSpace(TempleadoID.Text))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Por favor ingresa un código válido");
                    return;
                }

                // Verifica que el código sea un int
                int codigo;
                if (!int.TryParse(TempleadoID.Text, out codigo))
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es un número válido");
                    return;
                }

                List<Cls_Empleado> empleados = Bussiness_Empleado.ObtenerEmpleadoFiltro(codigo);

                // Verifica si hay resultados
                if (empleados.Count > 0)
                {
                    // Muestra solo los datos deseados
                    GridViewEmpleado.DataSource = empleados;
                    GridViewEmpleado.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código encontrado");

                    TempleadoID.Text = "";
                }
                else
                {
                    // Si no hay datos muestra mensaje y limpia el GridView
                    GridViewEmpleado.DataSource = null;
                    GridViewEmpleado.DataBind();

                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no encontrado");
                    LlenarGrid();
                }
            }
            catch (FormatException)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "El código ingresado no es válido. Por favor ingresa un número.");
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);
            }
        }

        protected void Bborrar_Click(object sender, EventArgs e)
        {
            try
            {
                int codigo = int.Parse(TempleadoID.Text);

                bool isDeleted = Bussiness_Empleado.BorrarEmpleado(codigo);

                if (isDeleted)
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Empleado borrado correctamente");
                }
                else
                {
                    DBConn.JavaScriptHelper.MostrarAlerta(this, "Código no existe o no se pudo borrar");
                }

                LlenarGrid();
                TempleadoID.Text = "";
            }
            catch (Exception ex)
            {
                DBConn.JavaScriptHelper.MostrarAlerta(this, "Ocurrió un error: " + ex.Message);
                LlenarGrid();
            }
        }
    }
}
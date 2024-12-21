using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ParcialFinal.CapaModelo;

namespace ParcialFinal.CapaLogica
{
    public class Bussiness_Proyecto
    {
        #region Listado de Proyectos

        public static List<Cls_Proyecto> ObtenerProyecto()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Proyecto> proyectos = new List<Cls_Proyecto>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarProyecto", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Proyecto proyecto = new Cls_Proyecto();
                            proyecto.proyectoID = reader.GetInt32(0);
                            proyecto.codigoProyecto = reader.GetInt32(1);
                            proyecto.nombre = reader.GetString(2);
                            proyecto.fechaInicio = reader.GetDateTime(reader.GetOrdinal("fechaInicio")).ToString("yyyy/MM/dd");
                            proyecto.fechaFin = reader.GetDateTime(reader.GetOrdinal("fechaFin")).ToString("yyyy/MM/dd");
                            proyecto.estadoProyecto = reader.GetString(5);

                            proyectos.Add(proyecto);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return proyectos;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return proyectos;
        }
        #endregion

        #region Agregar Proyecto
        public static int AgregarProyecto(int codigoProyecto, string nombre, string fechaInicio, string fechaFin, string estado)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarProyecto", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@CodigoProyecto", codigoProyecto));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));
                    cmd.Parameters.Add(new SqlParameter("@Estado", estado));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
        #endregion

        #region Borrar Proyecto

        public static bool BorrarProyecto(int proyectoID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM Proyectos WHERE ProyectoID = @ProyectoID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@ProyectoID", proyectoID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE Proyectos SET Estado = 'Inactivo' WHERE ProyectoID = @ProyectoID";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@ProyectoID", proyectoID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar proyecto: " + ex.Message);
                return false;
            }
            finally
            {
                if (Conn != null && Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
            }
        }

        #endregion
    }
}

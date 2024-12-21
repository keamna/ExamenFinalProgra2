using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ParcialFinal.CapaModelo;

namespace ParcialFinal.CapaLogica
{
    public class Bussiness_Asignacion
    {
        #region Listado de Asignaciones

        public static List<Cls_Asignacion> ObtenerAsignacion()
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            List<Cls_Asignacion> asignaciones = new List<Cls_Asignacion>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarAsignacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Asignacion asignacion = new Cls_Asignacion();
                            asignacion.asignacionID = reader.GetInt32(0);
                            asignacion.empleadoID = reader.GetInt32(1);
                            asignacion.proyectoID = reader.GetInt32(2);
                            asignacion.fechaAsignacion = reader.GetDateTime(reader.GetOrdinal("fechaAsignacion")).ToString("yyyy/MM/dd");
                            asignacion.estadoAsignacion = reader.GetString(4);

                            asignaciones.Add(asignacion);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return asignaciones;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return asignaciones;
        }
        #endregion

        #region Agregar Asignacion
        public static int AgregarAsignacion(int empleadoID, int proyectoID, string fechaAsignacion, string estado)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarAsignacion", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@EmpleadoID", empleadoID));
                    cmd.Parameters.Add(new SqlParameter("@ProyectoID", proyectoID));
                    cmd.Parameters.Add(new SqlParameter("@FechaAsignacion", fechaAsignacion));
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

        #region Borrar Asignacion

        public static bool BorrarAsignacion(int asignacionID)
        {
            SqlConnection Conn = null;

            try
            {
                Conn = DBConn.obtenerConexion();

                string queryExistencia = "SELECT COUNT(*) FROM Asignaciones WHERE AsignacionID = @AsignacionID";
                using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                {
                    cmdVerificar.Parameters.Add(new SqlParameter("@AsignacionID", asignacionID));

                    int count = (int)cmdVerificar.ExecuteScalar();
                    if (count == 0)
                    {
                        return false;
                    }
                }

                string actualizarEstado = "UPDATE Asignaciones SET Estado = 'Inactivo' WHERE AsignacionID = @AsignacionID";
                using (SqlCommand cmdEliminar = new SqlCommand(actualizarEstado, Conn))
                {
                    cmdEliminar.Parameters.Add(new SqlParameter("@AsignacionID", asignacionID));
                    int filasAfectadas = cmdEliminar.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar asignacion: " + ex.Message);
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

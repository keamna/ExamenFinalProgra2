using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using ParcialFinal.CapaModelo;

namespace ParcialFinal.CapaLogica
{
    public class Bussiness_Empleado
    {
        #region Listado de Empleados

        public static List<Cls_Empleado> ObtenerEmpleado()
        {
            List<Cls_Empleado> empleados = new List<Cls_Empleado>();
            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("consultarEmpleado", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cls_Empleado empleado = new Cls_Empleado
                            {
                                empleadoID = reader.GetInt32(0),
                                numeroCarnet = reader.GetString(1),
                                nombre = reader.GetString(2),
                                fechaNacimiento = reader.GetDateTime(3).ToString("yyyy/MM/dd"),
                                categoria = reader.GetString(4),
                                salario = reader.GetDecimal(5).ToString(),
                                direccion = reader.GetString(6),
                                telefono = reader.GetString(7),
                                correo = reader.GetString(8),
                                estadoEmpleado = reader.GetString(9)
                            };
                            empleados.Add(empleado);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al obtener empleados: " + ex.Message);
            }

            return empleados;
        }
        #endregion

        #region Agregar Empleados
        public static int AgregarEmpleado(string numeroCarnet, string nombre, DateTime fechaNacimiento, string categoria, decimal salario, string direccion, string telefono, string correo, string estado)
        {
            int retorno = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ingresarEmpleado", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@NumeroCarnet", numeroCarnet));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@FechaNacimiento", fechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@Categoria", categoria));
                    cmd.Parameters.Add(new SqlParameter("@Salario", salario));
                    cmd.Parameters.Add(new SqlParameter("@Direccion", direccion));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", telefono));
                    cmd.Parameters.Add(new SqlParameter("@Correo", correo));
                    cmd.Parameters.Add(new SqlParameter("@Estado", estado));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                System.Diagnostics.Debug.WriteLine("Error SQL: " + sqlEx.Message);
                retorno = -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error general: " + ex.Message);
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }

        #endregion

        #region Listado con Filtro
        public static List<Cls_Empleado> ObtenerEmpleadoFiltro(int empleadoID)
        {
            List<Cls_Empleado> empleados = new List<Cls_Empleado>();

            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("consultarEmpleadoFiltro", Conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@EmpleadoID", empleadoID));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Cls_Empleado empleado = new Cls_Empleado
                                {
                                    empleadoID = reader.GetInt32(0),
                                    numeroCarnet = reader.GetString(1),
                                    nombre = reader.GetString(2),
                                    fechaNacimiento = reader.GetDateTime(3).ToString("yyyy/MM/dd"),
                                    categoria = reader.GetString(4),
                                    salario = reader.GetDecimal(5).ToString(),
                                    direccion = reader.GetString(6),
                                    telefono = reader.GetString(7),
                                    correo = reader.GetString(8),
                                    estadoEmpleado = reader.GetString(9)
                                };

                                empleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error al obtener empleado por código: " + ex.Message);
            }

            return empleados;
        }
        #endregion

        #region Borrar Empleado

        public static bool BorrarEmpleado(int empleadoID)
        {
            try
            {
                using (SqlConnection Conn = DBConn.obtenerConexion())
                {
                    string queryExistencia = "IF EXISTS (SELECT 1 FROM Empleados WHERE EmpleadoID = @EmpleadoID) BEGIN UPDATE Empleados SET Estado = 'Inactivo' WHERE EmpleadoID = @EmpleadoID END";
                    using (SqlCommand cmdVerificar = new SqlCommand(queryExistencia, Conn))
                    {
                        cmdVerificar.Parameters.Add(new SqlParameter("@EmpleadoID", empleadoID));
                        int filasAfectadas = cmdVerificar.ExecuteNonQuery();
                        return filasAfectadas > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al borrar empleado: " + ex.Message);
                return false;
            }
        }

        #endregion

    }
}

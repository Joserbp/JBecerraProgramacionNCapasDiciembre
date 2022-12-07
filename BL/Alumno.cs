using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class Alumno
    {
        static public ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();
            try {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;

                    cmd.CommandText = "INSERT INTO [Almno]([Nombre],[ApellidoPaterno],[ApellidoMaterno],[Grado])VALUES (@Nombre,@ApellidoPaterno,@ApellidoMaterno,@Grado)";

                    SqlParameter[] collection = new SqlParameter[4];
                    collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                    collection[0].Value = alumno.Nombre;
                    collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                    collection[1].Value = alumno.ApellidoPaterno;
                    collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                    collection[2].Value = alumno.ApellidoMaterno;
                    collection[3] = new SqlParameter("Grado", SqlDbType.TinyInt);
                    collection[3].Value = alumno.Grado;

                    cmd.Parameters.AddRange(collection);

                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "Error al insertar el alumno";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        static public ML.Result Get()
        {
            ML.Result result = new ML.Result();
            try
            {
                using(SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = "SELECT [IdAlumno],[Nombre],[ApellidoPaterno],[ApellidoMaterno],[Grado] FROM [Alumno]";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable alumnoTable = new DataTable();

                    da.Fill(alumnoTable);

                    if(alumnoTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach(DataRow row in alumnoTable.Rows)
                        {
                            ML.Alumno alumno = new ML.Alumno();
                            alumno.IdAlumno = int.Parse(row[0].ToString());
                            alumno.Nombre = row[1].ToString();
                            alumno.ApellidoPaterno = row[2].ToString();
                            alumno.ApellidoMaterno = row[3].ToString();
                            alumno.Grado = byte.Parse(row[4].ToString());

                            result.Objects.Add(alumno);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No contiene registros la tabla Alumno";
                    }

                }


            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}

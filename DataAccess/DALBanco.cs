using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DALBanco
    {

        const string QueryListBanco = "select IDBanco,Nombre,Direccion,FechaRegistro from Banco where (@Nombre is null or Nombre like '%'+@Nombre+'%')";
        const string QuerySelectBanco = "select IDBanco,Nombre,Direccion,FechaRegistro from Banco where IDBanco=@IDBanco";
        const string QueryInsertBanco = "insert into Banco (Nombre,Direccion,FechaRegistro) values (@Nombre,@Direccion,@FechaRegistro)";
        const string QueryUpdateBanco = "update Banco set Nombre=@Nombre,Direccion=@Direccion,FechaRegistro=@FechaRegistro  where IDBanco=@IDBanco";
        const string QueryDeleteBanco = "delete from Banco where IDBanco=@IDBanco";

        string Conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

        public Banco SelectBanco(int prmIDBanco)
        {
            Banco _Item=new Banco();

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QuerySelectBanco, connection))
                {

                    command.Prepare();

                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmIDBanco, ParameterName = "@IDBanco" });

                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDBanco = reader.GetOrdinal("IDBanco");
                            int Nombre = reader.GetOrdinal("Nombre");
                            int Direccion = reader.GetOrdinal("Direccion");
                            int FechaRegistro = reader.GetOrdinal("FechaRegistro");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new Banco();

                                if (Header[IDBanco] != null)
                                {
                                    _Item.IDBanco = Convert.ToInt32(Header[IDBanco]);
                                }

                                if (Header[Nombre] != null)
                                {
                                    _Item.Nombre = Convert.ToString(Header[Nombre]);
                                }

                                if (Header[Direccion] != null)
                                {
                                    _Item.Direccion = Convert.ToString(Header[Direccion]);
                                }

                                if (Header[FechaRegistro] != null)
                                {
                                    _Item.FechaRegistro = Convert.ToDateTime(Header[FechaRegistro]);
                                }

                            }
                            reader.Close();

                        }
                    }
                }
            }

            return _Item;

        }

        public List<Banco> ListBanco(string prmNombre)
        {
            List<Banco> Result = new List<Banco>();
            Banco _Item;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryListBanco, connection))
                {

                    command.Prepare();

                    if (string.IsNullOrEmpty(prmNombre))
                    {
                        command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 128, Value = DBNull.Value, ParameterName = "@Nombre" });
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 128, Value = prmNombre, ParameterName = "@Nombre" });
                    }
                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDBanco = reader.GetOrdinal("IDBanco");
                            int Nombre = reader.GetOrdinal("Nombre");
                            int Direccion = reader.GetOrdinal("Direccion");
                            int FechaRegistro = reader.GetOrdinal("FechaRegistro");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new Banco();

                                if (Header[IDBanco] != null)
                                {
                                    _Item.IDBanco = Convert.ToInt32(Header[IDBanco]);
                                }

                                if (Header[Nombre] != null)
                                {
                                    _Item.Nombre = Convert.ToString(Header[Nombre]);
                                }

                                if (Header[Direccion] != null)
                                {
                                    _Item.Direccion = Convert.ToString(Header[Direccion]);
                                }

                                if (Header[FechaRegistro] != null)
                                {
                                    _Item.FechaRegistro = Convert.ToDateTime(Header[FechaRegistro]);
                                }

                                Result.Add(_Item);

                            }
                            reader.Close();

                        }
                    }
                }
            }

            return Result;

        }

        public int InsertBanco(Banco prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryInsertBanco, connection))
                {

                    command.Prepare();

                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 128, Value = prmInput.Nombre, ParameterName = "@Nombre" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 256, Value = prmInput.Direccion, ParameterName = "@Direccion" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.DateTime, Value = prmInput.FechaRegistro, ParameterName = "@FechaRegistro" });

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int UpdateBanco(Banco prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryUpdateBanco, connection))
                {

                    command.Prepare();

                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 128, Value = prmInput.Nombre, ParameterName = "@Nombre" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 256, Value = prmInput.Direccion, ParameterName = "@Direccion" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.DateTime, Value = prmInput.FechaRegistro, ParameterName = "@FechaRegistro" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmInput.IDBanco, ParameterName = "@IDBanco" });

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }


        public int DeleteBanco(int prmIDBanco)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryDeleteBanco, connection))
                {

                    command.Prepare();
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmIDBanco, ParameterName = "@IDBanco" });

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }


    }
}

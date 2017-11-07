using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Entities.Entities;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DALSucursales
    {
        const string QueryListBanco = "select IDBanco,Nombre from Banco ";
        const string QueryListSucursal = "SELECT IDSucursales, s.Nombre, s.Direccion, s.FechaRegistro, s.IDBanco, b.Nombre 'BancoName' FROM dbo.Sucursales s INNER JOIN dbo.Banco b ON s.IDBanco = b.IDBanco";
        const string QuerySelectSucursal = "SELECT s.IDSucursales, s.Nombre, s.Direccion, s.FechaRegistro, s.IDBanco, b.Nombre 'BancoName' FROM dbo.Sucursales s INNER JOIN dbo.Banco b ON s.IDBanco = b.IDBanco WHERE IDSucursales = @IDSucursales";
        const string QueryInsertSucursal = "INSERT INTO dbo.Sucursales( Nombre ,Direccion ,IDBanco)VALUES ( @Nombre,@Direccion,@IDBanco)";
        const string QueryUpdateSucursal = "UPDATE dbo.Sucursales SET Nombre = @Nombre, Direccion = @Direccion, FechaRegistro = GETDATE(), IDBanco=@IDBanco WHERE IDSucursales= @IDSucursales";
        const string QueryDeleteSucursal = "DELETE FROM dbo.Sucursales WHERE IDSucursales = @IDSucursales";
        const string QueryListSucursalByBanco = "SELECT IDSucursales, Nombre FROM dbo.Sucursales WHERE (@IDBanco=0 or IDBanco = @IDBanco)";

        string Conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

        public List<Banco> ListBanco()
        {
            List<Banco> Result = new List<Banco>();
            Banco _Item;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryListBanco, connection))
                {
                    command.Prepare();
                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDBanco = reader.GetOrdinal("IDBanco");
                            int Nombre = reader.GetOrdinal("Nombre");

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
                                Result.Add(_Item);
                            }
                            reader.Close();
                        }
                    }
                }
            }

            return Result;

        }

        public Sucursales SelectSucursal(int prmIDSucursal)
        {
            Sucursales _Item = new Sucursales();

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySelectSucursal, connection))
                {
                    command.Prepare();
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmIDSucursal, ParameterName = "@IDSucursales" });

                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDSucursales = reader.GetOrdinal("IDSucursales");
                            int Nombre = reader.GetOrdinal("Nombre");
                            int Direccion = reader.GetOrdinal("Direccion");
                            int FechaRegistro = reader.GetOrdinal("FechaRegistro");
                            int IDBanco = reader.GetOrdinal("IDBanco");
                            int BancoName = reader.GetOrdinal("BancoName");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new Sucursales();

                                if (Header[IDSucursales] != null)
                                {
                                    _Item.IDSucursales = Convert.ToInt32(Header[IDSucursales]);
                                }
                                if (Header[IDBanco] != null)
                                {
                                    _Item.IDBanco = Convert.ToInt32(Header[IDBanco]);
                                }
                                if (Header[BancoName] != null)
                                {
                                    _Item.BancoName = Convert.ToString(Header[BancoName]);
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

        public List<Sucursales> ListSucursal(string prmNombre)
        {
            List<Sucursales> Result = new List<Sucursales>();
            Sucursales _Item;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryListSucursal, connection))
                {
                    command.Prepare();
                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDSucursales = reader.GetOrdinal("IDSucursales");
                            int IDBanco = reader.GetOrdinal("IDBanco");
                            int Nombre = reader.GetOrdinal("Nombre");
                            int Direccion = reader.GetOrdinal("Direccion");
                            int FechaRegistro = reader.GetOrdinal("FechaRegistro");
                            int BancoName = reader.GetOrdinal("BancoName");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new Sucursales();

                                if (Header[IDSucursales] != null)
                                {
                                    _Item.IDSucursales = Convert.ToInt32(Header[IDSucursales]);
                                }
                                if (Header[IDBanco] != null)
                                {
                                    _Item.IDBanco = Convert.ToInt32(Header[IDBanco]);
                                }
                                if (Header[BancoName] != null)
                                {
                                    _Item.BancoName = Convert.ToString(Header[BancoName]);
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

        public List<Sucursales> ListSucursalByBanco(int prmIDBanco)
        {
            List<Sucursales> Result = new List<Sucursales>();
            Sucursales _Item;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryListSucursalByBanco, connection))
                {
                    command.Prepare();
                    command.Parameters.AddWithValue("@IDBanco", prmIDBanco);
                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDSucursales = reader.GetOrdinal("IDSucursales");                            
                            int Nombre = reader.GetOrdinal("Nombre");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new Sucursales();

                                if (Header[IDSucursales] != null)
                                {
                                    _Item.IDSucursales = Convert.ToInt32(Header[IDSucursales]);
                                }
                                if (Header[Nombre] != null)
                                {
                                    _Item.Nombre = Convert.ToString(Header[Nombre]);
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

        public int InsertSucursal(Sucursales prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryInsertSucursal, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@IDBanco", prmInput.IDBanco);
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 128, Value = prmInput.Nombre, ParameterName = "@Nombre" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 256, Value = prmInput.Direccion, ParameterName = "@Direccion" });

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int UpdateSucursal(Sucursales prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryUpdateSucursal, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@IDSucursales", prmInput.IDSucursales);
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 128, Value = prmInput.Nombre, ParameterName = "@Nombre" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.String, Size = 256, Value = prmInput.Direccion, ParameterName = "@Direccion" });
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmInput.IDBanco, ParameterName = "@IDBanco" });

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int DeleteSucursal(int prmIDSucursal)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryDeleteSucursal, connection))
                {

                    command.Prepare();
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmIDSucursal, ParameterName = "@IDSucursales" });

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }
    }
}

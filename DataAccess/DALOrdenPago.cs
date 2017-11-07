using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DALOrdenPago
    {
        const string QueryListOrdenPago = "SELECT o.IDOrdenPago, o.Monto, o.IDMoneda, o.IDEstado, o.FechaPago, m.Nombre 'Moneda', ep.Nombre 'Estado' FROM dbo.OrdenPago o INNER JOIN dbo.Moneda m ON o.IDMoneda = m.IDMoneda INNER JOIN dbo.EstadoPago ep ON o.IDEstado = ep.IDEstado";
        const string QuerySelectOrdenPago = "SELECT o.IDOrdenPago, o.Monto, o.IDMoneda, o.IDEstado, o.FechaPago, m.Nombre 'Moneda', ep.Nombre 'Estado', sop.IDSucursales, b.IDBanco,s.Nombre 'Sucursal', b.Nombre 'Banco' FROM dbo.OrdenPago o INNER JOIN dbo.Moneda m ON o.IDMoneda = m.IDMoneda INNER JOIN dbo.EstadoPago ep ON o.IDEstado = ep.IDEstado LEFT JOIN dbo.SucursalesOrdenPago sop ON o.IDOrdenPago = sop.IDOrdenPago LEFT JOIN dbo.Sucursales s ON sop.IDSucursales = s.IDSucursales LEFT JOIN dbo.Banco b ON b.IDBanco = s.IDBanco WHERE o.IDOrdenPago = @IDOrdenPago ";
        const string QueryInsertOrdenPago = "INSERT INTO dbo.OrdenPago( Monto ,IDMoneda ,IDEstado ,FechaPago)VALUES  ( @Monto, @IDMoneda,@IDEstado,@FechaPago )";
        const string QueryUpdateOrdenPago = "UPDATE dbo.OrdenPago SET Monto=@Monto, IDMoneda=@IDMoneda, IDEstado=@IDEstado, FechaPago=@FechaPago  WHERE IDOrdenPago=@IDOrdenPago";
        const string QueryDeleteOrdenPago = "DELETE FROM dbo.OrdenPago WHERE IDOrdenPago=@IDOrdenPago";
        const string QueryInsertOrdenPagoSucursal = "INSERT INTO dbo.SucursalesOrdenPago( IDSucursales ,IDOrdenPago ,FechaPago)VALUES  ( @IDSucursales ,@IDOrdenPago , GETDATE() )";
        const string QueryDeleteOrdenPagoSucursal = "DELETE FROM dbo.SucursalesOrdenPago WHERE IDOrdenPago =@IDOrdenPago AND IDSucursales=IDSucursales  ";
        const string QuerySelectOrdenPagoBySucursalAndMoneda = "select o.IDOrdenPago, o.Monto, o.IDMoneda, o.IDEstado, o.FechaPago from dbo.OrdenPago o inner join dbo.SucursalesOrdenPago so on o.IDOrdenPago=so.IDOrdenPago where (@IDSucursales=0 or so.IDSucursales=@IDSucursales) and (@IDMoneda=0 or o.IDMoneda=@IDMoneda)";

        string Conexion = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;

        public OrdenPago SelectOrdenPago(int prmIDBanco)
        {
            OrdenPago _Item = new OrdenPago();

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySelectOrdenPago, connection))
                {
                    command.Prepare();
                    command.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Value = prmIDBanco, ParameterName = "@IDOrdenPago" });

                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDOrdenPago = reader.GetOrdinal("IDOrdenPago");
                            int Monto = reader.GetOrdinal("Monto");
                            int IDMoneda = reader.GetOrdinal("IDMoneda");
                            int IDEstado = reader.GetOrdinal("IDEstado");
                            int FechaPago = reader.GetOrdinal("FechaPago");
                            int Moneda = reader.GetOrdinal("Moneda");
                            int Estado = reader.GetOrdinal("Estado");
                            int IDSucursales = reader.GetOrdinal("IDSucursales");
                            int IDBanco = reader.GetOrdinal("IDBanco");
                            int Sucursal = reader.GetOrdinal("Sucursal");
                            int Banco = reader.GetOrdinal("Banco");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new OrdenPago();

                                if (Header[IDOrdenPago] != null)
                                {
                                    _Item.IDOrdenPago = Convert.ToInt64(Header[IDOrdenPago]);
                                }
                                if (Header[Monto] != null)
                                {
                                    _Item.Monto = Convert.ToDouble(Header[Monto]);
                                }
                                if (Header[IDMoneda] != null)
                                {
                                    _Item.IDMoneda = Convert.ToByte(Header[IDMoneda]);
                                }
                                if (Header[IDEstado] != null)
                                {
                                    _Item.IDEstado = Convert.ToByte(Header[IDEstado]);
                                }
                                if (Header[FechaPago] != null)
                                {
                                    _Item.FechaPago = Convert.ToDateTime(Header[FechaPago]);
                                }
                                if (Header[Moneda] != null)
                                {
                                    _Item.Moneda = Convert.ToString(Header[Moneda]);
                                }
                                if (Header[Estado] != null)
                                {
                                    _Item.Estado = Convert.ToString(Header[Estado]);
                                }
                                if (!(Header[IDSucursales] is DBNull))
                                {
                                    _Item.IDSucursales = Convert.ToInt32(Header[IDSucursales]);
                                }
                                if (!(Header[IDBanco] is DBNull))
                                {
                                    _Item.IDBanco = Convert.ToInt32(Header[IDBanco]);
                                }
                                if (!(Header[Sucursal] is DBNull))
                                {
                                    _Item.Sucursal = Convert.ToString(Header[Sucursal]);
                                }
                                if (!(Header[Banco] is DBNull))
                                {
                                    _Item.Banco = Convert.ToString(Header[Banco]);
                                }
                            }
                            reader.Close();
                        }
                    }
                }
            }
            return _Item;
        }

        public List<OrdenPago> SelectOrdenPagoBySucursalAndMoneda(OrdenPago prmInput)
        {
            List<OrdenPago> Result = new List<OrdenPago>();
            OrdenPago _Item;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QuerySelectOrdenPagoBySucursalAndMoneda, connection))
                {
                    command.Prepare();
                    command.Parameters.AddWithValue("@IDSucursales", prmInput.IDSucursales);
                    command.Parameters.AddWithValue("@IDMoneda", prmInput.IDMoneda);
                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDOrdenPago = reader.GetOrdinal("IDOrdenPago");
                            int Monto = reader.GetOrdinal("Monto");
                            int IDMoneda = reader.GetOrdinal("IDMoneda");
                            int IDEstado = reader.GetOrdinal("IDEstado");
                            int FechaPago = reader.GetOrdinal("FechaPago");                            

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new OrdenPago();
                                if (Header[IDOrdenPago] != null)
                                {
                                    _Item.IDOrdenPago = Convert.ToInt64(Header[IDOrdenPago]);
                                }
                                if (Header[Monto] != null)
                                {
                                    _Item.Monto = Convert.ToDouble(Header[Monto]);
                                }
                                if (Header[IDMoneda] != null)
                                {
                                    _Item.IDMoneda = Convert.ToByte(Header[IDMoneda]);
                                }
                                if (Header[IDEstado] != null)
                                {
                                    _Item.IDEstado = Convert.ToByte(Header[IDEstado]);
                                }
                                if (Header[FechaPago] != null)
                                {
                                    _Item.FechaPago = Convert.ToDateTime(Header[FechaPago]);
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

        public List<OrdenPago> ListOrdenPago()
        {
            List<OrdenPago> Result = new List<OrdenPago>();
            OrdenPago _Item;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryListOrdenPago, connection))
                {
                    command.Prepare();
                    using (SqlDataReader reader = command.ExecuteReader(System.Data.CommandBehavior.SingleResult))
                    {
                        if (reader.HasRows)
                        {
                            Object[] Header = new Object[reader.FieldCount];

                            int IDOrdenPago = reader.GetOrdinal("IDOrdenPago");
                            int Monto = reader.GetOrdinal("Monto");
                            int IDMoneda = reader.GetOrdinal("IDMoneda");
                            int IDEstado = reader.GetOrdinal("IDEstado");
                            int FechaPago = reader.GetOrdinal("FechaPago");
                            int Moneda = reader.GetOrdinal("Moneda");
                            int Estado = reader.GetOrdinal("Estado");

                            while (reader.Read())
                            {
                                reader.GetValues(Header);
                                _Item = new OrdenPago();
                                if (Header[IDOrdenPago] != null)
                                {
                                    _Item.IDOrdenPago = Convert.ToInt64(Header[IDOrdenPago]);
                                }
                                if (Header[Monto] != null)
                                {
                                    _Item.Monto = Convert.ToDouble(Header[Monto]);
                                }
                                if (Header[IDMoneda] != null)
                                {
                                    _Item.IDMoneda = Convert.ToByte(Header[IDMoneda]);
                                }
                                if (Header[IDEstado] != null)
                                {
                                    _Item.IDEstado = Convert.ToByte(Header[IDEstado]);
                                }
                                if (Header[FechaPago] != null)
                                {
                                    _Item.FechaPago = Convert.ToDateTime(Header[FechaPago]);
                                }
                                if (Header[Moneda] != null)
                                {
                                    _Item.Moneda = Convert.ToString(Header[Moneda]);
                                }
                                if (Header[Estado] != null)
                                {
                                    _Item.Estado = Convert.ToString(Header[Estado]);
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

        public int InsertOrdenPago(OrdenPago prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryInsertOrdenPago, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@Monto", prmInput.Monto);
                    command.Parameters.AddWithValue("@IDMoneda", prmInput.IDMoneda);
                    command.Parameters.AddWithValue("@IDEstado", prmInput.IDEstado);
                    command.Parameters.AddWithValue("@FechaPago", prmInput.FechaPago);

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int UpdateOrdenPago(OrdenPago prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryUpdateOrdenPago, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@Monto", prmInput.Monto);
                    command.Parameters.AddWithValue("@IDMoneda", prmInput.IDMoneda);
                    command.Parameters.AddWithValue("@IDEstado", prmInput.IDEstado);
                    command.Parameters.AddWithValue("@FechaPago", prmInput.FechaPago);
                    command.Parameters.AddWithValue("@IDOrdenPago", prmInput.IDOrdenPago);
                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int DeleteOrdenPago(long prmIDOrdenPago)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryDeleteOrdenPago, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@IDOrdenPago", prmIDOrdenPago);

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int InsertOrdenPagoSucursal(SucursalesOrdenPago prmInput)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryInsertOrdenPagoSucursal, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@IDSucursales", prmInput.IDSucursales);
                    command.Parameters.AddWithValue("@IDOrdenPago", prmInput.IDOrdenPago);

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }

        public int DeleteOrdenPagoSucursal(SucursalesOrdenPago prmIDOrdenPago)
        {
            int Result = 0; //success

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(QueryDeleteOrdenPagoSucursal, connection))
                {

                    command.Prepare();
                    command.Parameters.AddWithValue("@IDSucursales", prmIDOrdenPago.IDSucursales);
                    command.Parameters.AddWithValue("@IDOrdenPago", prmIDOrdenPago.IDOrdenPago);

                    command.ExecuteNonQuery();

                }
            }

            return Result;
        }
    }
}

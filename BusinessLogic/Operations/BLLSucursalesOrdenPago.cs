using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities.Entities;

namespace BusinessLogic.Operations
{
    public class BLLSucursalesOrdenPago
    {
        public int InsertOrdenPagoSucursal(SucursalesOrdenPago prmInput)
        {
            return new DALOrdenPago().InsertOrdenPagoSucursal(prmInput);
        }
        public int DeleteOrdenPagoSucursal(SucursalesOrdenPago prmInput)
        {
            return new DALOrdenPago().DeleteOrdenPagoSucursal(prmInput);
        }

        public List<OrdenPago> SelectOrdenPagoBySucursalAndMoneda(OrdenPago prmInput)
        {
            return new DALOrdenPago().SelectOrdenPagoBySucursalAndMoneda(prmInput);
        }
    } 
}

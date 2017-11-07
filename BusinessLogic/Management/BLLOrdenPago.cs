using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities.Entities;

namespace BusinessLogic.Management
{
    public class BLLOrdenPago
    {
        public List<OrdenPago> ListOrdenPago()
        {
            return new DALOrdenPago().ListOrdenPago().OrderBy(x => x.FechaPago).ToList();
        }

        public OrdenPago SelectOrdenPago(int prmIDOrdenPago)
        {
            return new DALOrdenPago().SelectOrdenPago(prmIDOrdenPago);
        }

        public int InsertOrdenPago(OrdenPago prmInput)
        {
            return new DALOrdenPago().InsertOrdenPago(prmInput);
        }

        public int UpdateOrdenPago(OrdenPago prmInput)
        {
            return new DALOrdenPago().UpdateOrdenPago(prmInput);
        }

        public int DeleteOrdenPago(long prmIDOrdenPago)
        {
            return new DALOrdenPago().DeleteOrdenPago(prmIDOrdenPago);
        }
    }
}

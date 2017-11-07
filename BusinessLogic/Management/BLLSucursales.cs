using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Entities.Entities;

namespace BusinessLogic.Management
{
    public class BLLSucursales
    {
        public List<Banco> ListBanco()
        {
            return new DALSucursales().ListBanco().OrderBy(x => x.Nombre).ToList();
        }
        public List<Sucursales> ListSucursal(string prmNombre = null)
        {
            return new DALSucursales().ListSucursal(prmNombre).OrderBy(x => x.Nombre).ToList();
        }
        public List<Sucursales> ListSucursalByBanco(int prmIDBanco)
        {
            return new DALSucursales().ListSucursalByBanco(prmIDBanco).OrderBy(x => x.Nombre).ToList();
        }
        public Sucursales SelectSucursal(int prmIDSucursal)
        {
            return new DALSucursales().SelectSucursal(prmIDSucursal);
        }

        public int InsertSucursal(Sucursales prmInput)
        {
            return new DALSucursales().InsertSucursal(prmInput);
        }

        public int UpdateSucursal(Sucursales prmInput)
        {
            return new DALSucursales().UpdateSucursal(prmInput);
        }

        public int DeleteSucursal(int prmIDBanco)
        {
            return new DALSucursales().DeleteSucursal(prmIDBanco);
        }
    }
}

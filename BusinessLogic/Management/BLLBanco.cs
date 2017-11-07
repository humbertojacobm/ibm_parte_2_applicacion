using DataAccess;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Management
{
    public class BLLBanco
    {
        public List<Banco> ListBanco(string prmNombre = null)
        {
            return new DALBanco().ListBanco(prmNombre).OrderBy(x => x.Nombre).ToList();
        }

        public Banco SelectBanco(int prmIDBanco)
        {
            return new DALBanco().SelectBanco(prmIDBanco);
        }

        public int InsertBanco(Banco prmInput)
        {
            return new DALBanco().InsertBanco(prmInput);
        }

        public int UpdateBanco(Banco prmInput)
        {
            return new DALBanco().UpdateBanco(prmInput);
        }

        public int DeleteBanco(int prmIDBanco)
        {
            return new DALBanco().DeleteBanco(prmIDBanco);
        }

    }
}

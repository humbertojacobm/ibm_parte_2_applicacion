using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class SucursalesOrdenPago
    {
        public int IDSucursales { get; set; }

        public long IDOrdenPago { get; set; }

        public DateTime FechaPago { get; set; }
    }
}

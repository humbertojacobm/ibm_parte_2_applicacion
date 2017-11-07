using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class OrdenPago
    {
        public long IDOrdenPago { get; set; }
        public double Monto { get; set; }

        public byte IDMoneda { get; set; }
        public byte IDEstado { get; set; }

        public DateTime FechaPago { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }

        public int IDSucursales { get; set; }
        public int IDBanco { get; set; }

        public string Sucursal { get; set; }
        public string Banco { get; set; }


    }
}

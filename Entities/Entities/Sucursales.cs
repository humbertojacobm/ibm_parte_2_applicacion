using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Sucursales
    {
        public int IDSucursales { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public int IDBanco { get; set; }

        public DateTime FechaRegistro { get; set; }
        public string BancoName { get; set; }
    }
}

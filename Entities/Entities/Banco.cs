using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Banco
    {
        public int IDBanco { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public DateTime FechaRegistro { get; set; }

    }
}

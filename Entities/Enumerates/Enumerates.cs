using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enumerates
{
    public static class Enumerates
    {
        public enum IDMoneda
        {
            soles=1,
            dolares=2
        }

        public enum IDEstadoOrdenPago
        {
            Pagada = 1,
            Declinada = 2,
            Fallida = 3,
            Anulada = 4,
        }

    }
}

using Parte02Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Parte02Aplicacion.Controllers
{
    public class OrdenesPagoBySucursalesController : ApiController
    {
        // GET: api/OrdenesPagoBySucursales
        [HttpGet]
        public List<OrdenPago> Get()
        {
            OrdenPago _OrdenPago = new OrdenPago();
            return _OrdenPago.SelectOrdenPagoBySucursalAndMoneda();
        }

        [Route("api/OrdenesPagoBySucursales/{prmMoneda}/{prmIDSucursal}")]
        // GET: api/OrdenesPagoBySucursales/5
        public List<OrdenPago> Get(byte prmMoneda, int prmIDSucursal)
        {
            OrdenPago _OrdenPago = new OrdenPago();
            return _OrdenPago.SelectOrdenPagoBySucursalAndMoneda(prmMoneda, prmIDSucursal);            
        }
        
    }
}

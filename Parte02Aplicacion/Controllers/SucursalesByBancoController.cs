using Parte02Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Parte02Aplicacion.Controllers
{
    public class SucursalesByBancoController : ApiController
    {
        // GET: api/SucursalesByBanco
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<Sucursal> objectToSerialize = new List<Sucursal>();
            OrdenPago _OrdenPago = new OrdenPago();
            objectToSerialize = _OrdenPago.ListSucursalesByBancoToModel();

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Sucursal>>(objectToSerialize,
              new System.Net.Http.Formatting.XmlMediaTypeFormatter
              {
                  UseXmlSerializer = false
              })
            };
        }

        [Route("api/SucursalesByBanco/{IDBanco}")]
        
        // GET: api/SucursalesByBanco/5
        public HttpResponseMessage Get(int IDBanco)
        {

            List<Sucursal> objectToSerialize = new List<Sucursal>();
            OrdenPago _OrdenPago = new OrdenPago();
            objectToSerialize = _OrdenPago.ListSucursalesByBancoToModel(IDBanco);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<List<Sucursal>>(objectToSerialize,
              new System.Net.Http.Formatting.XmlMediaTypeFormatter
              {
                  UseXmlSerializer = false
              })
            };
        }
        
    }
}

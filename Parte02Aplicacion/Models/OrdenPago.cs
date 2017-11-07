using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Management;
using BusinessLogic.Operations;
using System.Web.Mvc;
using Entities.Enumerates;

namespace Parte02Aplicacion.Models
{
    public class OrdenPago
    {
        public long IDOrdenPago { get; set; }
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Monto inválido")]
        [Range(0.1, 100)]
        //[Display(Name = "Monto (S/.)")]
        public double Monto { get; set; }
        [Required]
        public short IDMoneda { get; set; }
        [Required]
        public short IDEstado { get; set; }
        
        public enum EnumMoneda
        {            
            soles = 1,
            dolares = 2
        }
        
        public enum EnumEstado
        {
            Pagada = 1,
            Declinada = 2,
            Fallida = 3,
            Anulada = 4,
        }
        [Required]
        public EnumMoneda lstMoneda1 { get; set; }
        [Required]
        public EnumEstado lstEstado1 { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }
        public string Banco { get; set; }
        [Required]
        public int IDBanco { get; set; }        
        public string Sucursal { get; set; }
        [Range(1, 100)]
        [Required]
        public int IDSucursal { get; set; }

        public SelectList lstMoneda { get; set; }
        public SelectList lstEstado { get; set; }
        public SelectList lstBanco { get; set; }
        public SelectList lstSucursal { get; set; }

        public SelectList ListaBanco()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            BLLSucursales _BLLSucursal = new BLLSucursales();            
            listItem.AddRange(new SelectList(_BLLSucursal.ListBanco(), "IDBanco", "Nombre"));

            return new SelectList(listItem, "Value", "Text");
        }

        public SelectList ListaSucursal(int Banco)
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            BLLSucursales _BLLSucursal = new BLLSucursales();            
            listItem.AddRange(new SelectList(_BLLSucursal.ListSucursalByBanco(Banco), "IDBanco", "Nombre"));            
            return new SelectList(listItem, "Value", "Text");
        }

        public SelectList ListaMoneda()
        {
            IEnumerable<Enumerates.IDMoneda> monedas = Enum.GetValues(typeof(Enumerates.IDMoneda)).Cast<Enumerates.IDMoneda>();
            List<SelectListItem> listItem = new List<SelectListItem>();

            var tempMoneda = from item in monedas
                             select new
                             {
                                 Nombre = item,
                                 IDMoneda = ((byte)item).ToString()
                             };
            listItem.AddRange(new SelectList(tempMoneda, "IDMoneda", "Nombre"));

            return new SelectList(listItem, "Value", "Text");
        }

        public SelectList ListaEstado()
        {
            IEnumerable<Enumerates.IDEstadoOrdenPago> estados = Enum.GetValues(typeof(Enumerates.IDEstadoOrdenPago)).Cast<Enumerates.IDEstadoOrdenPago>();
            List<SelectListItem> listItem = new List<SelectListItem>();

            var temp = from item in estados
                       select new
                       {
                           Nombre = item.ToString(),
                           IDEstado = ((byte)item).ToString()
                       };
            listItem.AddRange(new SelectList(temp, "IDEstado", "Nombre"));

            return new SelectList(listItem, "Value", "Text");
        }

        public List<OrdenPago> ListOrdenPago()
        {
            List<OrdenPago> Result = new List<OrdenPago>();
            BLLOrdenPago _BLLOrdenPago = new BLLOrdenPago();
            List<Entities.Entities.OrdenPago> ListSucursal = _BLLOrdenPago.ListOrdenPago();
            foreach (var item in ListSucursal)
            {
                OrdenPago _item = new OrdenPago();
                _item.IDOrdenPago = item.IDOrdenPago;
                _item.Monto = item.Monto;
                _item.IDMoneda = item.IDMoneda;
                _item.IDEstado = item.IDEstado;
                _item.FechaPago = item.FechaPago;
                _item.Moneda = item.Moneda;
                _item.Estado = item.Estado;
                Result.Add(_item);
            }
            return Result;
        }

        public List<Entities.Entities.Sucursales> ListSucursalesByBanco(int IDBanco)
        {
            List<Entities.Entities.Sucursales> Result = new List<Entities.Entities.Sucursales>();
            BLLSucursales _BLLSucursal = new BLLSucursales();
            Result = _BLLSucursal.ListSucursalByBanco(IDBanco);
            return Result;
        }

        public List<Sucursal> ListSucursalesByBancoToModel(int IDBanco=0)
        {
            List<Sucursal> Result = new List<Sucursal>();
            List<Entities.Entities.Sucursales> Source = new List<Entities.Entities.Sucursales>();
            Source = ListSucursalesByBanco(IDBanco);
            foreach (var item in Source)
            {
                Result.Add(new Models.Sucursal() {IDSucursal=item.IDSucursales, Nombre=item.Nombre });
            }

            return Result;
        }

        public OrdenPago SelectOrdenPAgo(int prmIDOrdenPago)
        {
            Entities.Entities.OrdenPago _Entidad = new Entities.Entities.OrdenPago();
            OrdenPago _OrdenPago = new OrdenPago();

            _Entidad = new BLLOrdenPago().SelectOrdenPago(prmIDOrdenPago);
            _OrdenPago.IDOrdenPago = _Entidad.IDOrdenPago;
            _OrdenPago.Monto = _Entidad.Monto;
            _OrdenPago.IDMoneda = _Entidad.IDMoneda;
            _OrdenPago.IDEstado = _Entidad.IDEstado;
            _OrdenPago.FechaPago = _Entidad.FechaPago;
            _OrdenPago.Moneda = _Entidad.Moneda;
            _OrdenPago.Estado = _Entidad.Estado;
            _OrdenPago.IDBanco = _Entidad.IDBanco;
            _OrdenPago.IDSucursal = _Entidad.IDSucursales;

            _OrdenPago.Sucursal = _Entidad.Sucursal;
            _OrdenPago.Banco = _Entidad.Banco;
            return _OrdenPago;
        }

        public int InsertOrdenPago(OrdenPago Model)
        {
            Entities.Entities.OrdenPago _OrdenPago = new Entities.Entities.OrdenPago();
            _OrdenPago.IDOrdenPago = Model.IDOrdenPago;
            _OrdenPago.Monto = Model.Monto;
            _OrdenPago.IDMoneda = Convert.ToByte(Model.IDMoneda);
            _OrdenPago.IDEstado = Convert.ToByte(Model.IDEstado);
            _OrdenPago.FechaPago = Model.FechaPago;

            return new BLLOrdenPago().InsertOrdenPago(_OrdenPago);
        }

        public int UpdateOrdenPago(OrdenPago Model)
        {
            Entities.Entities.OrdenPago _OrdenPago = new Entities.Entities.OrdenPago();
            _OrdenPago.IDOrdenPago = Model.IDOrdenPago;
            _OrdenPago.Monto = Model.Monto;
            _OrdenPago.IDMoneda = Convert.ToByte(Model.IDMoneda);
            _OrdenPago.IDEstado = Convert.ToByte(Model.IDEstado);
            _OrdenPago.FechaPago = Model.FechaPago;

            return new BLLOrdenPago().UpdateOrdenPago(_OrdenPago);
        }

        public int DeleteOrdenPago(long prmIDOrdenPago)
        {
            return new BLLOrdenPago().DeleteOrdenPago(prmIDOrdenPago);
        }

        public int InsertOrdenPagoSucursal(OrdenPago Model)
        {
            Entities.Entities.SucursalesOrdenPago _Entity = new Entities.Entities.SucursalesOrdenPago();
            _Entity.IDOrdenPago = Model.IDOrdenPago;
            _Entity.IDSucursales = Model.IDSucursal;

            return new BLLSucursalesOrdenPago().InsertOrdenPagoSucursal(_Entity);
        }

        public int DeleteOrdenPagoSucursal(OrdenPago Model)
        {
            Entities.Entities.SucursalesOrdenPago _Entity = new Entities.Entities.SucursalesOrdenPago();
            _Entity.IDOrdenPago = Model.IDOrdenPago;
            _Entity.IDSucursales = Model.IDSucursal;

            return new BLLSucursalesOrdenPago().DeleteOrdenPagoSucursal(_Entity);
        }

        public List<OrdenPago> SelectOrdenPagoBySucursalAndMoneda(byte prmMoneda=0, int prmIDSucursal=0)
        {
            Entities.Entities.OrdenPago _Entity = new Entities.Entities.OrdenPago();
            _Entity.IDMoneda = prmMoneda;
            _Entity.IDSucursales = prmIDSucursal;

            List<Entities.Entities.OrdenPago> ResultList = new List<Entities.Entities.OrdenPago>();
            List<OrdenPago> ModelResultList = new List<OrdenPago>();

            ResultList = new BLLSucursalesOrdenPago().SelectOrdenPagoBySucursalAndMoneda(_Entity);

            foreach (var item in ResultList)
            {
                ModelResultList.Add(new OrdenPago() { IDOrdenPago = item.IDOrdenPago, Monto = item.Monto, IDEstado = item.IDEstado, FechaPago = item.FechaPago });
            }

            return ModelResultList;

        }

    }
}
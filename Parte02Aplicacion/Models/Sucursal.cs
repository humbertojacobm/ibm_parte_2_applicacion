using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BusinessLogic.Management;
using System.Web.Mvc;

namespace Parte02Aplicacion.Models
{
    public class Sucursal
    {
        public int IDSucursal { get; set; }
        [Required]
        public int IDBanco { get; set; }
        [Required]
        [MaxLength(128)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(250)]
        public string Direccion { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }
        [Display(Name = "Banco")]
        public string BancoName { get; set; }

        public Banco Banco { get; set; }
        
        public SelectList lstBanco { get; set; }


        public SelectList ListaBanco()
        {
            List<SelectListItem> listItem = new List<SelectListItem>();
            BLLSucursales _BLLSucursal = new BLLSucursales();
            //listItem.Add(new SelectListItem { Value = "0", Text = "Seleccione" });
            listItem.AddRange(new SelectList(_BLLSucursal.ListBanco(), "IDBanco", "Nombre"));

            return new SelectList(listItem, "Value", "Text");
        }

        public List<Sucursal> ListSucursal()
        {
            List<Sucursal> Result = new List<Sucursal>();
            BLLSucursales _BLLSucursal = new BLLSucursales();
            List<Entities.Entities.Sucursales> ListSucursal = _BLLSucursal.ListSucursal();
            foreach (var item in ListSucursal)
            {
                Sucursal _item = new Sucursal();
                _item.IDSucursal = item.IDSucursales;
                _item.IDBanco = item.IDBanco;
                _item.Nombre = item.Nombre;
                _item.Direccion = item.Direccion;
                _item.FechaRegistro = item.FechaRegistro;
                _item.BancoName = item.BancoName;
                Result.Add(_item);
            }
            return Result;
        }

        public Sucursal SelectSucursal(int prmIDSucursal)
        {
            Entities.Entities.Sucursales _Entidad = new Entities.Entities.Sucursales();
            Sucursal _Sucursal = new Sucursal();

            _Entidad = new BLLSucursales().SelectSucursal(prmIDSucursal);
            _Sucursal.IDSucursal = _Entidad.IDSucursales;
            _Sucursal.IDBanco = _Entidad.IDBanco;
            _Sucursal.Nombre = _Entidad.Nombre;
            _Sucursal.Direccion = _Entidad.Direccion;
            _Sucursal.FechaRegistro = _Entidad.FechaRegistro;
            _Sucursal.BancoName = _Entidad.BancoName;

            return _Sucursal;            
        }

        public int InsertSucursal(Sucursal Model)
        {
            Entities.Entities.Sucursales _Sucursales = new Entities.Entities.Sucursales();
            _Sucursales.IDBanco = Model.IDBanco;
            _Sucursales.Nombre = Model.Nombre;
            _Sucursales.Direccion = Model.Direccion;
            _Sucursales.FechaRegistro = Model.FechaRegistro;

            return new BLLSucursales().InsertSucursal(_Sucursales);
        }

        public int UpdateSucursal(Sucursal Model)
        {
            Entities.Entities.Sucursales _Sucursales = new Entities.Entities.Sucursales();
            _Sucursales.Nombre = Model.Nombre;
            _Sucursales.Direccion = Model.Direccion;
            _Sucursales.FechaRegistro = Model.FechaRegistro;
            _Sucursales.IDBanco = Model.IDBanco;
            _Sucursales.IDSucursales = Model.IDSucursal;

            return new BLLSucursales().UpdateSucursal(_Sucursales);
        }

        public int DeleteSucursal(int prmIDSucursal)
        {
            return new BLLSucursales().DeleteSucursal(prmIDSucursal);
        }

        public Sucursal()
        {

        }
    }
}
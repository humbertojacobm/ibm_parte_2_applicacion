using BusinessLogic.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Parte02Aplicacion.Models
{
    public class Banco
    {

        public int IDBanco { get; set; }
        [Required]
        [MaxLength(128)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(250)]
        public string Direccion { get; set; }
        [Required]
        public DateTime FechaRegistro { get; set; }

        public List<Banco> ListBanco()
        {
            List<Banco> Result = new List<Banco>();
            BLLBanco _BLLBanco = new BLLBanco();
            List<Entities.Entities.Banco> ListBanco = _BLLBanco.ListBanco();
            foreach (var item in ListBanco)
            {
                Banco _item = new Banco();
                _item.IDBanco = item.IDBanco;
                _item.Nombre = item.Nombre;
                _item.Direccion = item.Direccion;
                _item.FechaRegistro = item.FechaRegistro;
                Result.Add(_item);
            }
            return Result;
        }

        public Banco SelectBanco(int prmIDBanco)
        {
            Entities.Entities.Banco _Entidad = new Entities.Entities.Banco();
            Banco _Banco = new Banco();

            _Entidad=new BLLBanco().SelectBanco(prmIDBanco);
            _Banco.IDBanco = _Entidad.IDBanco;
            _Banco.Nombre = _Entidad.Nombre;
            _Banco.Direccion = _Entidad.Direccion;
            _Banco.FechaRegistro = _Entidad.FechaRegistro;

            return _Banco;


        }

        public int InsertBanco(Banco Model)
        {
            Entities.Entities.Banco _Banco = new Entities.Entities.Banco();
            _Banco.Nombre = Model.Nombre;
            _Banco.Direccion = Model.Direccion;
            _Banco.FechaRegistro = Model.FechaRegistro;

            return new BLLBanco().InsertBanco(_Banco);
        }        

        public int UpdateBanco(Banco Model)
        {
            Entities.Entities.Banco _Banco = new Entities.Entities.Banco();
            _Banco.Nombre = Model.Nombre;
            _Banco.Direccion = Model.Direccion;
            _Banco.FechaRegistro = Model.FechaRegistro;
            _Banco.IDBanco = Model.IDBanco;

            return new BLLBanco().UpdateBanco(_Banco);
        }

        public int DeleteBanco(int prmIDBanco)
        {
            return new BLLBanco().DeleteBanco(prmIDBanco);
        }

    }
}
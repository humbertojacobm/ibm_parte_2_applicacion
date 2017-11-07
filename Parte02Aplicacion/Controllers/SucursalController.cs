using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Management;
using Parte02Aplicacion.Models;

namespace Parte02Aplicacion.Controllers
{
    public class SucursalController : Controller
    {
        Sucursal _Sucursal = new Sucursal();
        // GET: Sucursal
        public ActionResult Index()
        {
            var Model = _Sucursal.ListSucursal();
            return View(Model);
        }        

        // GET: Sucursal/Create
        public ActionResult Create()
        {
            var Model = new Sucursal
            {
                lstBanco = _Sucursal.ListaBanco(),
                FechaRegistro = DateTime.Now
            };
            return View(Model);
        }

        // POST: Sucursal/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IDSucursal,IDBanco,Nombre,Direccion,FechaRegistro")] Sucursal _Sucursal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(_Sucursal);
                }
                try
                {
                    _Sucursal.InsertSucursal(_Sucursal);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw;
                }                
            }
            catch
            {
                return View();
            }
        }

        // GET: Sucursal/Edit/5
        public ActionResult Edit(int id)
        {
            var Model = _Sucursal.SelectSucursal(id);
            Model.lstBanco = _Sucursal.ListaBanco();
            
            return View(Model);
        }

        // POST: Sucursal/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IDSucursal,IDBanco,Nombre,Direccion,FechaRegistro")] Sucursal _Sucursal)
        {
            if (!ModelState.IsValid)
            {
                return View(_Sucursal);
            }
            try
            {
                // TODO: Add update logic here
                _Sucursal.UpdateSucursal(_Sucursal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sucursal/Delete/5
        public ActionResult Delete(int id)
        {
            var Model = _Sucursal.SelectSucursal(id);
            return View(Model);
        }

        // POST: Sucursal/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "IDSucursal,IDBanco,Nombre,Direccion,FechaRegistro")] Sucursal _Sucursal)
        {
            try
            {
                // TODO: Add delete logic here
                _Sucursal.DeleteSucursal(_Sucursal.IDSucursal);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

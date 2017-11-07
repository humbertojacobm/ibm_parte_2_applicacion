using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Management;
using Parte02Aplicacion.Models;
using Entities.Enumerates;
namespace Parte02Aplicacion.Controllers
{
    public class OrdenPagoController : Controller
    {
        OrdenPago _OrdenPago = new OrdenPago();

        // GET: OrdenPago
        public ActionResult Index()
        {
            var Model = _OrdenPago.ListOrdenPago();
            return View(Model);
        }

        // GET: OrdenPago/Create
        public ActionResult Create()
        {
            var Model = new OrdenPago
            {
                FechaPago = DateTime.Now,
                lstMoneda = _OrdenPago.ListaMoneda(),
                lstEstado = _OrdenPago.ListaEstado()
            };

            return View(Model);
        }

        // POST: OrdenPago/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IDOrdenPago,Monto,FechaPago,IDMoneda,IDEstado")] OrdenPago _OrdenPago)
        {
            try
            {
                ModelState.Remove("IDBanco");
                ModelState.Remove("IDSucursal"); 
                if (!ModelState.IsValid)
                {
                    _OrdenPago.lstEstado = _OrdenPago.ListaEstado();
                    _OrdenPago.lstMoneda = _OrdenPago.ListaMoneda();
                    return View(_OrdenPago);
                }
                try
                {
                    _OrdenPago.InsertOrdenPago(_OrdenPago);
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

        // GET: OrdenPago/Edit/5
        public ActionResult Edit(int id)
        {
            var Model = _OrdenPago.SelectOrdenPAgo(id);
            Model.lstEstado = _OrdenPago.ListaEstado();
            Model.lstMoneda = _OrdenPago.ListaMoneda();

            return View(Model);
        }

        // POST: OrdenPago/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IDOrdenPago,Monto,IDMoneda,IDEstado,FechaPago")] OrdenPago _OrdenPago)
        {
            ModelState.Remove("IDBanco");
            ModelState.Remove("IDSucursal");
            if (!ModelState.IsValid)
            {
                _OrdenPago.lstEstado = _OrdenPago.ListaEstado();
                _OrdenPago.lstMoneda = _OrdenPago.ListaMoneda();
                return View(_OrdenPago);
            }
            try
            {
                // TODO: Add update logic here
                _OrdenPago.UpdateOrdenPago(_OrdenPago);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrdenPago/Delete/5
        public ActionResult Delete(int id)
        {
            var Model = _OrdenPago.SelectOrdenPAgo(id);
            return View(Model);
        }

        // POST: OrdenPago/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "IDOrdenPago,Monto,IDMoneda,IDEstado,FechaPago")] OrdenPago _OrdenPago)
        {
            try
            {
                // TODO: Add delete logic here
                _OrdenPago.DeleteOrdenPago(_OrdenPago.IDOrdenPago);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: OrdenPago/AsignarSucursal/5
        public ActionResult AsignarSucursal(int id)
        {
            var Model = _OrdenPago.SelectOrdenPAgo(id);
            Model.lstBanco = _OrdenPago.ListaBanco();
            if (Model.IDSucursal > 0)
            {
                return RedirectToAction("DetailOrdenPagoSucursal", new { id = Model.IDOrdenPago });
            }
            if (Model.lstBanco.Count() == 0)
            {
                Model.lstSucursal = new SelectList(new List<SelectListItem>());
            }
            else
            {
                Model.lstSucursal = _OrdenPago.ListaSucursal(int.Parse(Model.lstBanco.FirstOrDefault().Value));
            }
            return View(Model);
        }

        // GET: OrdenPago/Sucursal/5
        [HttpPost]
        public ActionResult AsignarSucursal([Bind(Include = "IDOrdenPago,Monto,IDMoneda,IDEstado,FechaPago,IDBanco,IDSucursal")] OrdenPago _OrdenPago)
        {
            if (!ModelState.IsValid)
            {
                _OrdenPago.lstBanco = _OrdenPago.ListaBanco();
                _OrdenPago.lstSucursal = _OrdenPago.ListaSucursal(_OrdenPago.IDBanco);
                return View(_OrdenPago);
            }
            try
            {
                _OrdenPago.InsertOrdenPagoSucursal(_OrdenPago);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
        }

        public ActionResult FillSucursal(int Banco)
        {
            var data = _OrdenPago.ListSucursalesByBanco(Banco);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: OrdenPago/AsignarSucursal/5
        public ActionResult DetailOrdenPagoSucursal(int id)
        {
            var Model = _OrdenPago.SelectOrdenPAgo(id);
            return View(Model);
        }

        // GET: OrdenPago/AsignarSucursal/5
        [HttpPost]
        public ActionResult DetailOrdenPagoSucursal([Bind(Include = "IDOrdenPago,Monto,IDMoneda,IDEstado,FechaPago,IDBanco,IDSucursal")] OrdenPago _OrdenPago)
        {
            try
            {
                _OrdenPago.DeleteOrdenPagoSucursal(_OrdenPago);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
                throw;
            }
        }

    }
}

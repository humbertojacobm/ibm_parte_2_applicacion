using BusinessLogic.Management;
using Parte02Aplicacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Parte02Aplicacion.Controllers
{
    public class BancoController : Controller
    {
        Banco _Banco = new Banco();
        // GET: Banco
        public ActionResult Index()
        {            
            var Model = _Banco.ListBanco();
            return View(Model);
        }

        // GET: Banco/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Banco/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Banco/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "IDBanco,Nombre,Direccion,FechaRegistro")] Banco _Banco)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }   
                     
            try
            {
                // TODO: Add insert logic here
                _Banco.InsertBanco(_Banco);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Banco/Edit/5
        public ActionResult Edit(int id)
        {
            var Model = _Banco.SelectBanco(id);
            return View(Model);
        }

        // POST: Banco/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "IDBanco,Nombre,Direccion,FechaRegistro")] Banco _Banco)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                // TODO: Add update logic here
                _Banco.UpdateBanco(_Banco);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Banco/Delete/5
        public ActionResult Delete(int id)
        {
            var Model = _Banco.SelectBanco(id);
            return View(Model);
        }

        // POST: Banco/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "IDBanco,Nombre,Direccion,FechaRegistro")] Banco _Banco)
        {
            try
            {
                _Banco.DeleteBanco(_Banco.IDBanco);
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

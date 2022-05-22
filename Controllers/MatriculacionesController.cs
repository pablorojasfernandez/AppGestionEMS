﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;

namespace AppGestionEMS.Controllers
{
    [Authorize(Roles = "admin,profesor")]
    public class MatriculacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Matriculaciones
        public ActionResult Index()
        {
            var matriculaciones = db.Matriculaciones.Include(m => m.Curso).Include(m => m.Grupo).Include(m => m.User);
            return View(matriculaciones.ToList());
        }

        // GET: Matriculaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculaciones matriculaciones = db.Matriculaciones.Find(id);
            if (matriculaciones == null)
            {
                return HttpNotFound();
            }
            return View(matriculaciones);
        }

        // GET: Matriculaciones/Create
        public ActionResult Create()
        {
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo");
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Nombre");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            return View();
        }

        // POST: Matriculaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,CursoId,GrupoId,Fecha")] Matriculaciones matriculaciones)
        {
            if (ModelState.IsValid)
            {
                matriculaciones.Fecha = DateTime.Now.ToString();
                db.Matriculaciones.Add(matriculaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo", matriculaciones.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Nombre", matriculaciones.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", matriculaciones.UserId);
            return View(matriculaciones);
        }

        // GET: Matriculaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculaciones matriculaciones = db.Matriculaciones.Find(id);
            if (matriculaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo", matriculaciones.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Nombre", matriculaciones.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", matriculaciones.UserId);
            return View(matriculaciones);
        }

        // POST: Matriculaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,CursoId,GrupoId,Fecha")] Matriculaciones matriculaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matriculaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo", matriculaciones.CursoId);
            ViewBag.GrupoId = new SelectList(db.Grupos, "Id", "Nombre", matriculaciones.GrupoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", matriculaciones.UserId);
            return View(matriculaciones);
        }

        // GET: Matriculaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matriculaciones matriculaciones = db.Matriculaciones.Find(id);
            if (matriculaciones == null)
            {
                return HttpNotFound();
            }
            return View(matriculaciones);
        }

        // POST: Matriculaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matriculaciones matriculaciones = db.Matriculaciones.Find(id);
            db.Matriculaciones.Remove(matriculaciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
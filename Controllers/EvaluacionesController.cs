using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppGestionEMS.Models;
using Microsoft.AspNet.Identity;

namespace AppGestionEMS.Controllers
{
    [Authorize(Roles ="admin,profesor")]
    public class EvaluacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluaciones
        public ActionResult Index()
        {
            IQueryable<Evaluaciones> evaluaciones;
            if (User.IsInRole("profesor"))
            {
                string currentUserId = User.Identity.GetUserId();
                var grupos = from usuario in db.Users
                             join ad in db.AsignacionDocentes on usuario.Id equals ad.UserId
                             where usuario.Id == currentUserId
                             select ad.GrupoId;

                var alumnos = from usuario in db.Users
                              from u_r in usuario.Roles
                              join rol in db.Roles on u_r.RoleId equals rol.Id
                              join mat in db.Matriculaciones on usuario.Id equals mat.UserId
                              where rol.Name == "alumno" && grupos.Any(g => g == mat.GrupoId)
                              select usuario.Id;

                evaluaciones = db.Evaluaciones.Include(e => e.Curso).Include(e => e.User).Where(a => alumnos.Contains(a.UserId));
            }
            else
            {
                var alumnos = from usuario in db.Users
                              from u_r in usuario.Roles
                              join rol in db.Roles on u_r.RoleId equals rol.Id
                              join mat in db.Matriculaciones on usuario.Id equals mat.UserId
                              where rol.Name == "alumno"
                              select usuario.Id;

                evaluaciones = db.Evaluaciones.Include(e => e.Curso).Include(e => e.User).Where(a => alumnos.Contains(a.UserId));
            }

            return View(evaluaciones.ToList());
        }

        // GET: Evaluaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones);
        }

        // GET: Evaluaciones/Create
        public ActionResult Create()
        {
            if (User.IsInRole("profesor"))
            {
                string currentUserId = User.Identity.GetUserId();

                var grupos = from usuario in db.Users
                             join ad in db.AsignacionDocentes on usuario.Id equals ad.UserId
                             where usuario.Id == currentUserId
                             select ad.GrupoId;

                var alumnos = from usuario in db.Users
                              from u_r in usuario.Roles
                              join rol in db.Roles on u_r.RoleId equals rol.Id
                              join mat in db.Matriculaciones on usuario.Id equals mat.UserId
                              where rol.Name == "alumno" && grupos.Any(g => g == mat.GrupoId)
                              select usuario.UserName;

                ViewBag.UserId = new SelectList(db.Users.Where(u => alumnos.Contains(u.UserName)), "Id", "Name");
            }
            else
            {
                var alumnos = from usuario in db.Users
                              from u_r in usuario.Roles
                              join rol in db.Roles on u_r.RoleId equals rol.Id
                              join mat in db.Matriculaciones on usuario.Id equals mat.UserId
                              where rol.Name == "alumno"
                              select usuario.UserName;

                ViewBag.UserId = new SelectList(db.Users.Where(u => alumnos.Contains(u.UserName)), "Id", "Name");
            }
            

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo");
            return View();
        }

        // POST: Evaluaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,CursoId,Convocatoria,NotaMediaTeoria,NotaMediaPractica,NotaMediaFinal")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Evaluaciones.Add(evaluaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo", evaluaciones.CursoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", evaluaciones.UserId);
            return View(evaluaciones);
        }

        // GET: Evaluaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo", evaluaciones.CursoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", evaluaciones.UserId);
            return View(evaluaciones);
        }

        // POST: Evaluaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,CursoId,Convocatoria,NotaMediaTeoria,NotaMediaPractica,NotaMediaFinal")] Evaluaciones evaluaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evaluaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CursoId = new SelectList(db.Cursos, "Id", "Anyo", evaluaciones.CursoId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", evaluaciones.UserId);
            return View(evaluaciones);
        }

        // GET: Evaluaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            if (evaluaciones == null)
            {
                return HttpNotFound();
            }
            return View(evaluaciones);
        }

        // POST: Evaluaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evaluaciones evaluaciones = db.Evaluaciones.Find(id);
            db.Evaluaciones.Remove(evaluaciones);
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

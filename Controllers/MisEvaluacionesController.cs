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
    [Authorize(Roles = "alumno")]
    public class MisEvaluacionesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Evaluaciones
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var evaluaciones = db.Evaluaciones.Include(e => e.Curso).Include(e => e.User).Where(p => p.UserId == currentUserId);
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
    }
}

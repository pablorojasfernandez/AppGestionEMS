using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppGestionEMS.Models
{
    public class AsignacionDocente
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int CursoId { get; set; }
        public virtual Cursos Curso { get; set; }

        public int GrupoId { get; set; }
        public virtual Grupos Grupo { get; set; }
    }
}
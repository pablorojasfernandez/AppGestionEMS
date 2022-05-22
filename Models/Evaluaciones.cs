using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppGestionEMS.Models
{
    public class Evaluaciones
    {
        public enum ConvocatoriaType
        {
            Ordinaria,
            Extraordinaria
        }

        public int Id { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int CursoId { get; set; }
        public virtual Cursos Curso { get; set; }

        public ConvocatoriaType Convocatoria { get; set; }

        public float? NotaMediaTeoria { get; set; }

        public float? NotaMediaPractica { get; set; }

        public float? NotaMediaFinal { get; set; }
    }
}
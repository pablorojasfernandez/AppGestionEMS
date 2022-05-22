using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppGestionEMS.Models
{
    public class Grupos
    {
        public enum TipoGrupo
        {
            Mañana,
            Tarde
        }

        public int Id { get; set; }

        public string Nombre { get; set; }

        public TipoGrupo Tipo { get; set; }

        public int Creditos { get; set; }
    }
}
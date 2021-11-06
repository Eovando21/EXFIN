using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXFIN.Models
{
    public class AlumnoClass
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Carnet { get; set; }
        public int IdCurso { get; set; }
        public String Estado { get; set; }
    }
}
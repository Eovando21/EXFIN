using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class ConsultaAprobadoAPIController : ApiController
    {
        EFEntities bd = new EFEntities();

        public IHttpActionResult getConsultaActivo()
        {
            List<CURSO> lcurso = bd.CURSO.ToList();
            List<ALUMNOS> lalumnos = bd.ALUMNOS.ToList();
            List<NOTAS> lnotas = bd.NOTAS.ToList();


            var query = from c in lcurso
                        join a in lalumnos on c.ID equals a.IDCURSO into t1
                        from a in t1.DefaultIfEmpty()
                        join n in lnotas on c.ID equals n.IDCURSO into t2
                        from n in t2.Where(x => x.ESTADO.StartsWith("APROBADO"))
                        select new AprobadoClass { getcurso = c, getalumnos = a, getnotas = n };
            return Ok(query);
        }
    }
}

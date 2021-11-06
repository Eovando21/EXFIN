using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class NotasAPIController : ApiController
    {
        EFEntities bd = new EFEntities();

        public IHttpActionResult getNotas()
        {
            var result = bd.SP_NOTA(0,0,0,"Get").ToList();
            return Ok(result);
        }


        public IHttpActionResult InsertNotas(NOTAS nt)
        {
            var insertNot = bd.SP_NOTA(0,nt.NOTA,nt.IDCURSO,"Insert").ToList();
            return Ok(insertNot);
        }

        public IHttpActionResult getNotasId(int id)
        {
            var ntdetail = bd.SP_NOTA(id,0,0, "GetId").Select(x => new NotasClass()
            {
                Id = x.ID,
                Nota = Convert.ToInt32(x.NOTA),
                IdCurso = Convert.ToInt32(x.IDCURSO),
                Estado = x.ESTADO

            }).FirstOrDefault<NotasClass>();
            return Ok(ntdetail);
        }

        public IHttpActionResult Put(NotasClass nt)
        {
            var updatent = bd.SP_NOTA(nt.Id,nt.Nota,nt.IdCurso, "Update").ToList();
            return Ok(updatent);
        }
        public IHttpActionResult Delete(int id)
        {
            var deletetemp = bd.SP_NOTA(id,0,0, "Delete").Select(x => new NotasClass()
            {
                Id = x.ID,
                Nota = Convert.ToInt32(x.NOTA),
                IdCurso = Convert.ToInt32(x.IDCURSO),
                Estado = x.ESTADO
            }).FirstOrDefault<NotasClass>();

            bd.SaveChanges();
            return Ok();

        }
    }
}

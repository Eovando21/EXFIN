using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class CursosAPIController : ApiController
    {
        EFEntities bd = new EFEntities();

        public IHttpActionResult getCursos()
        {
            var result = bd.SP_CURSOS(0, "", "Get").ToList();
            return Ok(result);
        }


        public IHttpActionResult InsertCursos(CURSO cs)
        {
            var insertCur = bd.SP_CURSOS(0, cs.NOMBRE,"Insert").ToList();
            return Ok(insertCur);
        }

        public IHttpActionResult getCursosId(int id)
        {
            var csdetail = bd.SP_CURSOS(id, "", "GetId").Select(x => new CursoClass()
            {
                Id = x.ID,
                Nombre = x.NOMBRE

            }).FirstOrDefault<CursoClass>();
            return Ok(csdetail);
        }

        public IHttpActionResult Put(CursoClass cs)
        {
            var updatecs = bd.SP_CURSOS(cs.Id, cs.Nombre, "Update").ToList();
            return Ok(updatecs);
        }
        public IHttpActionResult Delete(int id)
        {
            var deletetemp = bd.SP_CURSOS(id, "", "Delete").Select(x => new CursoClass()
            {
                Id = x.ID,
                Nombre = x.NOMBRE
            }).FirstOrDefault<CursoClass>();

            bd.SaveChanges();
            return Ok();

        }
    }
}

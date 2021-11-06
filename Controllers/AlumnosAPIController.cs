using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class AlumnosAPIController : ApiController
    {
        EFEntities bd = new EFEntities();

        public IHttpActionResult getAlumno()
        {
            var result = bd.SP_ALUMNOS(0, "", "", "", 0, "", "Get").ToList();
            return Ok(result);
        }


        public IHttpActionResult InsertAlumnos(ALUMNOS al)
        {
            var insertCli = bd.SP_ALUMNOS(0, al.NOMBRE, al.APELLIDOS, al.CARNET, al.IDCURSO, al.ESTADO, "Insert").ToList();
            return Ok(insertCli);
        }

        public IHttpActionResult getAlumnosId(int id)
        {
            var aldetail = bd.SP_ALUMNOS(id, "", "", "", 0, "", "GetId").Select(x => new AlumnoClass()
            {
                Id = x.ID,
                Nombre = x.NOMBRE,
                Apellido = x.APELLIDOS,
                Carnet = x.CARNET,
                IdCurso = Convert.ToInt32(x.IDCURSO),
                Estado = x.ESTADO
            }).FirstOrDefault<AlumnoClass>();
            return Ok(aldetail);
        }

        public IHttpActionResult Put(AlumnoClass al)
        {
            var updateal = bd.SP_ALUMNOS(al.Id, al.Nombre, al.Apellido, al.Carnet, al.IdCurso, al.Estado, "Update").ToList();
            return Ok(updateal);
        }
        public IHttpActionResult Delete(int id)
        {
            var deletetemp = bd.SP_ALUMNOS(id, "", "", "", 0, "", "Delete").Select(x => new AlumnoClass()
            {
                Id = x.ID,
                Nombre = x.NOMBRE,
                Apellido = x.APELLIDOS,
                Carnet = x.CARNET,
                IdCurso = Convert.ToInt32(x.IDCURSO),
                Estado = x.ESTADO
            }).FirstOrDefault<AlumnoClass>();

            bd.SaveChanges();
            return Ok();

        }
    }
}

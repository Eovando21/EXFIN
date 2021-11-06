using EXFIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace EXFIN.Controllers
{
    public class ConsultaActivoController : Controller
    {
        // GET: ConsultaActivo
        public ActionResult Index()
        {
            IEnumerable<ConsultasClass> conobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/ConsultaActivoAPI");

            var consumeapi = hc.GetAsync("ConsultaActivoAPI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<ConsultasClass>>();
                displaydata.Wait();
                conobj = displaydata.Result;
            }
            return View(conobj);
        }
    }
}
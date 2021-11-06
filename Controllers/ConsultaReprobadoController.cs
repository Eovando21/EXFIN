using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class ConsultaReprobadoController : Controller
    {
        // GET: ConsultaReprobado
        public ActionResult Index()
        {
            IEnumerable<ReprobadoClass> conobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeapi = hc.GetAsync("ConsultaReprpobadoAPI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<ReprobadoClass>>();
                displaydata.Wait();
                conobj = displaydata.Result;
            }
            return View(conobj);
        }
    }
}
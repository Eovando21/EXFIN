using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class CursosController : Controller
    {
        // GET: Cursos
        public ActionResult Index()
        {
            IEnumerable<CURSO> csobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeapi = hc.GetAsync("CursosAPI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<CURSO>>();
                displaydata.Wait();
                csobj = displaydata.Result;
            }
            return View(csobj);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CURSO crs)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var insertCli = hc.PostAsJsonAsync<CURSO>("CursosAPI", crs);
            insertCli.Wait();

            var savedata = insertCli.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create");
        }

        public ActionResult Details(int id)
        {
            CursoClass objcrs = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeAPI = hc.GetAsync("CursosAPI?id=" + id.ToString());
            consumeAPI.Wait();

            var readdata = consumeAPI.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayCrsDetatails = readdata.Content.ReadAsAsync<CursoClass>();
                displayCrsDetatails.Wait();
                objcrs = displayCrsDetatails.Result;
            }
            return View(objcrs);
        }
        public ActionResult Edit(int id)
        {
            CursoClass objcrs = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeAPI = hc.GetAsync("CursosAPI?id=" + id.ToString());
            consumeAPI.Wait();

            var readdata = consumeAPI.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayCrsDetatails = readdata.Content.ReadAsAsync<CursoClass>();
                displayCrsDetatails.Wait();
                objcrs = displayCrsDetatails.Result;
            }
            return View(objcrs);
        }

        [HttpPost]
        public ActionResult Edit(CursoClass crs)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/CursosAPI");

            var updateal = hc.PutAsJsonAsync<CursoClass>("CursosAPI", crs);
            updateal.Wait();

            var savedata = updateal.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(crs);
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/CursosAPI");

            var deletecrs = hc.DeleteAsync("CursosAPI/" + id.ToString());
            deletecrs.Wait();

            var savedata = deletecrs.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }
    }
}
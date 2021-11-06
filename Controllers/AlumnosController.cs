using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class AlumnosController : Controller
    {
        // GET: Alumnos
        public ActionResult Index()
        {
            IEnumerable<ALUMNOS> alobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeapi = hc.GetAsync("AlumnosAPI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<ALUMNOS>>();
                displaydata.Wait();
                alobj = displaydata.Result;
            }
            return View(alobj);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ALUMNOS al)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var insertCli = hc.PostAsJsonAsync<ALUMNOS>("AlumnosAPI", al);
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
            AlumnoClass objal = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeAPI = hc.GetAsync("AlumnosAPI?id=" + id.ToString());
            consumeAPI.Wait();

            var readdata = consumeAPI.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayCliDetatails = readdata.Content.ReadAsAsync<AlumnoClass>();
                displayCliDetatails.Wait();
                objal = displayCliDetatails.Result;
            }
            return View(objal);
        }
        public ActionResult Edit(int id)
        {
            AlumnoClass objcli = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeAPI = hc.GetAsync("AlumnosAPI?id=" + id.ToString());
            consumeAPI.Wait();

            var readdata = consumeAPI.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayCliDetatails = readdata.Content.ReadAsAsync<AlumnoClass>();
                displayCliDetatails.Wait();
                objcli = displayCliDetatails.Result;
            }
            return View(objcli);
        }

        [HttpPost]
        public ActionResult Edit(AlumnoClass al)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/AlumnosAPI");

            var updateal = hc.PutAsJsonAsync<AlumnoClass>("AlumnosAPI", al);
            updateal.Wait();

            var savedata = updateal.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(al);
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/AlumnosAPI");

            var deleteal = hc.DeleteAsync("AlumnosAPI/" + id.ToString());
            deleteal.Wait();

            var savedata = deleteal.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");

        }
    }
}
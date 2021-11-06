using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using EXFIN.Models;

namespace EXFIN.Controllers
{
    public class NotasController : Controller
    {
        // GET: Notas
        public ActionResult Index()
        {
            IEnumerable<NOTAS> ntobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeapi = hc.GetAsync("NotasAPI");
            consumeapi.Wait();

            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<NOTAS>>();
                displaydata.Wait();
                ntobj = displaydata.Result;
            }
            return View(ntobj);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(NOTAS nt)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var insertCli = hc.PostAsJsonAsync<NOTAS>("NotasAPI", nt);
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
            NotasClass objnt = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeAPI = hc.GetAsync("NotasAPI?id=" + id.ToString());
            consumeAPI.Wait();

            var readdata = consumeAPI.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayCrsDetatails = readdata.Content.ReadAsAsync<NotasClass>();
                displayCrsDetatails.Wait();
                objnt = displayCrsDetatails.Result;
            }
            return View(objnt);
        }
        public ActionResult Edit(int id)
        {
            NotasClass objnt = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/");

            var consumeAPI = hc.GetAsync("NotasAPI?id=" + id.ToString());
            consumeAPI.Wait();

            var readdata = consumeAPI.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displayCrsDetatails = readdata.Content.ReadAsAsync<NotasClass>();
                displayCrsDetatails.Wait();
                objnt = displayCrsDetatails.Result;
            }
            return View(objnt);
        }

        [HttpPost]
        public ActionResult Edit(NotasClass nt)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/NotasAPI");

            var updateal = hc.PutAsJsonAsync<NotasClass>("NotasAPI", nt);
            updateal.Wait();

            var savedata = updateal.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(nt);
        }

        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44338/api/NotasAPI");

            var deletecrs = hc.DeleteAsync("NotasAPI/" + id.ToString());
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
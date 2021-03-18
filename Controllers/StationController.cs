using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VCubWatcher.Models;

namespace VCubWatcher.Controllers
{
    public class StationController : Controller
    {
        // GET: Site
        public ActionResult Carte()
        {
            return View();
        }
        public ActionResult Liste()
        {
            String url = "https://api.alexandredubois.com/vcub-backend/vcub.php";
            String json = new WebClient().DownloadString(url);

            List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(json);
            ViewData["Stations"] = stations;

            return View();
        }
    }
}
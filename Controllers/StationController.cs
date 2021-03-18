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
            ViewData["Stations"] = GetStations();

            return View();
        }
        public ActionResult Liste()
        {
            ViewData["Stations"] = GetStations();

            return View();
        }

        public static List<Station> GetStations()
        {
            String url = "https://api.alexandredubois.com/vcub-backend/vcub.php";
            String json = new WebClient().DownloadString(url);

            List<Station> stations = JsonConvert.DeserializeObject<List<Station>>(json);
            return stations;
        }
    }
}
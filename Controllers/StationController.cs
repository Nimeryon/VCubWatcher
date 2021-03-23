using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VCubWatcher.Models;

namespace VCubWatcher.Controllers
{
    public class StationController : Controller
    {
        public IActionResult Liste()
        {
            ViewData["Stations"] = GetStations();
            return View();
        }

        public IActionResult Carte()
        {
            ViewData["Stations"] = JsonConvert.SerializeObject(GetStations());
            return View();
        }

        public IActionResult HandleButtonClick(string name)
        {
            return Ok();
        }

        public static List<StationModel> GetStations()
        {
            String url = "https://api.alexandredubois.com/vcub-backend/vcub.php";
            String json = new WebClient().DownloadString(url);

            List<StationModel> stations = JsonConvert.DeserializeObject<List<StationModel>>(json);
            return stations;
        }
    }
}
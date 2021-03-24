using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VCubWatcher.Models;

namespace VCubWatcher.Controllers
{
    public class StationController : Controller
    {
        public IActionResult Liste()
        {
            ViewData["favoriteIds"] = GetCookie("favoriteIds");
            return View();
        }
        public IActionResult HandleButtonClickListe(string fav)
        {
            SetFavorites(fav);
            return RedirectToActionPermanent("Liste");
        }

        public IActionResult Carte()
        {
            ViewData["Stations"] = JsonConvert.SerializeObject(GetStations());
            ViewData["favoriteIds"] = JsonConvert.SerializeObject(GetCookie("favoriteIds"));
            return View();
        }

        public IActionResult HandleButtonClickCarte(string fav)
        {
            SetFavorites(fav);
            return RedirectToActionPermanent("Carte");
        }

        public void SetFavorites(string fav)
        {
            string favoriteIdsString = GetCookie("favoriteIds");
            if (favoriteIdsString != null)
            {
                string[] favoriteIds = favoriteIdsString.Split(",");
                if (Array.IndexOf(favoriteIds, fav) == -1)
                {
                    List<string> favoriteList = favoriteIds.ToList();
                    favoriteList.Add(fav);
                    SetCookie("favoriteIds", String.Join(",", favoriteList), Int32.MaxValue);
                }
                else
                {
                    List<string> favoriteList = favoriteIds.ToList();
                    favoriteList.Remove(fav);
                    SetCookie("favoriteIds", String.Join(",", favoriteList), Int32.MaxValue);
                }
            }
            else
            {
                SetCookie("favoriteIds", fav, Int32.MaxValue);
            }
        }

        public string? GetCookie(string key)
        {
            string request = Request.Cookies[key];
            return request;
        }

        public void SetCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }

        public static List<StationModel> GetStations()
        {
            try
            {
                String url = "https://api.alexandredubois.com/vcub-backend/vcub.php";
                String json = new WebClient().DownloadString(url);

                List<StationModel> stations = JsonConvert.DeserializeObject<List<StationModel>>(json);
                return stations;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
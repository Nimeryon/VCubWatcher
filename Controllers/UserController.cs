using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace VCubWatcher.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Favoris()
        {
            ViewData["favoriteIds"] = GetCookie("favoriteIds");
            return View();
        }

        public IActionResult HandleButtonClickFavoris(string fav)
        {
            SetFavorites(fav);
            return RedirectToActionPermanent("Favoris");
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
    }
}
using API.Models;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MCV.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UsuarioLogin usuarioLogin)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    var response = client.PostAsJsonAsync<UsuarioLogin>("Login", usuarioLogin);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<TokenResult>();
                        reader.Wait();
                        HttpCookie cookie = new HttpCookie("tecCookie", reader.Result.token);
                        cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }

                }


            }
            catch
            {
                return View();
            };
        }
    }
}
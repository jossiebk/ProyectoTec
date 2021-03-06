using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MCV.Controllers
{
    public class TiendasController : Controller
    {
        // GET: Tiendas
        public ActionResult Index()
        {
            List<Tienda> tiendas = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try
                {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("tiendas");
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<List<Tienda>>();
                        reader.Wait();
                        tiendas = reader.Result;
                    }
                    else
                    {
                        tiendas = new List<Tienda>();
                        ModelState.AddModelError(String.Empty, "No hay datos del API");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    tiendas = new List<Tienda>();
                }
            }
            return View(tiendas);
        }

        // GET: Tiendas/Details/5
        public ActionResult Details(int id)
        {
            Tienda tienda = GetTiendaByID(id);
            return View(tienda);
        }

        public Tienda GetTiendaByID(int id)
        {
            Tienda tienda = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try
                {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("tiendas/" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<Tienda>();
                        reader.Wait();
                        tienda = reader.Result;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    tienda = new Tienda();
                }
            }
            return tienda;
        }

        // GET: Tiendas/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Tiendas/Create
        [HttpPost]
        public ActionResult Create(Tienda newTienda)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    try
                    {
                        cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                        var response = client.PostAsJsonAsync<Tienda>("tiendas", newTienda);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    catch (Exception e)
                    {
                        return View();
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tiendas/Edit/5
        public ActionResult Edit(int id)
        {
            Tienda tienda = GetTiendaByID(id);
            return View(tienda);
        }

        // POST: Tiendas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tienda newTienda)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    try
                    {
                        cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                        var response = client.PutAsJsonAsync("tiendas/" + id, newTienda);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    catch (Exception e)
                    {
                        return View();
                    }
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Tiendas/Delete/5
        public ActionResult Delete(int id)
        {
            Tienda tienda = GetTiendaByID(id);
            return View(tienda);
        }

        // POST: Tiendas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    try
                    {
                        cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                        var response = client.DeleteAsync("tiendas/" + id);
                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        
                    }
                    catch (Exception e)
                    {
                        return View();
                    }
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}

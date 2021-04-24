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
    public class VentasController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            List<Venta> ventas = null;

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("ventas");
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<List<Venta>>();
                        reader.Wait();
                        ventas = reader.Result;
                    }
                    else
                    {
                        ventas = new List<Venta>();
                        ModelState.AddModelError(String.Empty, "No hay datos del API");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    ventas = new List<Venta>();
                }
            }
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public ActionResult Details(int id)
        {
            Venta venta = GetVentaByID(id);
            return View(venta);
        }

        public Venta GetVentaByID(int id)
        {
            Venta venta = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("ventas/" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<Venta>();
                        reader.Wait();
                        venta = reader.Result;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    venta = new Venta();
                }
            }
            return venta;
        }







        // GET: Ventas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        public ActionResult Create(Venta newVenta)
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
                        var response = client.PostAsJsonAsync<Venta>("ventas", newVenta);
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

        // GET: Ventas/Edit/5
        public ActionResult Edit(int id)
        {
            Venta venta = GetVentaByID(id);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Venta newVentas)
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
                        var response = client.PutAsJsonAsync("ventas/" + id, newVentas);
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

        // GET: Ventas/Delete/5
        public ActionResult Delete(int id)
        {
            Venta venta = GetVentaByID(id);
            return View(venta);
        }

        // POST: Ventas/Delete/5
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
                        var response = client.DeleteAsync("ventas/" + id);
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

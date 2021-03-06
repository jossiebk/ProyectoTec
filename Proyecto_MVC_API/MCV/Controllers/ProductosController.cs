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
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            List<Producto> productos = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try
                {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("productos");
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<List<Producto>>();
                        reader.Wait();
                        productos = reader.Result;
                    }
                    else
                    {
                        productos = new List<Producto>();
                        ModelState.AddModelError(String.Empty, "No hay datos del API");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    productos = new List<Producto>();
                }
            }
            return View(productos);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            Producto productos = GetProductoByID(id);
            return View(productos);
        }


        public Producto GetProductoByID(int id)
        {
            Producto productos = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try
                {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("productos/" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<Producto>();
                        reader.Wait();
                        productos = reader.Result;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    productos = new Producto();
                }
            }
            return productos;
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            return View();
        }





        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Producto newProducto)
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
                        var response = client.PostAsJsonAsync<Producto>("productos", newProducto);
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

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            Producto productos = GetProductoByID(id);
            return View(productos);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Producto newProducto)
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
                        var response = client.PutAsJsonAsync("productos/" + id, newProducto);
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

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            Producto productos = GetProductoByID(id);
            return View(productos);
        }

        // POST: Productos/Delete/5
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
                        var response = client.DeleteAsync("productos/" + id);
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

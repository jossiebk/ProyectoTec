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
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            List<Cliente> clientes = null;
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try
                {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("clientes");
                    response.Wait();

                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<List<Cliente>>();
                        reader.Wait();
                        clientes = reader.Result;
                    }
                    else
                    {
                        clientes = new List<Cliente>();
                        ModelState.AddModelError(String.Empty, "No hay datos del API");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    clientes = new List<Cliente>();
                }
            }
            return View(clientes);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int id)
        {
            Cliente cliente = GetClienteByID(id);
            return View(cliente);
        }


        public Cliente GetClienteByID(int id)
        {
            Cliente cliente = null;

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");

                try {
                    cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                    var response = client.GetAsync("clientes/" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var reader = result.Content.ReadAsAsync<Cliente>();
                        reader.Wait();
                        cliente = reader.Result;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                    cliente = new Cliente();
                }
            }
            return cliente;
        }



        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        [HttpPost]
        public ActionResult Create(Cliente newCliente)
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
                        var response = client.PostAsJsonAsync<Cliente>("clientes", newCliente);
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




        // GET: Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            Cliente cliente = GetClienteByID(id);
            return View(cliente);
        }


        // POST: Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cliente newCliente)
        {
            try
            {
                var cookieContainer = new CookieContainer();
                using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    try {
                        cookieContainer.Add(new Uri("https://" + Request.Url.Host.ToString()), new Cookie("tecCookie", Request.Cookies["tecCookie"].Value));
                        var response = client.PutAsJsonAsync("clientes/" + id, newCliente);
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

        // GET: Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            Cliente cliente = GetClienteByID(id);
            return View(cliente);
        }

        // POST: Clientes/Delete/5
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
                        var response = client.DeleteAsync("clientes/" + id);
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
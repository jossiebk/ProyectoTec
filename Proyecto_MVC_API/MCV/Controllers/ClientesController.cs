using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");
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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

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
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    var response = client.DeleteAsync("clientes/" + id);
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
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

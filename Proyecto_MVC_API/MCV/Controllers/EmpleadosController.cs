using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MCV.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        public ActionResult Index()
        {
            List<Empleado> empleados = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");
                var response = client.GetAsync("empleados");
                response.Wait();

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var reader = result.Content.ReadAsAsync<List<Empleado>>();
                    reader.Wait();
                    empleados = reader.Result;
                }
                else
                {
                    empleados = new List<Empleado>();
                    ModelState.AddModelError(String.Empty, "No hay datos del API");
                }
            }
            return View(empleados);
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int id)
        {
            Empleado empleados = GetProductoByID(id);
            return View(empleados);
        }


        public Empleado GetProductoByID(int id)
        {
            Empleado empleados = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44381/api/");
                var response = client.GetAsync("empleados/" + id);
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var reader = result.Content.ReadAsAsync<Empleado>();
                    reader.Wait();
                    empleados = reader.Result;
                }
            }
            return empleados;
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        [HttpPost]
        public ActionResult Create(Empleado newEmpleado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    var response = client.PostAsJsonAsync<Empleado>("empleados", newEmpleado);
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
        // GET: Empleados/Edit/5
        public ActionResult Edit(int id)
        {
            Empleado empleados = GetProductoByID(id);
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Empleado newEmpleado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    var response = client.PutAsJsonAsync("empleados/" + id, newEmpleado);
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

        // GET: Empleados/Delete/5
        public ActionResult Delete(int id)
        {
            Empleado empleados = GetProductoByID(id);
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44381/api/");

                    var response = client.DeleteAsync("empleados/" + id);
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

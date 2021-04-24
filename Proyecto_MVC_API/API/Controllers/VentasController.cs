using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using API.Models;

namespace API.Controllers
{
    public class VentasController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Ventas
        [Authorize]
        public IQueryable<Venta> GetVenta()
        {
            return db.Venta;
        }

        // GET: api/Ventas/5
        [ResponseType(typeof(Venta))]
        [Authorize]
        public IHttpActionResult GetVenta(int id)
        {
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            return Ok(venta);
        }

        // PUT: api/Ventas/5
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutVenta(int id, Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venta.Id)
            {
                return BadRequest();
            }

            db.Entry(venta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VentaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Ventas
        [ResponseType(typeof(Venta))]
        [Authorize]
        public IHttpActionResult PostVenta(Venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Venta.Add(venta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = venta.Id }, venta);
        }

        // DELETE: api/Ventas/5
        [ResponseType(typeof(Venta))]
        [Authorize]
        public IHttpActionResult DeleteVenta(int id)
        {
            Venta venta = db.Venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            db.Venta.Remove(venta);
            db.SaveChanges();

            return Ok(venta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VentaExists(int id)
        {
            return db.Venta.Count(e => e.Id == id) > 0;
        }
    }
}
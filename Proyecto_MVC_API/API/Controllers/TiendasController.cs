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
    public class TiendasController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/Tiendas
        [Authorize]
        public IQueryable<Tienda> GetTienda()
        {
            return db.Tienda;
        }

        // GET: api/Tiendas/5
        [ResponseType(typeof(Tienda))]
        [Authorize]
        public IHttpActionResult GetTienda(int id)
        {
            Tienda tienda = db.Tienda.Find(id);
            if (tienda == null)
            {
                return NotFound();
            }

            return Ok(tienda);
        }

        // PUT: api/Tiendas/5
        [ResponseType(typeof(void))]
        [Authorize]
        public IHttpActionResult PutTienda(int id, Tienda tienda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tienda.Id)
            {
                return BadRequest();
            }

            db.Entry(tienda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiendaExists(id))
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

        // POST: api/Tiendas
        [ResponseType(typeof(Tienda))]
        [Authorize]
        public IHttpActionResult PostTienda(Tienda tienda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tienda.Add(tienda);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tienda.Id }, tienda);
        }

        // DELETE: api/Tiendas/5
        [ResponseType(typeof(Tienda))]
        [Authorize]
        public IHttpActionResult DeleteTienda(int id)
        {
            Tienda tienda = db.Tienda.Find(id);
            if (tienda == null)
            {
                return NotFound();
            }

            db.Tienda.Remove(tienda);
            db.SaveChanges();

            return Ok(tienda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TiendaExists(int id)
        {
            return db.Tienda.Count(e => e.Id == id) > 0;
        }
    }
}
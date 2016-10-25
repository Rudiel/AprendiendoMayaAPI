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
using AprendiendoMayaAPI;
using AprendiendoMayaAPI.Models;

namespace AprendiendoMayaAPI.Controllers
{
    public class PalabrasController : ApiController
    {
        private AprendiendoMayaEntities db = new AprendiendoMayaEntities();

        // GET: api/Palabras
        public IQueryable<Palabra> GetPalabras()
        {
            return db.Palabras;
        }

        // GET: api/Palabras/5
        public HttpResponseMessage GetPalabra(String categoria)
        {
      
            List<Palabra> palabras = db.Palabras.Where(e => e.Categoria == categoria).ToList();

            List<ModelPalabra> modelPalabras = new List<ModelPalabra>();

            foreach (Palabra p in palabras) {
                ModelPalabra model = new ModelPalabra();
                model.PalabraMaya = p.PalabraMaya;
                model.PalabraEspanol = p.PalabraEspanol;
                model.PalabraIngles = p.PalabraIngles;
                model.Imagen = p.Imagen;
                model.Audio = p.Audio;
                model.PalabraNueva = p.PalabraNueva.Value;

                modelPalabras.Add(model);
            }

            return Request.CreateResponse(HttpStatusCode.OK, modelPalabras);

        }

        // PUT: api/Palabras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPalabra(int id, Palabra palabra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != palabra.ID_Palabra)
            {
                return BadRequest();
            }

            db.Entry(palabra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PalabraExists(id))
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

        // POST: api/Palabras
        [ResponseType(typeof(Palabra))]
        public IHttpActionResult PostPalabra(Palabra palabra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Palabras.Add(palabra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = palabra.ID_Palabra }, palabra);
        }

        // DELETE: api/Palabras/5
        [ResponseType(typeof(Palabra))]
        public IHttpActionResult DeletePalabra(int id)
        {
            Palabra palabra = db.Palabras.Find(id);
            if (palabra == null)
            {
                return NotFound();
            }

            db.Palabras.Remove(palabra);
            db.SaveChanges();

            return Ok(palabra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PalabraExists(int id)
        {
            return db.Palabras.Count(e => e.ID_Palabra == id) > 0;
        }
    }
}
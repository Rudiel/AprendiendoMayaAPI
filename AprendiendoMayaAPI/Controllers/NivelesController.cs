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
    public class NivelesController : ApiController
    {
        private AprendiendoMayaEntities db = new AprendiendoMayaEntities();

        // GET: api/Niveles
        public IQueryable<Nivele> GetNiveles()
        {
            return db.Niveles;
        }

        // GET: api/Niveles/5
        [ResponseType(typeof(Nivele))]
        public IHttpActionResult GetNivele(int id)
        {
            Nivele nivele = db.Niveles.Find(id);
            if (nivele == null)
            {
                return NotFound();
            }

            return Ok(nivele);
        }

        public HttpResponseMessage GetNiveles(long usuario, String categoria)
        {

            Categoria cate = db.Categorias.Where(e => e.Nombre.Equals(categoria) && e.ID_Usuario == usuario).FirstOrDefault();

            if (cate == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No existen niveles con esa categoria");

            List<Nivele> niveles = db.Niveles.Where(e => e.ID_Usuario == usuario && e.ID_Categoria == cate.ID_Categoria).ToList();

            List<ModelNivel> modelNiveles = new List<ModelNivel>();

            foreach (Nivele n in niveles)
            {
                ModelNivel modelNivel = new ModelNivel();
                modelNivel.Bloqueado = n.Bloqueado.Value;
                modelNivel.Nombre = n.nivel;


                modelNiveles.Add(modelNivel);
            }

            return Request.CreateResponse(HttpStatusCode.OK, modelNiveles);
        }

        // PUT: api/Niveles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNivele(int id, Nivele nivele)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nivele.ID_Nivel)
            {
                return BadRequest();
            }

            db.Entry(nivele).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NiveleExists(id))
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

        // POST: api/Niveles
        [ResponseType(typeof(Nivele))]
        public IHttpActionResult PostNivele(Nivele nivele)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Niveles.Add(nivele);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = nivele.ID_Nivel }, nivele);
        }

        // DELETE: api/Niveles/5
        [ResponseType(typeof(Nivele))]
        public IHttpActionResult DeleteNivele(int id)
        {
            Nivele nivele = db.Niveles.Find(id);
            if (nivele == null)
            {
                return NotFound();
            }

            db.Niveles.Remove(nivele);
            db.SaveChanges();

            return Ok(nivele);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NiveleExists(int id)
        {
            return db.Niveles.Count(e => e.ID_Nivel == id) > 0;
        }
    }
}
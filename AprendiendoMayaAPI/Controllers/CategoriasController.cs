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
    public class CategoriasController : ApiController
    {
        private AprendiendoMayaEntities db = new AprendiendoMayaEntities();

        // GET: api/Categorias
        public IQueryable<Categoria> GetCategorias()
        {
            return db.Categorias;
        }

        // GET: api/Categorias/5
        public HttpResponseMessage GetCategoria(long id)
        {
            List<Categoria> categorias = db.Categorias.Where(e => e.ID_Usuario == id).ToList();

            List<ModelCategoria> modelCategorias= new List<ModelCategoria>();

            foreach (Categoria c in categorias) {
                ModelCategoria model = new ModelCategoria();
                model.ID_Categoria = c.ID_Categoria;
                model.ID_Usuario = c.ID_Usuario.Value;
                model.Nombre = c.Nombre;
                model.NombreIngles = c.NombreIngles;
                model.Imagen = c.Imagen;
                model.Bloqueado = c.Bloqueado.Value;

                modelCategorias.Add(model);
            }

            return Request.CreateResponse(HttpStatusCode.OK, modelCategorias);
        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.ID_Categoria)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categoria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoria.ID_Categoria }, categoria);
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Categoria))]
        public IHttpActionResult DeleteCategoria(int id)
        {
            Categoria categoria = db.Categorias.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            db.SaveChanges();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categorias.Count(e => e.ID_Categoria == id) > 0;
        }
    }
}
﻿using System;
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
    public class PuntuacionesController : ApiController
    {
        private AprendiendoMayaEntities db = new AprendiendoMayaEntities();

        // GET: api/Puntuaciones
        public HttpResponseMessage GetPuntuaciones()
        {
            List<ModelPuntMax> puntmaxList = new List<ModelPuntMax>();
            List<Usuario> users = db.Usuarios.ToList();
            foreach (Usuario u in users)
            {
                List<Puntuacione> puntuaciones = db.Puntuaciones.Where(p => p.Usuario.ID_Usuario == u.ID_Usuario).ToList();
                int PuntuacionMaxima = 0;

                foreach (Puntuacione puntos in puntuaciones) {
                    PuntuacionMaxima += puntos.Puntuacion.Value;
                }

                ModelPuntMax puntMax = new ModelPuntMax();
                puntMax.Nombre = u.Nombre + " " + u.Apellido ;
                puntMax.Puntuacion = PuntuacionMaxima;

                puntmaxList.Add(puntMax);
            }

            return Request.CreateResponse(HttpStatusCode.OK, puntmaxList.OrderByDescending(p=> p.Puntuacion).Take(10));
        }

        // GET: api/Puntuaciones/5
        [ResponseType(typeof(Puntuacione))]
        public IHttpActionResult GetPuntuacione(int id)
        {
            Puntuacione puntuacione = db.Puntuaciones.Find(id);
            if (puntuacione == null)
            {
                return NotFound();
            }

            return Ok(puntuacione);
        }

        // PUT: api/Puntuaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPuntuacione(int id, Puntuacione puntuacione)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != puntuacione.ID_Puntuacion)
            {
                return BadRequest();
            }

            db.Entry(puntuacione).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntuacioneExists(id))
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

        // POST: api/Puntuaciones
        [ResponseType(typeof(Puntuacione))]
        public IHttpActionResult PostPuntuacione(Puntuacione puntuacione)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Puntuaciones.Add(puntuacione);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = puntuacione.ID_Puntuacion }, puntuacione);
        }


        // DELETE: api/Puntuaciones/5
        [ResponseType(typeof(Puntuacione))]
        public IHttpActionResult DeletePuntuacione(int id)
        {
            Puntuacione puntuacione = db.Puntuaciones.Find(id);
            if (puntuacione == null)
            {
                return NotFound();
            }

            db.Puntuaciones.Remove(puntuacione);
            db.SaveChanges();

            return Ok(puntuacione);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PuntuacioneExists(int id)
        {
            return db.Puntuaciones.Count(e => e.ID_Puntuacion == id) > 0;
        }
    }
}
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

namespace AprendiendoMayaAPI.Controllers
{
    public class UsuariosController : ApiController
    {
        private AprendiendoMayaEntities db = new AprendiendoMayaEntities();
        private List<Categoria> listCategorias = new List<Categoria>();

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios;
        }

        // GET: api/Usuarios/5
        public HttpResponseMessage GetUsuario(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No existen usuarios");
            }

            return Request.CreateResponse(HttpStatusCode.OK, "" + usuario.Apellido);
        }

        // PUT: api/Usuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(long id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.ID_Usuario)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        public HttpResponseMessage PostUsuario(Usuario usuario)
        {
            Usuario repetido = db.Usuarios.Where(e => e.ID_Usuario == usuario.ID_Usuario).FirstOrDefault();

            if (repetido != null)
                return Request.CreateResponse(HttpStatusCode.OK, "Usuario Repetido");

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Bad Request");

            db.Usuarios.Add(usuario);
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Ocurrió un error. Intente Nuevamente");

            }

            //Colores
            Categoria color = new Categoria();
            color.ID_Usuario = usuario.ID_Usuario;
            color.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/colores.png";
            color.Nombre = "Colores";
            color.NombreIngles = "Colors";
            color.Bloqueado = false;

            db.Categorias.Add(color);

            //Frutas 
            Categoria fruta = new Categoria();
            fruta.ID_Usuario = usuario.ID_Usuario;
            fruta.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/frutas.png";
            fruta.NombreIngles = "Fruits";
            fruta.Nombre = "Frutas";
            fruta.Bloqueado = false;

            db.Categorias.Add(fruta);

            //Animales
            Categoria animal = new Categoria();
            animal.ID_Usuario = usuario.ID_Usuario;
            animal.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/animales.png";
            animal.NombreIngles = "Animals";
            animal.Nombre = "Animales";
            animal.Bloqueado = false;

            db.Categorias.Add(animal);

            //Numeros
            Categoria numero = new Categoria();
            numero.ID_Usuario = usuario.ID_Usuario;
            numero.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/numeros.png";
            numero.NombreIngles = "Numbers";
            numero.Nombre = "Numeros";
            numero.Bloqueado = false;

            db.Categorias.Add(numero);

            //Body parts
            Categoria partes = new Categoria();
            partes.ID_Usuario = usuario.ID_Usuario;
            partes.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/partesCuerpo.png";
            partes.NombreIngles = "Body Parts";
            partes.Nombre = "Partes del Cuerpo";
            partes.Bloqueado = true;

            db.Categorias.Add(partes);

            //Family
            Categoria familia = new Categoria();
            familia.ID_Usuario = usuario.ID_Usuario;
            familia.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/familia.png";
            familia.NombreIngles = "Family";
            familia.Nombre = "Familia";
            familia.Bloqueado = true;

            db.Categorias.Add(familia);

            //Frases
            Categoria frases = new Categoria();
            frases.ID_Usuario = usuario.ID_Usuario;
            frases.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Categorias/frases.png";
            frases.NombreIngles = "Common Phrases";
            frases.Nombre = "Frases Comunes";
            frases.Bloqueado = true;

            db.Categorias.Add(frases);

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Ocurrió un error. Intente Nuevamente");

            }

            List<Categoria> categorias = db.Categorias.Where(e => e.ID_Usuario == usuario.ID_Usuario).ToList();

            for (int i = 0; i < categorias.Count; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    Nivele nivel = new Nivele();

                    if (j <= 1 )
                        nivel.Bloqueado = false;
                    else
                        nivel.Bloqueado = true;

                    switch (j) { 
                        case 0:
                            nivel.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Niveles/LeeYElige.png";
                            break;
                        case 1:
                            nivel.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Niveles/MiraYEscribe.png";
                            break;
                        case 2:
                            nivel.Imagen = "http://narumasolutions-001-site1.dtempurl.com/Imagenes/Niveles/EscuchaYElige.png";
                            break;
                    }

                    nivel.ID_Categoria = categorias.ElementAt(i).ID_Categoria;
                    nivel.ID_Usuario = usuario.ID_Usuario;
                    nivel.nivel = "Nivel " + (j + 1);

                    db.Niveles.Add(nivel);
                }
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Ocurrió un error. Intente Nuevamente");

            }

            List<Nivele> niveles = db.Niveles.Where(e => e.ID_Usuario == usuario.ID_Usuario).ToList();

            foreach (Nivele n in niveles) {
                Puntuacione puntuacion = new Puntuacione();
                puntuacion.ID_Usuario = usuario.ID_Usuario;
                puntuacion.ID_Nivel = n.ID_Nivel;
                puntuacion.ID_Categoria = n.ID_Categoria;
                puntuacion.Puntuacion = 0;

                db.Puntuaciones.Add(puntuacion);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Ocurrió un error. Intente Nuevamente");

            }


            return Request.CreateResponse(HttpStatusCode.OK, "Usuario Creado con éxito");
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(long id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(long id)
        {
            return db.Usuarios.Count(e => e.ID_Usuario == id) > 0;
        }
    }
}
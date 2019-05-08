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
using BlogPessoal.Web.Data.Contexto;
using BlogPessoal.Web.Models.CategoriasDeArtigo;

namespace BlogPessoal.API.Controllers
{
    public class CategoriaDeArtigosController : ApiController
    {
        private BlogPessoalContexto db = new BlogPessoalContexto();

        // GET: api/CategoriaDeArtigos
        public List<CategoriaDeArtigo> GetCategoriasDeArtigo()
        {
            return db.CategoriasDeArtigo.ToList();
        }

        // GET: api/CategoriaDeArtigos/5
        [ResponseType(typeof(CategoriaDeArtigo))]
        public IHttpActionResult GetCategoriaDeArtigo(int id)
        {
            CategoriaDeArtigo categoriaDeArtigo = db.CategoriasDeArtigo.Find(id);
            if (categoriaDeArtigo == null)
            {
                return NotFound();
            }

            return Ok(categoriaDeArtigo);
        }

        // PUT: api/CategoriaDeArtigos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategoriaDeArtigo(int id, CategoriaDeArtigo categoriaDeArtigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoriaDeArtigo.Id)
            {
                return BadRequest();
            }

            db.Entry(categoriaDeArtigo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaDeArtigoExists(id))
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

        // POST: api/CategoriaDeArtigos
        [ResponseType(typeof(CategoriaDeArtigo))]
        public IHttpActionResult PostCategoriaDeArtigo(CategoriaDeArtigo categoriaDeArtigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CategoriasDeArtigo.Add(categoriaDeArtigo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = categoriaDeArtigo.Id }, categoriaDeArtigo);
        }

        // DELETE: api/CategoriaDeArtigos/5
        [ResponseType(typeof(CategoriaDeArtigo))]
        public IHttpActionResult DeleteCategoriaDeArtigo(int id)
        {
            CategoriaDeArtigo categoriaDeArtigo = db.CategoriasDeArtigo.Find(id);
            if (categoriaDeArtigo == null)
            {
                return NotFound();
            }

            db.CategoriasDeArtigo.Remove(categoriaDeArtigo);
            db.SaveChanges();

            return Ok(categoriaDeArtigo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaDeArtigoExists(int id)
        {
            return db.CategoriasDeArtigo.Count(e => e.Id == id) > 0;
        }
    }
}
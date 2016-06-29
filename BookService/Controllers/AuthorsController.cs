using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookService.Models;
using BookService.Filters;
using BookService.Extensions;
using System;
using System.Collections.Generic;

namespace BookService.Controllers
{
    [AuthFilter]
    [AllowAnonymous]
    [RequireHttps]
    public class AuthorsController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        // GET: api/Authors
        public IEnumerable<Author> GetAuthors()
        {
            return Authors();
        }


        // GET: api/Authors/5
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> GetAuthor(int id)
        {
            Author author = await Authors().Where(a => a.Id == id).FirstAsync();
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.Id)
            {
                return BadRequest();
            }

            var existing = await Authors().Where(a => a.Id == id).FirstAsync();
            if (existing == null)
            {
                return NotFound();
            }

            existing.Name = author.Name;

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Authors
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = new Author
            {
                Name = author.Name,
                EnvironmentId = Request.GetUserAccessToken()
            };

            db.Authors.Add(entity);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = author.Id }, entity);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> DeleteAuthor(int id)
        {
            Author author = await Authors().Where(a => a.Id == id).FirstAsync();
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            await db.SaveChangesAsync();

            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IQueryable<Author> Authors()
        {
            return db.Authors.FilterEnvironment(Request.GetUserAccessToken());
        }

    }
}
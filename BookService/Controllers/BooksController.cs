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

namespace BookService.Controllers
{
    [AuthFilter]
    public class BooksController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            return Books().Select(b => new BookDTO()
            {
                Id = b.Id,
                Title = b.Title,
                AuthorName = b.Author.Name
            });
        }

        [ResponseType(typeof(BookDetailDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var book = await Books().Include(b => b.Author).SingleOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(MapToDetailDto(book));
        }              

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            var existing = await Books().Where(a => a.Id == id).FirstAsync();
            if (existing == null)
            {
                return NotFound();
            }

            existing.AuthorId = book.AuthorId; // todo: make sure author is from this environment

            existing.Title = book.Title;
            existing.Year = book.Year;
            existing.Genre = book.Genre;
            existing.Price = book.Price; 
            
            await db.SaveChangesAsync();
            
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            book.EnvironmentId = Request.GetUserAccessToken();

            db.Books.Add(book);
            await db.SaveChangesAsync();

            // Load author name
            db.Entry(book).Reference(x => x.Author).Load();

            var dto = new BookDTO()
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.Name,
            };

            return CreatedAtRoute("DefaultApi", new { id = book.Id }, dto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await Books().Where(a => a.Id == id).FirstAsync();
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(MapToDetailDto(book));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IQueryable<Book> Books()
        {
            return db.Books.FilterEnvironment(Request.GetUserAccessToken());
        }

        private static BookDetailDTO MapToDetailDto(Book b)
        {
            return new BookDetailDTO()
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Price = b.Price,
                AuthorName = b.Author.Name,
                Genre = b.Genre
            };
        }
    }
}
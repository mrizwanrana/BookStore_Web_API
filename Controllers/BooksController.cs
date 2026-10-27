using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStore_Web_API.Models;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookStore_Web_APIContext _context;
    public BooksController(BookStore_Web_APIContext context)
    {
        _context = context;
    }

    // GET: api/BookModel
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookModel>>> GetBookModel()
    {
        return await _context.BookModel.Include(x => x.Authors).Include(x => x.Price).ToListAsync();
    }

    // GET: api/BookModel/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookModel>> GetBookModel(int id)
    {
        var bookmodel = await _context.BookModel.FindAsync(id);

        if (bookmodel == null)
        {
            return NotFound();
        }

        return bookmodel;
    }

    // PUT: api/BookModel/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBookModel(int? id, BookModel bookmodel)
    {
        if (id != bookmodel.Id)
        {
            return BadRequest();
        }

        _context.Entry(bookmodel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookModelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/BookModel
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<BookModel>> PostBookModel(BookModel bookmodel)
    {
        _context.BookModel.Add(bookmodel);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBookModel", new { id = bookmodel.Id }, bookmodel);
    }

    // DELETE: api/BookModel/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookModel(int? id)
    {
        var bookmodel = await _context.BookModel.FindAsync(id);
        if (bookmodel == null)
        {
            return NotFound();
        }

        _context.BookModel.Remove(bookmodel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookModelExists(int? id)
    {
        return _context.BookModel.Any(e => e.Id == id);
    }
}

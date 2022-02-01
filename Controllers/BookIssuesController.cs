using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Library21.Data;
using Library21.Models;

namespace Library21.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookIssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<BookIssue> GetBookIssues()
        {
            return _context.BookIssues;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookIssue([FromRoute] int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookIssue = await _context.BookIssues.FindAsync(id);

            if (bookIssue == null)
            {
                return NotFound();
            }

            return Ok(bookIssue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookIssue([FromRoute] int? id, [FromBody] BookIssue bookIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bookIssue.BookIssueID)
            {
                return BadRequest();
            }

            _context.Entry(bookIssue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookIssueExists(id))
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

        [HttpPost]
        public async Task<IActionResult> PostBookIssue([FromBody] BookIssue bookIssue)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.BookIssues.Add(bookIssue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookIssue", new { id = bookIssue.BookIssueID }, bookIssue);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookIssue([FromRoute] int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookIssue = await _context.BookIssues.FindAsync(id);
            if (bookIssue == null)
            {
                return NotFound();
            }

            _context.BookIssues.Remove(bookIssue);
            await _context.SaveChangesAsync();

            return Ok(bookIssue);
        }

        private bool BookIssueExists(int? id)
        {
            return _context.BookIssues.Any(e => e.BookIssueID == id);
        }
    }
}
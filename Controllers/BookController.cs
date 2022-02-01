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
    [Route("Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ApplicationDbContext _ctx = null;
        public BookController(ApplicationDbContext context)
        {
            _ctx = context;
        }


        [HttpGet, Route("GetBooks")]
        public async Task<object> GetBook()
        {
            List<Book> book = null;
            try
            {
                using (_ctx)
                {
                    book = await _ctx.Books.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return book;
        }

        [HttpGet, Route("GetBook/{id}")]
        public async Task<Book> GetBook(int id)
        {
            Book book = null;
            try
            {
                using (_ctx)
                {
                    book = await _ctx.Books.FirstOrDefaultAsync(b => b.BookID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return book;
        }

        [HttpPost, Route("AddBook")]
        public async Task<object> AddBook(Book book)
        {
            object result = null; string message = "";
            if (book == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.Books.Add(book);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }


        [HttpPut, Route("UpdateBook")]
        public async Task<object> UpdateBook(Book book)
        {
            object result = null; string message = "";
            if (book == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.Books.FirstOrDefault(x => x.BookID == book.BookID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.BookName = book.BookName;
                        entityUpdate.BookPublisher = book.BookPublisher;
                        entityUpdate.Category = book.Category;

                        await _ctx.SaveChangesAsync();
                    }
                    message = "Entry Updated";
                }
                catch (Exception e)
                {
                    e.ToString();
                    message = "Entry Update Failed!!";
                }
                result = new
                {
                    message
                };
            }
            return result;
        }

        [HttpDelete, Route("DeleteBook")]
        public async Task<object> DeleteBook(Book book)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.Books.SingleOrDefault(x => x.BookID == book.BookID);
                    if (idToRemove != null)
                    {
                        _ctx.Books.Remove(idToRemove);
                        await _ctx.SaveChangesAsync();
                    }
                    message = "Deleted Successfully";
                }
                catch (Exception e)
                {
                    e.ToString();
                    message = "Error on Deleting!!";
                }
                result = new
                {
                    message
                };
            }
            return result;
        }
    }
}
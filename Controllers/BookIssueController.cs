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
    [Route("BookIssue")]
    [ApiController]
    public class BookIssueController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;

        public BookIssueController(ApplicationDbContext context)
        {
            _ctx = context;
        }

        [HttpGet, Route("GetBookIssues")]
        public async Task<object> GetBookIssue()
        {
            List<BookIssue> bookIssue = null;
            try
            {
                using (_ctx)
                {
                    bookIssue = await _ctx.BookIssues.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return bookIssue;
        }

        [HttpGet, Route("GetBookIssue/{id}")]
        public async Task<BookIssue> GetBookIssue(int id)
        {
            BookIssue bookIssue = null;
            try
            {
                using (_ctx)
                {
                    bookIssue = await _ctx.BookIssues.FirstOrDefaultAsync(b => b.BookIssueID == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return bookIssue;
        }

        [HttpPut, Route("PutBookIssue")]
        public async Task<object> PutBookIssue(BookIssue bookIssue)
        {
            object result = null; string message = "";
            if (bookIssue == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                try
                {
                    var entityUpdate = _ctx.BookIssues.FirstOrDefault(x => x.BookIssueID == bookIssue.BookIssueID);
                    if (entityUpdate != null)
                    {
                        entityUpdate.IssueDate = bookIssue.IssueDate;
                        entityUpdate.MemberAddress = bookIssue.MemberAddress;
                        entityUpdate.BookID = bookIssue.BookID;
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

        [HttpPost, Route("AddBookIssue")]
        public async Task<object> PostBookIssue(BookIssue bookIssue)
        {
            object result = null; string message = "";
            if (bookIssue == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                _ctx.BookIssues.Add(bookIssue);
                await _ctx.SaveChangesAsync();
                result = new
                {
                    message
                };
            }
            return result;
        }

        [HttpDelete, Route("DeleteBookIssue")]
        public async Task<object> DeleteBookIssue(BookIssue bookIssue)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                try
                {
                    var idToRemove = _ctx.BookIssues.SingleOrDefault(x => x.BookIssueID == bookIssue.BookIssueID);
                    if (idToRemove != null)
                    {
                        _ctx.BookIssues.Remove(idToRemove);
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

        private bool BookIssueExists(int? id)
        {
            return _ctx.BookIssues.Any(e => e.BookIssueID == id); 
        }
    }

}
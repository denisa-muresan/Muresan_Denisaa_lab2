using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muresan_Denisaa_lab2.Data;
using Muresan_Denisaa_lab2.Models;

namespace Muresan_Denisaa_lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context _context;

        public IndexModel(Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context context)
        {
            _context = context;
        }

        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync(int? authorId)
        {
            ViewData["AuthorID"] = new SelectList(await _context.Authors.ToListAsync(), "ID", "LastName");

            IQueryable<Book> booksQuery = _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher);

            if (authorId.HasValue)
            {
                booksQuery = booksQuery.Where(b => b.AuthorID == authorId.Value);
            }

            Book = await booksQuery.ToListAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muresan_Denisaa_lab2.Data;
using Muresan_Denisaa_lab2.Models;
using Muresan_Denisaa_lab2.Models;

namespace Muresan_Denisaa_lab2.Pages.Books
{
    [Authorize(Roles = "Admin")]
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context _context;

        public EditModel(Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book =  await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
PopulateAssisgnedCategoryData(_context, Book);

             var authorList = _context.Authors.Select (x => new
                {
                  x.ID,
                  FullName = x.LastName + " " + x.FirstName
                });
            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID","PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if ( id == null)
            {
                return NotFound();
            }

            var bookToUpdate = await _context.Book
               .Include(i => i.Publisher)
               .Include(i => i.BookCategories)
                   .ThenInclude(i => i.Category)
               .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Book>(
                bookToUpdate,
                "Book",
                 i => i.Title, i => i.Author,
                  i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssisgnedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Muresan_Denisaa_lab2.Data;
using Muresan_Denisaa_lab2.Models;

namespace Muresan_Denisaa_lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context _context;

        public CreateModel(Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var AuthorList = _context.Authors.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });

            ViewData["AuthorID"] = new SelectList(_context.Authors, "ID", "LastName");
            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID","PublisherName");

            var Book = new Book();
            Book.BookCategories = new List<BookCategory>();

            PopulateAssisgnedCategoryData(_context, Book);

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();
            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }
            Book.BookCategories = newBook.BookCategories;
            _context.Book.Add(Book);
              await _context.SaveChangesAsync();
             
              return RedirectToPage("./Index");
        }
    }
}

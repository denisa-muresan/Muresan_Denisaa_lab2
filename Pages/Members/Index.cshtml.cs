using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Muresan_Denisaa_lab2.Data;
using Muresan_Denisaa_lab2.Models;

namespace Muresan_Denisaa_lab2.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context _context;

        public IndexModel(Muresan_Denisaa_lab2.Data.Muresan_Denisaa_lab2Context context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Member = await _context.Member.ToListAsync();
        }
    }
}

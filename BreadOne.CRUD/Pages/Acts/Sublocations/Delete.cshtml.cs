using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BreadOne.CRUD.Data;
using BreadOne.CRUD.Models;

namespace BreadOne.CRUD.Pages.Acts.Sublocations
{
    public class DeleteModel : PageModel
    {
        private readonly BreadOne.CRUD.Data.ApplicationDbContext _context;

        public DeleteModel(BreadOne.CRUD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Sublocation Sublocation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Sublocations == null)
            {
                return NotFound();
            }

            var sublocation = await _context.Sublocations.FirstOrDefaultAsync(m => m.Id == id);

            if (sublocation == null)
            {
                return NotFound();
            }
            else 
            {
                Sublocation = sublocation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Sublocations == null)
            {
                return NotFound();
            }
            var sublocation = await _context.Sublocations.FindAsync(id);

            if (sublocation != null)
            {
                Sublocation = sublocation;
                _context.Sublocations.Remove(Sublocation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

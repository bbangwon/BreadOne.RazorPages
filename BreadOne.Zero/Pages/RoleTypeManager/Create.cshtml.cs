using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BreadOne.Zero.Data;
using BreadOne.Zero.Models;

namespace BreadOne.Zero.Pages.RoleTypeManager
{
    public class CreateModel : PageModel
    {
        private readonly BreadOne.Zero.Data.ApplicationDbContext _context;

        public CreateModel(BreadOne.Zero.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RoleType RoleType { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.RoleType == null || RoleType == null)
            {
                return Page();
            }

            _context.RoleType.Add(RoleType);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BreadOne.CRUD.Data;
using BreadOne.CRUD.Models;

namespace BreadOne.CRUD.Pages.Acts.Locations
{
    public class DeleteModel : PageModel
    {
        private readonly BreadOne.CRUD.Data.ApplicationDbContext _context;

        public DeleteModel(BreadOne.CRUD.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Location Location { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }

            var location = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }
            else 
            {
                Location = location;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Locations == null)
            {
                return NotFound();
            }
            var location = await _context.Locations.FindAsync(id);

            if (location != null)
            {
                Location = location;
                _context.Locations.Remove(Location);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

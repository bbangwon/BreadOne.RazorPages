﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BreadOne.CRUD.Data;
using BreadOne.CRUD.Models;

namespace BreadOne.CRUD.Pages.Acts.Locations
{
    public class EditModel : PageModel
    {
        private readonly BreadOne.CRUD.Data.ApplicationDbContext _context;

        public EditModel(BreadOne.CRUD.Data.ApplicationDbContext context)
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

            var location =  await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }
            Location = location;
           ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(Location.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LocationExists(int id)
        {
          return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

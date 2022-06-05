using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BreadOne.Website.Data;
using BreadOne.Website.Models;

namespace BreadOne.Website.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly BreadOne.Website.Data.ApplicationDbContext _context;

        public IndexModel(BreadOne.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Post != null)
            {
                Post = await _context.Post
                .Include(p => p.Blog).ToListAsync();
            }
        }
    }
}

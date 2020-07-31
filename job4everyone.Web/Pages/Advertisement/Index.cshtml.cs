using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using job4everyone.Data;
using job4everyone.Models;

namespace job4everyone.Web.Pages.Advertisement
{
    public class IndexModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;

        public IndexModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
        }

        public IList<job4everyone.Models.Advertisement> Advertisement { get;set; }

        public async Task OnGetAsync()
        {
            Advertisement = await _context.Advertisements
                .Include(a => a.Employer)
                .Include(a => a.JobPosition).ToListAsync();
        }
    }
}

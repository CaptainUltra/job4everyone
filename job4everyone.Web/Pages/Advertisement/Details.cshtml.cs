using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using job4everyone.Data;
using job4everyone.Models;
using Microsoft.AspNetCore.Authorization;

namespace job4everyone.Web.Pages.Advertisement
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;

        public DetailsModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
        }

        public job4everyone.Models.Advertisement Advertisement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advertisement = await _context.Advertisements
                .Include(a => a.Employer)
                .Include(a => a.JobPosition).FirstOrDefaultAsync(m => m.Id == id);

            if (Advertisement == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

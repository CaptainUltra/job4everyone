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

namespace job4everyone.Web.Pages.JobPosition
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;

        public DeleteModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public job4everyone.Models.JobPosition JobPosition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JobPosition = await _context.JobPositions.FirstOrDefaultAsync(m => m.Id == id);

            if (JobPosition == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JobPosition = await _context.JobPositions.FindAsync(id);

            if (JobPosition != null)
            {
                _context.JobPositions.Remove(JobPosition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

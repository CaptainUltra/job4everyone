using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using job4everyone.Data;
using job4everyone.Models;
using Microsoft.AspNetCore.Authorization;

namespace job4everyone.Web.Pages.JobPosition
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;

        public CreateModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public job4everyone.Models.JobPosition JobPosition { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobPositions.Add(JobPosition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

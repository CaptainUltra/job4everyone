using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using job4everyone.Data;
using job4everyone.Models;
using job4everyone.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace job4everyone.Web.Pages.Advertisement
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;
        private IAdvertisementService service;

        public CreateModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
            service = new AdvertisementService(_context);
        }

        public IActionResult OnGet()
        {
        ViewData["EmployerId"] = new SelectList(_context.Employers, "Id", "Id");
        ViewData["JobPositionId"] = new SelectList(_context.JobPositions, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public job4everyone.Models.Advertisement Advertisement { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userName = User.Identity.Name;
            
            service.CreateAdvertisement(Advertisement.Name, Advertisement.Description, Advertisement.Active, Advertisement.JobPositionId, userName);

            return RedirectToPage("./Index");
        }
    }
}

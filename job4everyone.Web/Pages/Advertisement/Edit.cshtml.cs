using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using job4everyone.Data;
using job4everyone.Models;
using job4everyone.Services;
using Microsoft.AspNetCore.Authorization;

namespace job4everyone.Web.Pages.Advertisement
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;
        private IAdvertisementService service;

        public EditModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
            service = new AdvertisementService(_context);
        }

        [BindProperty]
        public job4everyone.Models.Advertisement Advertisement { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Advertisement = await _context.Advertisements
                .Include(a => a.JobPosition).FirstOrDefaultAsync(m => m.Id == id);

            if (Advertisement == null)
            {
                return NotFound();
            }
           ViewData["JobPositionId"] = new SelectList(_context.JobPositions, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var advertisement = new job4everyone.Models.Advertisement() 
            {
                Name = Advertisement.Name,
                Description = Advertisement.Description,
                Active = Advertisement.Active,
                JobPosition = Advertisement.JobPosition
            };

            try
            {
                service.UpdateAdvertisement(Advertisement.Id, advertisement);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(Advertisement.Id))
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

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}

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
using Microsoft.EntityFrameworkCore;

namespace job4everyone.Web.Pages.Advertisement.AdvertisementCandidate
{
    public class AddCandidateModel : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;
        private ICandidateService service;
        private int id;

        public AddCandidateModel(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
            service = new CandidateService(_context);
        }

        [BindProperty]
        public int AdvertisementId { get; set; }
        [BindProperty]
        public job4everyone.Models.Candidate Candidate { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userName = User.Identity.Name;

            var advertisementCandidateService = new AdvertisementCandidateService(_context);
            var candidate = this._context.Candidates.FirstOrDefault(c => c.Email == Candidate.Email);

            if(candidate == null)
            {
                candidate = service.CreateCandidate(Candidate.FirstName, Candidate.LastName, Candidate.Email);
            }
            
            var advId = Convert.ToInt32(Request.Form["advId"]);

            advertisementCandidateService.AddCandidateToAdvertisement(advId, candidate.Id);

            return RedirectToPage("/Index");
        }
    }
}

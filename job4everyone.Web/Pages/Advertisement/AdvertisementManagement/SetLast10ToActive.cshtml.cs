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
using job4everyone.Services;

namespace job4everyone.Web.Pages.Advertisement.AdvertisementManagement
{
    [Authorize]
    public class SetLast10ToActive : PageModel
    {
        private readonly job4everyone.Data.Job4EveryoneDbContext _context;
        private IAdvertisementService service;

        public SetLast10ToActive(job4everyone.Data.Job4EveryoneDbContext context)
        {
            _context = context;
            this.service = new AdvertisementService(_context);
        }

        public async Task<IActionResult> OnPostAsync()
        {

            this.service.ChangeLast10AdvertisementsToActive(User.Identity.Name);

            return RedirectToPage("/Advertisement/MyAdvertisements");
        }
    }
}

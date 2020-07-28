using System;
using job4everyone.Data;
using job4everyone.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(job4everyone.Web.Areas.Identity.IdentityHostingStartup))]
namespace job4everyone.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Job4EveryoneDbContext>(options =>
                    options.UseMySql(Configuration.ConnectionString));

                services.AddDefaultIdentity<Employer>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<Job4EveryoneDbContext>();
            });
        }
    }
}
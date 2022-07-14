using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Platforms.Data.Models;
using System;
using System.Diagnostics;
using System.Linq;

namespace Services.Platforms.Data.Context
{
    public static class DataLoader
    {
        public static void Load(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = (AppDbContext)scope.ServiceProvider.GetRequiredService<DbContext>();

                if (!context.Platforms.Any())
                {
                    Debug.WriteLine("--- Creating new data ---");
                    context.Platforms.AddRange(
                        new Platform() { Name = ".NET", Publisher = "Microsoft", Cost = "Free" },
                        new Platform() { Name = "SQL EXPRESS", Publisher = "Microsoft", Cost = "Free" },
                        new Platform() { Name = "Kubernetes", Publisher = "Some company", Cost = "Free" }
                        );
                    context.SaveChanges();
                }
                else
                {
                    Debug.WriteLine("--- Data already exists ---");
                }
            }
        }
    }
}

using System;
using webapi_yotam.Data;
using webapi_yotam.Models;

namespace webapi_yotam.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using( var serviceScope =app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if(!context.Platforms.Any())
            {
                Console.WriteLine("--> seeding data..");
                Platform Platform1 = new Platform() { Name = "Dot net", Publisher = "Microsoft", Cost = "Soft" };
                Platform Platform2 = new Platform() { Name = "sql server", Publisher = "Microsoft", Cost = "Soft" };
                Platform Platform3 = new Platform() { Name = "Kubernetes", Publisher = "Cloud", Cost = "Soft" };
                context.Platforms.AddRange(
                    Platform1, Platform2, Platform3

                 );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> we have data already");
            }
        }
    }
}


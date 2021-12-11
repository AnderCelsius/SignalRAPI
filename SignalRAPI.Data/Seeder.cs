using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using SignalRAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRAPI.Data
{
    public static class Seeder
    {
        public static async Task SeedData(AppDbContext dbContext,
            UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            var baseDir = Directory.GetCurrentDirectory();

            await dbContext.Database.EnsureCreatedAsync();

            // Seed Data Here
            if (!dbContext.Users.Any())
            {
                List<string> roles = new List<string> { "Admin", "Approver", "Disburser", "Regular" };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                }

                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Obinna",
                    LastName = "Asiegbulam",
                    Email = "oasiegbulam@gmail.com",
                    PhoneNumber = "09043546576",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                user.UserName = user.Email;
                user.EmailConfirmed = true;
                await userManager.CreateAsync(user, "Password@123");
                await userManager.AddToRoleAsync(user, "Admin");

                var path = File.ReadAllText(FilePath(baseDir, "Json/users.json"));

                var appUsers = JsonConvert.DeserializeObject<List<AppUser>>(path);
                for (int i = 0; i < appUsers.Count; i++)
                {
                    appUsers[i].UserName = appUsers[i].Email;
                    appUsers[i].EmailConfirmed = true;
                    await userManager.CreateAsync(appUsers[i], "Password@123");
                    if (i < 2)
                    {
                        await userManager.AddToRoleAsync(appUsers[i], "Approver");
                        continue;
                    }
                    if(i > 1 && i < 4)
                    {
                        await userManager.AddToRoleAsync(appUsers[i], "Disburser");
                        continue;
                    }
                    await userManager.AddToRoleAsync(appUsers[i], "Regular");
                }
            }

            if (!dbContext.FormStatuses.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/FormStatus.json"));
                var formStatuses = JsonConvert.DeserializeObject<List<FormStatus>>(path);
                await dbContext.FormStatuses.AddRangeAsync(formStatuses);
            }

            if (!dbContext.RequestForms.Any())
            {
                var path = File.ReadAllText(FilePath(baseDir, "Json/RequestForm.json"));
                var requestForms = JsonConvert.DeserializeObject<List<RequestForm>>(path);
                await dbContext.RequestForms.AddRangeAsync(requestForms);
            }

            await dbContext.SaveChangesAsync();
        }

        static string FilePath(string folderName, string fileName)
        {
            return Path.Combine(folderName, fileName);
        }
    }
}

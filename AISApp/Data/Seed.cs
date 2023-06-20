using AISApp.Data.Enum;
using AISApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace AISApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Hospitals.Any())
                {
                    context.Hospitals.AddRange(new List<Hospital>()
                    {
                        new Hospital()
                        {
                            Title = "Institut za javno zdravlje Vojvodine",
                            Image = "https://www.goodhouse.rs/wp-content/uploads/2017/12/Institut_za_javno_zdravlje_Vojvodine.jpg",
                            Description = "This is the description of the hospital",
                            HospitalCategory = HospitalCategory.PublicMedicalCenter,
                            Address = new Address()
                            {
                                Street = "Futoska 121",
                                City = "Novi Sad"
                                
                            }
                         },
                        new Hospital()
                        {
                            Title = "Bolnica Sveti Sava",
                            Image = "https://media.pink.rs/images/2f85956d7-d135-49f5-ba8f-d3ddf26685bd/0,0,864,486/676",
                            Description = "This is the description of the hospital",
                            HospitalCategory = HospitalCategory.SpecializedClinic,
                            Address = new Address()
                            {
                               Street = "Futoska 121",
                               City = "Novi Sad"

                            }
                        },
                        new Hospital()
                        {
                            Title = "Jugolab laboratorija Centar",
                            Image = "https://www.jugolab.rs/img/labs/centar/2x-800x600.jpg",
                            Description = "This is the description of the hospital",
                            HospitalCategory = HospitalCategory.MedicalLaboratory,
                            Address = new Address()
                            {
                                Street = "Futoska 121",
                                City = "Novi Sad"

                            }
                        },
                        new Hospital()
                        {
                            Title = "Hospital 4",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the hospital",
                            HospitalCategory = HospitalCategory.PublicMedicalCenter,
                            Address = new Address()
                            {
                                Street = "Futoska 121",
                                City = "Novi Sad"

                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "teddysmithdeveloper@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "teddysmithdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "Futoska 121",
                            City = "Novi Sad"

                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "Futoska 121",
                            City = "Novi Sad"

                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}

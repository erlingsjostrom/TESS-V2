using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRestfulAPI.Entities.TESS
{
    public class MainClass
    {
        static int Main(string[] args)
        {
            CreateAll();
            //DropAll();
            return 0;
        }

        private static void CreateAll()
        {
            using (var context = new UserEntities())
            {
                CreatePermissions(context);
                CreateRoles(context);
                CreateResources(context);
                CreateUsers(context);

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void DropAll()
        {
            var dropList = new List<string>()
            {
                "ResourceUsers",
                "UserRoles",
                "RolePermissions",
                "Permissions",
                "Roles",
                "Resources",
                "Users",
                "__MigrationHistory"
            };

            using (var context = new UserEntities())
            {
                ConsoleLog("Running Migration: Delete All");
                foreach (var table in dropList)
                {
                    try
                    {
                        context.Database.ExecuteSqlCommand("DROP TABLE dbo." + table);
                    }
                    catch (Exception e)
                    {
                        // Ignore all exceptions 8D
                    }
                    
                }
                context.SaveChanges();
                ConsoleLog("Success! All migrations deleted. \n\n");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static void CreatePermissions(UserEntities context)
        {
            if (!context.Permissions.Any())
            {
                ConsoleLog("Running migration: Creating Permissions");
                var read = new Permission();
                read.Name = "Read";
                read.Description = "Gives the permission to read records";
                
                var write = new Permission();
                write.Name = "Write";
                write.Description = "Gives the permission to write records";

                var modify = new Permission();
                modify.Name = "Modify";
                modify.Description = "Gives the permission to modify existing records";

                context.Permissions.Add(read);
                context.Permissions.Add(write);
                context.Permissions.Add(modify);

                context.SaveChanges();
                
                ConsoleLog("Success! Permissions inserted.\n\n");
            }
        }

        private static void CreateRoles(UserEntities context)
        {
            if (!context.Roles.Any())
            {
                ConsoleLog("Running migration: Creating Roles");

                // Admin Role
                var admin = new Role();
                admin.Name = "Admin";
                admin.Description = "System Administrator with full access";
                context.Roles.Add(admin);

                // Standard Role
                var standard = new Role();
                standard.Name = "Standard";
                standard.Description = "Standard User with limited access";
                var ReadPermission = context.Permissions.FirstOrDefault(p => p.Name == "Read");
                standard.Permissions.Add(ReadPermission);
                context.Roles.Add(standard);

                // Product Owner Role
                var productOwner = new Role();
                productOwner.Name = "ProductOwner";
                productOwner.Description = "Product Owner with access to multiple resources";
                productOwner.Permissions.Add(ReadPermission);
                context.Roles.Add(productOwner);
                
                context.SaveChanges();

                ConsoleLog("Success! Roles inserted.\n\n");
            }
        }

        private static void CreateResources(UserEntities context)
        {
            if (!context.Resources.Any())
            {
                ConsoleLog("Running migration: Creating Resources");

                var tess = new Resource();
                tess.Name = "TESS";
                tess.Location = "TEST_TESS_TESS";
                tess.Description = "Default TEST TESS Database";
                
                context.SaveChanges();

                ConsoleLog("Success! Resources inserted.\n\n");
            }
        }

        private static void CreateUsers(UserEntities context)
        {
            if (!context.Users.Any())
            {
                ConsoleLog("Running migration: Creating Users");

                var anton = new User();
                anton.Name = "Anton Lundqvist";
                anton.WindowsUser = "eu\\lundqant";

                anton.Roles.Add(context.Roles.FirstOrDefault(r => r.Name == "Admin"));
                anton.Resources.Add(context.Resources.FirstOrDefault(r => r.Name == "TESS"));
                context.Users.Add(anton);

                var erik = new User();
                erik.Name = "Erik Lundmark";
                erik.WindowsUser = "eu\\lundmeri";

                erik.Roles.Add(context.Roles.FirstOrDefault(r => r.Name == "Admin"));
                erik.Resources.Add(context.Resources.FirstOrDefault(r => r.Name == "TESS"));
                context.Users.Add(erik);

                context.SaveChanges();

                ConsoleLog("Success! Users inserted.\n\n");
            }
        }

        private static void ConsoleLog(string message, DateTime? time = null)
        {
            DateTime? stamp = time ?? DateTime.Now;
            Console.WriteLine("[" + stamp.Value.ToLongTimeString() + "] " + message);
        }
    }
}

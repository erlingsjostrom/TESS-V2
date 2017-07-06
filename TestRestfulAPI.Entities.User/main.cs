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
            using (var context = new UserEntities())
            {

                var user = context.Users.FirstOrDefault(u => u.Id == 1);
                user.Name = "Erik \"Den coole\" Lundmark";
                context.SaveChanges();

                Console.WriteLine("[" + user.UpdatedAt.ToShortDateString() + "] >> User " + user.Name + " was updated with ID " + user.Id);
                Console.ReadKey();
            }
            return 0;
        }

        private static void CreateUser(UserEntities context)
        {
            var user = new User();
            user.Name = "Anton Lundqvist";
            user.WindowsUser = "eu\\lundqant";

            context.Users.Add(user);
            context.SaveChanges();

            Console.WriteLine("[" + user.UpdatedAt.ToShortDateString() + "] >> User " + user.Name + " was created with ID " + user.Id);
            Console.ReadKey();
        }
    }
}

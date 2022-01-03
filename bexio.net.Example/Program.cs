using System;
using System.Threading.Tasks;

namespace bexio.net.Example
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Create token from https://office.bexio.com/admin/apiTokens
            string token = "...";

            var bexioApi = new BexioApi(token, unsuccessfulReturnStyle: UnsuccessfulReturnStyle.Throw);

            Console.WriteLine("Users:");
            foreach (var user in await bexioApi.GetUsers())
            {
                Console.WriteLine($"{user.SalutationType} {user.Firstname} {user.Lastname}");
                Console.WriteLine($"    Is Superadmin: {user.IsSuperadmin}");
                Console.WriteLine($"    Is Accountant: {user.IsAccountant}");
            }
        }
    }
}

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

            // Note: with "Throw"-Style, we can ignore nullability in the following code. But make sure,
            // you handle "UnsuccessfulException"'s properly, not like in this example ;)
            // With "ReturnNull"-Style you do not get any information on why the request failed.


            // var paginatedList = (await bexioApi.GetUsersAsync());
            // if (paginatedList != null)
            // {
            //     Console.WriteLine($"Users: Showing {paginatedList}");
            //     foreach (var user in paginatedList!.List)
            //     {
            //         Console.WriteLine($"{user.SalutationType} {user.Firstname} {user.Lastname}");
            //         Console.WriteLine($"    Is Superadmin: {user.IsSuperadmin}");
            //         Console.WriteLine($"    Is Accountant: {user.IsAccountant}");
            //     }
            // }

            // await bexioApi.CreateBusinessActivityAsync(new BusinessActivity()
            // {
            //     Name = "test-api-124",
            // });

            Console.WriteLine("Business activities:");
            foreach (var ba in (await bexioApi.Project.GetBusinessActivitiesAsync())!)
            {
                Console.WriteLine($"{ba.Id}: {ba.Name} \t {ba.DefaultPricePerHour}");
            }

            Console.WriteLine("Projects:");
            foreach (var project in (await bexioApi.Project.GetProjectsAsync())!)
            {
                Console.WriteLine($"{project.Id}: {project.Uuid} {project.Name}");
                Console.WriteLine("Project milestones:");
                foreach (var milestone in (await bexioApi.Project.GetProjectMilestonesAsync(project.Id))!)
                {
                    Console.WriteLine($"{milestone.Id}: {milestone.Name}");
                }
            }

            Console.WriteLine("Contacts: ");
            foreach (var contact in (await bexioApi.Contact.GetContactsAsync())!)
            {
                Console.WriteLine($"{contact.Id}: {contact.Name1} {contact.Name2} {contact.Mail}");
                Console.WriteLine(contact.ContactBranchIdsList);
            }

            Console.WriteLine("ContactGroups:");
            foreach (var group in (await bexioApi.Contact.GetContactGroupsAsync())!)
                Console.WriteLine($"{group.Id} {group.Name}");

            Console.WriteLine("ContactSectors:");
            foreach (var g in (await bexioApi.Contact.GetContactSectorsAsync())!)
                Console.WriteLine($"{g.Id} {g.Name}");

            Console.WriteLine("Salutations:");
            foreach (var g in (await bexioApi.Contact.GetSalutationsAsync())!)
                Console.WriteLine($"{g.Id} {g.Name}");

            Console.WriteLine("Titles:");
            foreach (var g in (await bexioApi.Contact.GetTitlesAsync())!)
                Console.WriteLine($"{g.Id} {g.Name}");


            Console.WriteLine();
            Console.WriteLine("EOS"); // end of script
        }
    }
}

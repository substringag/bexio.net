using bexio.net.Models.Contacts;

namespace bexio.net.Example.Options;

public static class ContactsOption
{
    public static async Task ShowContacts(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Contacts: ");
        
        foreach (Contact contact in (await bexioApi.Contact.GetContactsAsync())!)
        {
            Console.WriteLine($"{contact.Id}: {contact.Name1} {contact.Name2} {contact.Mail}");
            Console.WriteLine(contact.ContactBranchIdsList);
        }
    }
}
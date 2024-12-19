using System.ComponentModel.DataAnnotations;
using bexio.net.Models;
using bexio.net.Models.Contacts;

namespace bexio.net.Example.Options;

internal static class ContactsOption
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

    public static async Task CreateContact(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Create Contact: ");

        var apiModels = new ApiModels
        {
            Contact = new Contact
            {
                ContactTypeId = 0,
                Name1 = "NAME_1",
                Name2 = "NAME_2",
                UserId = 0,
                OwnerId = 0
            }
        };
        
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(apiModels.Contact);
        bool isValid = Validator.TryValidateObject(apiModels.Contact, context, validationResults, true);
        
        if (!isValid)
        {
            foreach (ValidationResult validationResult in validationResults)
            {
                Console.WriteLine(validationResult.ErrorMessage);
            }
        }
        else
        {
            Contact? response = await bexioApi.Contact.CreateContactAsync(apiModels.Contact);

            Console.WriteLine(response != null
                ? $"{response.Id}: {response.Name1} {response.Name2} {response.Mail}"
                : "Contact not created.");
        }
    }
    
}
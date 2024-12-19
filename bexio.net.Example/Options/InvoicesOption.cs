using bexio.net.Models.Sales;

namespace bexio.net.Example.Options;

internal static class InvoicesOption
{
    public static async Task ShowInvoices(ApiBexio.BexioApi bexioApi)
    {
        Console.WriteLine("Invoices: ");
        
        foreach (Invoice invoice in (await bexioApi.SaleOrderManagement.GetInvoicesAsync())!)
        {
            Console.WriteLine($"{invoice.Id}: {invoice.Title} {invoice.ProjectId} {invoice.Total}");
        }
    }
}
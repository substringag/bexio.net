using System.Reflection;
using bexio.net.Exceptions;
using Microsoft.Extensions.Configuration;

// Note: with "Throw"-Style, we can ignore nullability in the following code. But make sure,
// you handle "UnsuccessfulException"'s properly, not like in this example ;)
// With "ReturnNull"-Style you do not get any information on why the request failed.

namespace bexio.net.Example
{
    public class Program
    {
        private static IConfigurationRoot _configuration = null!;
        private static ApiBexio.BexioApi _bexioApi = null!;

        public static async Task Main(string[] args)
        {
            // Build configuration
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string token = _configuration["BexioToken"]!;
            _bexioApi = new ApiBexio.BexioApi(token, unsuccessfulReturnStyle: UnsuccessfulReturnStyle.Throw);

            // Start dynamic menu
            await RunMenuAsync();
        }

        private static async Task RunMenuAsync()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Choose an option (type the number):");

                // Dynamically load all menu options
                List<(MethodInfo Method, string ClassName)> options = GetMenuOptions();

                // Display menu
                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i].Method.Name} ({options[i].ClassName})");
                }

                Console.WriteLine($"{options.Count + 1}. Exit");

                // Get user input
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > options.Count + 1)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                if (choice == options.Count + 1)
                {
                    exit = true;
                    Console.WriteLine("Exiting...");
                    break;
                }

                // Execute the selected method
                (MethodInfo Method, string ClassName) selectedOption = options[choice - 1];
                await InvokeMenuOptionAsync(selectedOption);
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Dynamically retrieve menu options from the Options namespace
        private static List<(MethodInfo Method, string ClassName)> GetMenuOptions()
        {
            var options = new List<(MethodInfo, string)>();

            // Load the current assembly
            var assembly = Assembly.GetExecutingAssembly();

            // Filter types in the "Options" namespace
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => t.Namespace != null && t.Namespace.EndsWith(".Options") && t.IsClass);

            // Retrieve public static methods from each class
            foreach (Type type in types)
            {
                IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .Where(m => m.GetParameters().Length == 1 &&
                                m.GetParameters()[0].ParameterType == typeof(ApiBexio.BexioApi));

                options.AddRange(methods.Select(method => (method, type.Name)));
            }

            return options;
        }

        // Invoke a dynamically selected method
        private static async Task InvokeMenuOptionAsync((MethodInfo Method, string ClassName) option)
        {
            try
            {
                Console.WriteLine($"Executing: {option.Method.Name} in {option.ClassName}");
                Type? type = option.Method.DeclaringType;

                // Invoke the method dynamically
                object? result = option.Method.Invoke(null, [_bexioApi]);

                if (result is Task taskResult)
                    await taskResult;
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error executing method: {exception.Message}");
            }
        }
    }
}



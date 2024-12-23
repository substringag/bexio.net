using System.Runtime.CompilerServices;

namespace bexio.net.Helpers;

public static class FileHelper
{
    public static string GetResponseFilePath([CallerMemberName] string callerName = "")
    {
        // Get the directory of the current assembly (bexio.net.Example)
        string projectRoot = AppContext.BaseDirectory;

        // Navigate up to the project root (adjust as needed if subfolders exist)
        string rootPath = Path.Combine(projectRoot, "Responses");

        // Ensure the directory exists
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }

        // Combine with the callerName for the file
        return Path.Combine(rootPath, $"{callerName}.txt");
    }
}
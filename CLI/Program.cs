using Classes;
using System;
using System.IO;
using System.Linq;
/*
class Program 
{
    private static string distance; // variable to save the distance
    private static string folderPath; // Agregar esta línea para almacenar la ruta de la carpeta

    static void Main(string[] args)
    {
        var indexer = new Indexer();
        Console.WriteLine("Welcome to the Indexer/Search Engine CLI");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var commandParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (commandParts.Length == 0) continue;

            string command = commandParts[0];
            switch (command.ToLower())
            {
                case "index":
                    if (commandParts.Length == 7 && commandParts[1] == "-f" && commandParts[3] == "-t" && commandParts[5] == "-dis")
                    {
                        folderPath = commandParts[2]; // Guardar la ruta del folder
                        string type = commandParts[4]; // tfidf o vectorizer
                        distance = commandParts[6]; // save the distance
                        try
                        {
                            indexer.IndexFolder(folderPath, type, distance);
                            Console.WriteLine("Indexing completed.");
                            // No eliminar el index.json aquí, se crea en IndexFolder
                        }
                        catch (DirectoryNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error indexing folder: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: index -f <folder> -t <type> -dis <distance>");
                    }
                    break;

                case "load":
                    if (commandParts.Length == 3 && commandParts[1] == "-p")
                    {
                        string indexPath = commandParts[2];
                        try
                        {
                            indexer.LoadIndex(indexPath);
                            Console.WriteLine("Index loaded successfully.");
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error loading index: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: load -p <index_path>");
                    }
                    break;

                case "search":
                    if (commandParts.Length >= 5 && commandParts[1] == "-q" && commandParts[^2] == "-k")
                    {
                        string query = string.Join(" ", commandParts.Skip(2).Take(commandParts.Length - 4));
                        if (int.TryParse(commandParts[^1], out int k))
                        {
                            string distances = distance; //use saved distance
                            try 
                            {
                                Console.WriteLine($"Searching for query: '{query}' with k = {k}"); // Debugging line
                                var results = indexer.Search(query, k, distances);
                                Console.WriteLine($"Results for '{query}':");
                                foreach (var result in results)
                                {
                                    Console.WriteLine(result);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error searching: {ex.Message}");
                                Console.WriteLine(ex.StackTrace); // Print stack trace for more context
                            }
                        }
                        else 
                        {
                            Console.WriteLine("k must be of type: integer.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: search -q <query> -k <k>");
                    }
                    break;

                case "exit":
                    // Eliminar el index.json al salir
                    string exitIndexFilePath = Path.Combine(folderPath, "index.json"); // Usa folderPath
                    if (File.Exists(exitIndexFilePath))
                    {
                        File.Delete(exitIndexFilePath);
                        Console.WriteLine("index.json deleted on exit.");
                    }
                    return;

                default:
                    Console.WriteLine("Command not recognized. Use 'index -f <folder> -t <indexer> -dis <similarity>', 'load -p <index_path>' or 'search -q <query> -k <k>'. Type 'exit' to quit.");
                    break;
            }
        }
    }
}
*/
using Classes;
using System;
using System.IO;
using System.Linq;

class Program 
{
    private static string distance; // Variable to save the distance
    private static string folderPath; // Variable to store the path of the folder

    static void Main(string[] args)
    {
        var indexer = new Indexer();
        Console.WriteLine("Welcome to the Indexer/Search Engine CLI");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;

            var commandParts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (commandParts.Length == 0) continue;

            string command = commandParts[0];
            switch (command.ToLower())
            {
                case "index":
                    if (commandParts.Length == 7 && commandParts[1] == "-f" && commandParts[3] == "-t" && commandParts[5] == "-dis")
                    {
                        string folderName = commandParts[2];

                        // Construct path to the folder inside the "Folders" directory
                        string cliDirectory = Directory.GetCurrentDirectory();
                        string parentDirectory = Directory.GetParent(cliDirectory).FullName;
                        folderPath = Path.Combine(parentDirectory, "Folders", folderName);

                        if (!Directory.Exists(folderPath))
                        {
                            Console.WriteLine($"Error: The folder '{folderName}' does not exist at the expected path: {folderPath}");
                            break;
                        }

                        string type = commandParts[4]; // tfidf or vectorizer
                        distance = commandParts[6]; // Save the distance

                        try
                        {
                            indexer.IndexFolder(folderPath, type, distance);
                            Console.WriteLine("Indexing completed.");
                        }
                        catch (DirectoryNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error indexing folder: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: index -f <folder_name> -t <type> -dis <distance>");
                    }
                    break;

                case "load":
                    if (commandParts.Length == 3 && commandParts[1] == "-p")
                    {
                        string indexFileName = commandParts[2];

                        // Construct path to the index file inside the "Folders" directory
                        string cliDirectory = Directory.GetCurrentDirectory();
                        string parentDirectory = Directory.GetParent(cliDirectory).FullName;
                        string indexPath = Path.Combine(parentDirectory, "Folders", indexFileName);

                        try
                        {
                            indexer.LoadIndex(indexPath);
                            Console.WriteLine("Index loaded successfully.");
                        }
                        catch (FileNotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error loading index: {ex.Message}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: load -p <index_file_name>");
                    }
                    break;

                case "search":
                    if (commandParts.Length >= 5 && commandParts[1] == "-q" && commandParts[^2] == "-k")
                    {
                        string query = string.Join(" ", commandParts.Skip(2).Take(commandParts.Length - 4));
                        if (int.TryParse(commandParts[^1], out int k))
                        {
                            string distances = distance; // Use saved distance
                            try 
                            {
                                Console.WriteLine($"Searching for query: '{query}' with k = {k}"); // Debugging line
                                var results = indexer.Search(query, k, distances);
                                Console.WriteLine($"Results for '{query}':");
                                foreach (var result in results)
                                {
                                    Console.WriteLine(result);
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error searching: {ex.Message}");
                            }
                        }
                        else 
                        {
                            Console.WriteLine("k must be of type: integer.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Usage: search -q <query> -k <k>");
                    }
                    break;

                case "exit":
                    // Delete the index.json on exit
                    string exitIndexFilePath = Path.Combine(folderPath, "index.json"); // Use folderPath
                    if (File.Exists(exitIndexFilePath))
                    {
                        File.Delete(exitIndexFilePath);
                        Console.WriteLine("index.json deleted on exit.");
                    }
                    return;

                default:
                    Console.WriteLine("Command not recognized. Use 'index -f <folder_name> -t <indexer> -dis <similarity>', 'load -p <index_file_name>' or 'search -q <query> -k <k>'. Type 'exit' to quit.");
                    break;
            }
        }
    }
}
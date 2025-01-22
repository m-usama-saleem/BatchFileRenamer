// File Renaming Utility
// This program renames files in a specified folder by replacing specified substrings in their names.
// Before using this program, please note:
// 1. Provide the full path to the folder containing the files you want to rename.
// 2. You will be prompted to specify how many replacements you want to make.
// 3. For each replacement, input the substring to replace and its replacement value.
// 4. If you want to remove a substring entirely, simply press Enter for empty string as the replacement value.
// 5. The program will rename files directly; no duplicates or backups will be created.
// 6. Ensure no file with the target name already exists in the folder, or renaming will be skipped.
// 7. Use with caution, as incorrect inputs may lead to unintended renaming.

Console.WriteLine("Enter the folder path:");
string folderPath = Console.ReadLine();

if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
{
    Console.WriteLine("Invalid folder path. Please provide a valid path.");
    return;
}

Console.WriteLine("Enter the number of replacements you want to make:");
if (!int.TryParse(Console.ReadLine(), out int replacementCount) || replacementCount <= 0)
{
    Console.WriteLine("Invalid number of replacements. Please provide a positive integer.");
    return;
}

var replacements = new List<(string, string)>();
for (int i = 0; i < replacementCount; i++)
{
    Console.WriteLine($"Enter the string to replace (#{i + 1}):");
    string toReplace = Console.ReadLine();

    Console.WriteLine($"Enter the replacement string for '{toReplace}':");
    string replacement = Console.ReadLine();

    replacements.Add((toReplace, replacement));
}

try
{
    string[] files = Directory.GetFiles(folderPath);

    foreach (string file in files)
    {
        string fileName = Path.GetFileNameWithoutExtension(file);
        string fileExtension = Path.GetExtension(file);
        string newFileName = fileName;

        foreach (var (toReplace, replacement) in replacements)
        {
            if (!string.IsNullOrEmpty(toReplace))
            {
                newFileName = newFileName.Replace(toReplace, replacement);
            }
        }

        if (newFileName != fileName)
        {
            string newFilePath = Path.Combine(folderPath, newFileName) + fileExtension;

            if (!File.Exists(newFilePath))
            {
                File.Move(file, newFilePath);
                Console.WriteLine($"Renamed: {fileName} -> {newFileName}");
            }
            else
            {
                Console.WriteLine($"Skipping rename: {newFileName} already exists.");
            }
        }
    }

    Console.WriteLine("File renaming completed successfully.");
}
catch (Exception ex)
{
    Console.WriteLine("An error occurred: " + ex.Message);
}
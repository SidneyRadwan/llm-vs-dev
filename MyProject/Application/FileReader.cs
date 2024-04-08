using System.Text.RegularExpressions;

namespace MyProject.Application;

public static class FileReader
{
    public static string _ReadAndValidateFile(string filePath)
    {
        try
        {
            string textOutput = File.ReadAllText(filePath);
            var illegalChars = textOutput.Where(c => !char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c)).ToList();

            if (illegalChars.Any())
                throw new InvalidOperationException($"Message contains illegal characters {string.Join(",", illegalChars)}");

            return textOutput;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }
    
    public static string ReadAndValidateFile(string filePath)
    {
        try
        {
            // Check if the file path is valid
            if (!File.Exists(filePath))
            {
                throw new InvalidOperationException("Invalid file path. The file does not exist.");
            }

            // Read the entire file content
            string fileContent = File.ReadAllText(filePath);

            // Validate that the text contains only alphanumeric characters and spaces
            if (!IsValidText(fileContent))
            {
                throw new InvalidOperationException("File content contains invalid characters. Only alphanumeric characters and spaces are allowed.");
            }

            // Return the valid file content
            return fileContent;
        }
        catch (Exception ex)
        {
            // Handle any exceptions (e.g., file not found, permissions, etc.)
            throw new InvalidOperationException($"Error reading or validating the file: {ex.Message}");
        }
    }

    private static bool IsValidText(string text)
    {
        // Use a regular expression to check for valid characters (alphanumeric and spaces)
        return Regex.IsMatch(text, @"^[a-zA-Z0-9\s]+$");
    }
}
using System.Text.Json;

internal static class FileHelper
{
    private static string filePath = "expenses.json";

    public static void SaveExpenses(List<Expense> expenses)
    {
        string json = JsonSerializer.Serialize(expenses, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText(filePath, json);
    }

    public static List<Expense> LoadExpenses()
    {
        if (!File.Exists(filePath))
        {
            return new List<Expense>();
        }

        string json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
    }
}
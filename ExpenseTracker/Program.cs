using System;
using System.Collections.Generic;
using System.Linq;

List<Expense> expenses = FileHelper.LoadExpenses();

while (true)
{
    Console.Clear();

    Console.WriteLine("===== EXPENSE TRACKER =====");
    Console.WriteLine("1. Add Expense");
    Console.WriteLine("2. View Expenses");
    Console.WriteLine("3. Total Spending");
    Console.WriteLine("4. Delete Expense");
    Console.WriteLine("5. Edit Expense");
    Console.WriteLine("6. Search Expense");
    Console.WriteLine("7. Category Report");
    Console.WriteLine("8. Dashboard");
    Console.WriteLine("9. Monthly Report");
    Console.WriteLine("10. Exit");

    Console.Write("\nChoose an option: ");
    string choice = Console.ReadLine()!;

    switch (choice)
    {
        case "1":

            Console.Write("Enter Title: ");
            string title = Console.ReadLine()!;

            Console.Write("Enter Amount: ");

            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                Console.WriteLine();
                Console.WriteLine("Categories");
                Console.WriteLine("1. Food");
                Console.WriteLine("2. Travel");
                Console.WriteLine("3. Bills");
                Console.WriteLine("4. Shopping");
                Console.WriteLine("5. Entertainment");

                Console.Write("Choose Category: ");

                string categoryChoice = Console.ReadLine()!;

                string category = categoryChoice switch
                {
                    "1" => "Food",
                    "2" => "Travel",
                    "3" => "Bills",
                    "4" => "Shopping",
                    "5" => "Entertainment",
                    _ => "Other"
                };

                expenses.Add(new Expense
                {
                    Id = expenses.Count + 1,
                    Title = title,
                    Amount = amount,
                    Category = category,
                    Date = DateTime.Now
                });

                FileHelper.SaveExpenses(expenses);

                Console.WriteLine("\nExpense Added Successfully!");
            }
            else
            {
                Console.WriteLine("Invalid Amount!");
            }

            break;

        case "2":

            if (expenses.Count == 0)
            {
                Console.WriteLine("\nNo Expenses Found.");
            }
            else
            {
                Console.WriteLine("\n-------------------------------");

                foreach (var expense in expenses)
                {
                    Console.WriteLine( $"{expense.Id} | {expense.Title} | ${expense.Amount} | {expense.Category}");
                }

                Console.WriteLine("-------------------------------");
            }

            break;

        case "3":

            decimal total = expenses.Sum(e => e.Amount);

            Console.WriteLine($"\nTotal Spending: ${total}");

            break;

        case "4":

            Console.Write("Enter Expense ID to Delete: ");

            if (int.TryParse(Console.ReadLine(), out int deleteId))
            {
                Expense expense = expenses.FirstOrDefault(e => e.Id == deleteId);

                if (expense != null)
                {
                    Console.Write("Are you sure? (Y/N): ");

                    string confirm = Console.ReadLine()!;

                    if (confirm.ToUpper() == "Y")
                    {
                        expenses.Remove(expense);
                        Console.WriteLine("Expense Deleted Successfully!");
                        FileHelper.SaveExpenses(expenses);
                    }
                }
                else
                {
                    Console.WriteLine("Expense Not Found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }

            break;

        case "5":

            Console.Write("Enter Expense ID to Edit: ");

            if (int.TryParse(Console.ReadLine(), out int editId))
            {
                Expense expense = expenses.FirstOrDefault(e => e.Id == editId);

                if (expense != null)
                {
                    Console.Write("New Title: ");
                    expense.Title = Console.ReadLine()!;

                    Console.Write("New Amount: ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal newAmount))
                    {
                        expense.Amount = newAmount;
                        Console.WriteLine("Expense Updated Successfully!");
                        FileHelper.SaveExpenses(expenses);
                    }
                    else
                    {
                        Console.WriteLine("Invalid Amount!");
                    }
                }
                else
                {
                    Console.WriteLine("Expense Not Found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }

            break;

        case "6":

            Console.Write("Search Title: ");

            string search = Console.ReadLine()!;

            var results = expenses.Where(e =>
                e.Title.Contains(search, StringComparison.OrdinalIgnoreCase));

            foreach (var expense in results)
            {
                Console.WriteLine(
                    $"{expense.Id} | {expense.Title} | ${expense.Amount} | {expense.Category}");
            }

            break;
        case "7":

            var report = expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    Total = g.Sum(x => x.Amount)
                });

            Console.WriteLine();

            foreach (var item in report)
            {
                Console.WriteLine($"{item.Category} : ${item.Total}");
            }

            break;
        case "8":

            Console.Clear();

            Console.WriteLine("========== DASHBOARD ==========");

            Console.WriteLine($"Total Expenses : {expenses.Count}");

            decimal totalAmount = expenses.Sum(e => e.Amount);

            Console.WriteLine($"Total Spending : ${totalAmount}");

            if (expenses.Count > 0)
            {
                Expense highest = expenses.OrderByDescending(e => e.Amount).First();

                Expense lowest = expenses.OrderBy(e => e.Amount).First();

                Console.WriteLine($"Highest Expense : {highest.Title} (${highest.Amount})");

                Console.WriteLine($"Lowest Expense : {lowest.Title} (${lowest.Amount})");

                Console.WriteLine($"Average Expense : ${expenses.Average(e => e.Amount):0.00}");
            }
            else
            {
                Console.WriteLine("No expenses found.");
            }

            break;
        case "9":

            Console.Clear();

            Console.WriteLine("====== MONTHLY REPORT ======");

            var monthly = expenses
                .GroupBy(e => e.Date.ToString("MMMM yyyy"));

            foreach (var month in monthly)
            {
                Console.WriteLine($"\n{month.Key}");

                foreach (var expense in month)
                {
                    Console.WriteLine($"{expense.Title} - ${expense.Amount}");
                }

                Console.WriteLine($"Total : ${month.Sum(x => x.Amount)}");
            }

            break;
            case "10":

            Console.WriteLine("\nExit");
            break;
        default:

            Console.WriteLine("\nInvalid Choice!");

            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}
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
    Console.WriteLine("6. Exit");

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
                expenses.Add(new Expense
                {
                    Id = expenses.Count + 1,
                    Title = title,
                    Amount = amount,
                    Date = DateTime.Now
                });

                Console.WriteLine("\nExpense Added Successfully!");
                FileHelper.SaveExpenses(expenses);
            }

            else
            {
                Console.WriteLine("\nInvalid Amount!");
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
                    Console.WriteLine($"{expense.Id} | {expense.Title} | ${expense.Amount} | {expense.Date:d}");
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

            Console.WriteLine("\nThank you for using Expense Tracker!");
            return;

        default:

            Console.WriteLine("\nInvalid Choice!");

            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}
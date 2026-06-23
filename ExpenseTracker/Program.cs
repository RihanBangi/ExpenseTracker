using System;
using System.Collections.Generic;
using System.Linq;

List<Expense> expenses = new();

while (true)
{
    Console.WriteLine("\n===== Expense Tracker =====");
    Console.WriteLine("1. Add Expense");
    Console.WriteLine("2. View Expenses");
    Console.WriteLine("3. Total Spending");
    Console.WriteLine("4. Exit");

    Console.Write("Choose: ");
    string choice = Console.ReadLine()!;

    switch (choice)
    {
        case "1":

            Console.Write("Title: ");
            string title = Console.ReadLine()!;

            Console.Write("Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine()!);

            expenses.Add(new Expense
            {
                Id = expenses.Count + 1,
                Title = title,
                Amount = amount,
                Date = DateTime.Now
            });

            Console.WriteLine("Expense Added!");
            break;

        case "2":

            foreach (var expense in expenses)
            {
                Console.WriteLine(
                    $"{expense.Id} | {expense.Title} | ₹{expense.Amount}");
            }

            break;

        case "3":

            decimal total = expenses.Sum(e => e.Amount);

            Console.WriteLine($"Total Spending: ₹{total}");
            break;

        case "4":
            return;

        default:
            Console.WriteLine("Invalid Choice");
            break;
    }
}
using System;
using System.Collections.Generic;
using UnitTesting02.Projects;

namespace UnitTesting02.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var issues = new List<Issue>
            {
                new Issue("Secretary laptop giving blue screen", Priority.High, Category.UnKnown, DateTime.Now),
                new Issue("Printer switch at office #104 is broken", Priority.Urgent, Category.Hardware, DateTime.Now),
                new Issue("Upgrade OS for Laptop 101 to windows 11", Priority.Medium, Category.Software, DateTime.Now),
                new Issue("Install Password manager application on Laptop 112", Priority.Low, Category.UnKnown, DateTime.Now),
                new Issue("Change HDMI Cable for Cafeteria TV", Priority.Low, Category.Hardware, DateTime.Now)

            };
            Print(issues);
            Console.ReadKey();
        }

        private static void Print(List<Issue> issues)
        {
            var defaultColor = Console.ForegroundColor;
            foreach (var item in issues)
            {
                switch (item.Priority)
                {
                    case Priority.Urgent: Console.ForegroundColor = ConsoleColor.Red; break;
                    case Priority.High: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                    case Priority.Medium: Console.ForegroundColor = ConsoleColor.Yellow; break;
                    case Priority.Low: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                    default: Console.ForegroundColor = ConsoleColor.Cyan; break;
                }


                Console.WriteLine(item);

                Console.ForegroundColor = defaultColor;

            }
            Console.WriteLine($"\n\nTotal Issues: [{issues.Count}]");

        }
    }
}

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using LeetCode.MenuSystem;
using Newtonsoft.Json;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var solutions = typeof(Program).GetTypeInfo().Assembly.GetTypes()
                .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface && t.Name != "__Template")
                .Select(t => (ISolution)Activator.CreateInstance(t))
                .OrderBy(p => p.Name)
                .ToArray();

            var program = new Program();
            program.DisplayMenu(solutions);
        }

        private void DisplayMenu(ISolution[] solutions)
        {
            var menu = new Menu("LeetCode");

            foreach (var solution in solutions)
            {
                menu.Add(new Menu.Item(new string(solution.Name.TakeWhile(c => c != '.').ToArray()), solution.Name,
                    new Menu.Item("1", "Launch LeetCode page",
                        c =>
                        {
                            Process.Start("explorer.exe", solution.Link);
                            c.WriteLine($"Opened browser to '{solution.Link}'");
                        }),
                    new Menu.Item("2", "Run test cases",
                        c =>
                        {
                            Array cases = ((dynamic)solution).TestCases;
                            foreach (dynamic @case in cases)
                            {
                                var input = @case.Item1;
                                var expected = @case.Item2;
                                var expectedStr = JsonConvert.SerializeObject(expected);

                                var result = ((dynamic)solution).Execute(input);

                                var resultStr = JsonConvert.SerializeObject(result);
                                var originalColor = Console.ForegroundColor;
                                Console.ForegroundColor =
                                    expectedStr == resultStr ? ConsoleColor.Green : ConsoleColor.Red;
                                var report = $"{JsonConvert.SerializeObject(input)} expected: {expectedStr}, result {resultStr}";
                                if (report.Length > Console.LargestWindowWidth)
                                {
                                    report = $@"{JsonConvert.SerializeObject(input)}
- expected: {expectedStr}
- result {resultStr}";
                                }
                                Console.WriteLine(report);
                                Console.ForegroundColor = originalColor;
                            }
                        }),
                    Menu.Item.GoUp));
            }

            menu.Add(new Menu.Item("q", "Quit", c => c.CloseAndReturn("0")));

            menu.Show();
        }
    }
}
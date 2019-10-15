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
            (string num, ISolution solution)[] solutions = typeof(Program).GetTypeInfo().Assembly.GetTypes()
                .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface && t.Name != "__Template")
                .Select(t => (ISolution)Activator.CreateInstance(t))
                .Select(solution => (new string(solution.Name.TakeWhile(c => c != '.').ToArray()), solution))
                .OrderBy(p => p.solution.Name)
                .ToArray();

            if (args.Contains("--run"))
            {
                var solution = solutions.FirstOrDefault(s => s.num == args[1]);
                RunSolution(solution);
            }
            else
            {
                var program = new Program();
                program.DisplayMenu(solutions);
            }
        }

        private void DisplayMenu((string num, ISolution solution)[] solutions)
        {
            var menu = new Menu("LeetCode");

            foreach (var solution in solutions)
            {
                menu.Add(new Menu.Item(solution.num, solution.solution.Name,
                    new Menu.Item("1", "Launch LeetCode page",
                        c =>
                        {
                            Process.Start("explorer.exe", solution.solution.Link);
                            c.WriteLine($"Opened browser to '{solution.solution.Link}'");
                        }),
                    new Menu.Item("2", "Run test cases", _ => RunSolution(solution)),
                    Menu.Item.GoUp));
            }

            menu.Add(new Menu.Item("q", "Quit", c => c.CloseAndReturn("0")));

            menu.Show();
        }

        private static void RunSolution((string num, ISolution solution) item)
        {
            dynamic solution = item.solution;
            Array cases = solution.TestCases;
            foreach (dynamic @case in cases)
            {
                var input = @case.Item1;
                var expected = @case.Item2;
                var expectedStr = JsonConvert.SerializeObject(expected);

                var result = solution.Execute(input);

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
        }
    }
}
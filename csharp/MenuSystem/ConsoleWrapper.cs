namespace LeetCode.MenuSystem
{
    using System;

    public class ConsoleWrapper : IConsole
    {
        private static readonly object s_syncRoot = new object();

        public static readonly ConsoleWrapper Instance = new ConsoleWrapper();

        public void WriteLine(string message, ConsoleColor? color = null)
        {
            Write(message + Environment.NewLine, color);
        }

        public void Write(string message, ConsoleColor? color = null)
        {
            // Locking is not a problem here (it's single threaded anyway)
            lock(s_syncRoot)
            {
                var backup = Console.ForegroundColor;

                try
                {
                    if (color != null)
                    {
                        Console.ForegroundColor = color.Value;
                    }

                    Console.Write(message);
                }
                finally
                {
                    if (color != null)
                    {
                        Console.ForegroundColor = backup;
                    }
                }
            }
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public int BufferWidth
        {
            get
            {
                try
                {
                    return Console.BufferWidth;
                }
                catch (Exception)
                {
                    return 80;
                }
            }
        }
    }
}
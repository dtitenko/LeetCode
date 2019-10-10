namespace LeetCode.MenuSystem
{
    using System;

    public interface IConsole
    {
        void WriteLine(string message, ConsoleColor? color = null);
        void Write(string message, ConsoleColor? color = null);

        string ReadLine();
        ConsoleKeyInfo ReadKey();
        int BufferWidth { get; }
    }
}
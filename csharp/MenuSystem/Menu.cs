namespace LeetCode.MenuSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Menu
    {
        public readonly string Name;
        private readonly IConsole _console;
        private readonly Stack<Item> _crumbPath = new Stack<Item>();
        private readonly List<Item> _items = new List<Item>();
        private bool _menuClosed;
        private string _returnValue;

        public static readonly IConsole Console = new ConsoleWrapper();

        public Menu(string name, params Item[] items) : this(name, new ConsoleWrapper(), items)
        {

        }
        public Menu(string name, IConsole console, params Item[] items)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Argument is null or empty", nameof(name));

            Name = name;
            _console = console ?? throw new ArgumentNullException(nameof(console));
            Add(items);
        }

        public void Add(params Item[] items)
        {
            _items.AddRange(items);
        }


        public string Show()
        {
            while (!_menuClosed)
            {
                bool hasParent;
                Item[] items;

                if (_crumbPath.Count == 0)
                {
                    items = _items.ToArray();
                    hasParent = true;
                    _console.WriteLine("==================================", ConsoleColor.Green);
                    _console.WriteLine($" {Name}", ConsoleColor.Green);
                    _console.WriteLine("==================================", ConsoleColor.Green);
                }
                else
                {
                    var item = _crumbPath.Peek();
                    hasParent = item.HasParent;
                    items = item.Children;
                    _console.WriteLine("==================================", ConsoleColor.Green);
                    foreach (var path in _crumbPath.ToArray())
                    {
                        if (path != item)
                        {
                            _console.Write(" -->" + path.Name, ConsoleColor.Gray);
                        }
                    }
                    _console.WriteLine($"--> {item.Name}", ConsoleColor.Green);
                    _console.WriteLine("==================================", ConsoleColor.Green);

                }

                RenderOptions(items);

                var userChoice = GetChoice(items);

                if (userChoice != null)
                {
                    if (userChoice.OnExecute != null)
                    {
                        userChoice.OnExecute(new Context(this, userChoice));
                    }
                    else
                    {
                        _crumbPath.Push(userChoice);
                    }
                }
            }

            return _returnValue;
        }

        private Item GetChoice(Item[] items)
        {
            _console.Write("Please select one option: ");
            Item selectedItem;
            if (items.All(i => i.Accessor.Length == 1))
            {
                var keyPressed = _console.ReadKey();
                selectedItem = items.FirstOrDefault(x => x.Accessor[0] == keyPressed.KeyChar);
            }
            else
            {
                var line = _console.ReadLine();
                selectedItem = items.FirstOrDefault(x => x.Accessor == line);
            }
            
            _console.WriteLine("");
            
            if (selectedItem == null)
            {
                _console.WriteLine("Unknown option.", ConsoleColor.Red);
            }
            return selectedItem;
        }

        private void RenderOptions(Item[] items)
        {
            var table = new ConsoleTable("", "");

            foreach (var item in items)
            {
                string key;
                if (item.Accessor[0] == (int)ConsoleKey.Escape)
                {
                    key = "<esc>";
                }
                else if (item.Accessor[0] == (int)ConsoleKey.Backspace)
                {
                    key = "<backspace>";
                }
                else if (item.Accessor[0] == (int)ConsoleKey.Enter)
                {
                    key = "<enter>";
                }
                else
                {
                    key = item.Accessor;
                }

                table.AddRow($"( {key} ) --> ", item.Name);
            }

            _console.WriteLine(table.ToString(_console.BufferWidth, printHeaders: false));

        }

        public class Context
        {
            public void GoUp()
            {
                if (Menu._crumbPath.Any())
                {
                    Menu._crumbPath.Pop();
                }
            }

            public void ToMainMenu()
            {
                Menu._crumbPath.Clear();
            }

            public void CloseAndReturn(string returnValue = null)
            {
                Menu._menuClosed = true;
                Menu._returnValue = returnValue;
            }

            public readonly Item Item;
            public readonly Menu Menu;

            public void WriteLine(string message, ConsoleColor? color = null)
            {
                Menu._console.WriteLine(message, color);
            }

            public void Write(string message, ConsoleColor? color = null)
            {
                Menu._console.Write(message, color);
            }

            public string ReadLine()
            {
                return Menu._console.ReadLine();
            }

            public Context(Menu menu, Item item)
            {
                Menu = menu;
                Item = item;
            }
        }

        public class Item
        {
            public static readonly Item GoUp = new Item(new string((char)(int)ConsoleKey.Escape, 1), "Back", x => x.GoUp());

            public string Accessor { get; }

            public string Name => _nameBuilder();

            public Item[] Children => _items.ToArray();

            public void Add(params Item[] items)
            {
                if (OnExecute != null)
                {
                    throw new InvalidOperationException($"Cannot add menu items and have an onexecute for menu item: '{Accessor}': {Name}");
                }

                _items.AddRange(items);
            }

            public Action<Context> OnExecute { get; }

            public bool HasParent { get; private set; }
            private readonly List<Item> _items = new List<Item>();
            private readonly Func<string> _nameBuilder;

            public Item(string accessor, Func<string> nameBuilder, Action<Menu.Context> onExecute)
            {
                Accessor = accessor;
                _nameBuilder = nameBuilder;
                OnExecute = onExecute;
            }

            public Item(string accessor, string name, params Item[] items) :
                this(accessor, () => name, items)
            {
            }

            public Item(string accessor, Func<string> nameBuilder, params Item[] items)
            {
                Accessor = accessor;
                _nameBuilder = nameBuilder;
                _items = items.ToList();

                foreach (var item in _items)
                {
                    item.HasParent = true;
                }
            }

            public Item(string accessor, string name, Action<Menu.Context> onExecute = null)
                : this(accessor, () => name, onExecute)
            {
            }
        }
    }
}

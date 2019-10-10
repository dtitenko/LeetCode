using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeetCode.MenuSystem
{
    internal class ConsoleTable
    {
        private readonly Column[] _columns;
        private readonly List<Row> _rows = new List<Row>();

        public ConsoleTable(params Column[] columns)
        {
            _columns = columns;
            _rows.Add(new Row(columns.Select(c => c.Name).ToArray()));
        }

        public void AddRow(params string[] cellValues)
        {
            if (cellValues.Length != _columns.Length)
            {
                throw new InvalidOperationException("You must specify as many rows as settings");
            }
            _rows.Add(new Row(cellValues));
        }

        public string ToString(int maxWidth, bool printHeaders = true)
        {
            var padding = _columns.Length;
            if (_rows.Count == 0)
                return string.Empty;

            var maxRowWidth = _rows.Max(cells => cells.Sum(cell => cell.Width) + padding);

            var requiresTrim = maxRowWidth >= maxWidth;

            if (requiresTrim)
            {
                var columnWidth = (maxWidth - padding) / _columns.Length;
                foreach (var row in _rows)
                {
                    for (int index = 0; index < _columns.Length; index++)
                    {
                        row[index].Wrap(columnWidth);
                    }
                }
            }

            var maxWidthPerColumn = _columns.Select((column, index) =>
            {
                return _rows.Max(cells => cells[index].Width + 1);
            }).ToArray();

            var stringBuilder = new StringBuilder();

            stringBuilder.Append(Environment.NewLine);
            for (int rowIndex = 0; rowIndex < _rows.Count; rowIndex++)
            {
                if (rowIndex == 0 && !printHeaders)
                {
                    continue;
                }

                if (rowIndex == 1 && printHeaders)
                {
                    stringBuilder.Append("  ");
                    for (int columnIndex = 0; columnIndex < _columns.Length; columnIndex++)
                    {
                        stringBuilder.Append(new string('-', maxWidthPerColumn[columnIndex] - 1) + " ");
                    }
                    stringBuilder.AppendLine();
                }
                stringBuilder.Append("  ");
                var row = _rows[rowIndex];
                for (int lineIndex = 0; lineIndex < row.Height; lineIndex++)
                {
                    for (int columnIndex = 0; columnIndex < _columns.Length; columnIndex++)
                    {
                        stringBuilder.Append(row[columnIndex].LineValue(lineIndex).PadRight(maxWidthPerColumn[columnIndex]));
                    }
                    stringBuilder.Append(Environment.NewLine);
                }
            }
            return stringBuilder.ToString();
        }

        private class Row : IEnumerable<Cell>
        {
            private List<Cell> _cells;

            public Row(params string[] cells)
            {
                _cells = cells.Select(x => new Cell(x)).ToList();
            }

            public Row(params Cell[] cells)
            {
                _cells = cells.ToList();
            }

            public int Height => _cells.Max(x => x.Height);

            public Cell this[int index] => _cells[index];
            public int Length => _cells.Count;
            public IEnumerator<Cell> GetEnumerator()
            {
                return _cells.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable)_cells).GetEnumerator();
            }
        }

        private class Cell
        {
            private string _value;
            private string[] _lines;

            public Cell(string value)
            {
                _value = value ?? "";
                SetLines();
            }

            public int Width
            {
                get { return _lines.Max(x => x.Length); }
            }

            public string LineValue(int lineIndex)
            {
                if (_lines.Length >= lineIndex + 1)
                {
                    return _lines[lineIndex] ?? "";
                }
                return "";
            }

            private void SetLines()
            {
                _lines = _value.Replace("\r\n", "\n").Split('\n');
            }

            public int Height => _value.Split('\n').Length;

            public void Wrap(int maxLength)
            {
                if (_value.Length <= maxLength)
                {
                    return;
                }

                var value = _value.Replace("\r\n", " ");
                var numberOfLines = value.Length / maxLength;
                var lines = string.Join("\n", Enumerable.Range(0, numberOfLines)
                    .Select(i => value.Substring(i * maxLength, maxLength)));

                var remainder = value.Length % maxLength;
                if (remainder > 0)
                {
                    lines += "\n" + value.Substring(value.Length - remainder, remainder);
                }

                _value = lines;
                SetLines();
            }

            public static implicit operator Cell(string s)
            {
                return new Cell(s);
            }
        }

        internal class Column
        {
            public readonly string Name;
            public readonly bool AllowWrap;
            public Column(string name, bool allowWrap = true)
            {
                Name = name;
                AllowWrap = allowWrap;
            }

            public static implicit operator Column(string name)
            {
                return new Column(name);
            }

        }

    }
}
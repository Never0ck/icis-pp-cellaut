using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;

namespace Cellaut.Domain
{
    public class Field : IEnumerable<Cell>, ICloneable
    {
        private List<List<Cell>> _cellField;
        public IReadOnlyList<IReadOnlyList<Cell>> CellField => _cellField;

        public Field(List<List<Cell>> cellField)
        {
            _cellField = cellField;
        }

        public Field(int countX, int countY)
        {
            _cellField = new List<List<Cell>>();
            for (int y = 0; y < countY; y++)
            {
                var row = new List<Cell>();
                _cellField.Add(row);
                for (int x = 0; x < countX; x++)
                {
                    row.Add(new Cell());
                }
            }
        }

        public Cell this[int i, int j]
        {
            get => _cellField[i][j];
        }

        public void Resize(int CountX, int CountY)
        {
            if (CountX < _cellField.Count)
            {
                _cellField.RemoveRange(CountX, _cellField.Count - CountX);
            }
            else if (CountX > _cellField.Count)
            {
                if (CountX > _cellField.Capacity)
                    _cellField.Capacity = CountX;

                _cellField.AddRange(Enumerable.Range(0, CountX - _cellField.Count).Select(i => new List<Cell>()));
            }

            foreach (var col in _cellField)
            {
                if (CountY < col.Count)
                {
                    col.RemoveRange(CountY, col.Count - CountY);
                }
                else if (CountY > col.Count)
                {
                    if (CountY > col.Capacity)
                        col.Capacity = CountY;

                    col.AddRange(Enumerable.Range(0, CountY - col.Count).Select(i => new Cell()));
                }
            }
        }

        /// <summary>
        /// Возвращает окно для центральной клетки и кол-во соседей countOfNeighbours = 1
        /// </summary>
        /// <param name="cell">центральная клетка</param>
        /// <param name="countOfNeighbours">кол-во соседей</param>
        /// <returns></returns>
        public List<List<Cell>> GetWindow(Cell cell, int countOfNeighbours = 1)
        {
            // Релаизовать поддержку циклического окна
            var x = _cellField.FindIndex(cells => cells.Contains(cell));
            if (x == -1) throw new ArgumentOutOfRangeException(nameof(cell));
            
            var y = _cellField[x].FindIndex(c => c.Equals(cell));
            if (y == -1) throw new ArgumentOutOfRangeException(nameof(cell));

            var startX = x - countOfNeighbours;
            var endX = x + countOfNeighbours;
            if (startX < 0) startX = 0;
            if (endX >= _cellField.Count) endX = _cellField.Count - 1;

            var startY = y - countOfNeighbours;
            var endY = y + countOfNeighbours;

            if (startY < 0) startY = 0;

            if (endY >= _cellField[x].Count) endY = _cellField[x].Count -1;

            var window = new List<List<Cell>>();

            for (var i = startX; i <= endX; i++)
            {
                var row = new List<Cell>();
                for (var j = startY; j <= endY; j++)
                {
                    var cellFromField = _cellField[i][j];
                    row.Add(cellFromField);
                }
                window.Add(row);
            }

            return window;
        }

        public void UpdateField(List<Cell> toUpdateCells)
        {
            foreach (var updateCell in toUpdateCells)
            {
                UpdateCell(updateCell);
            }
        }

        private void UpdateCell(Cell cell)
        {
            var x = _cellField.FindIndex(cells => cells.Exists(c => c.Id == cell.Id));
            if (x == -1) throw new ArgumentOutOfRangeException(nameof(cell));

            var y = _cellField[x].FindIndex(c => c.Id == cell.Id);
            if (y == -1) throw new ArgumentOutOfRangeException(nameof(cell));

            _cellField[x][y] = cell;
        }

        class FieldEnumerator : IEnumerator<Cell>
        {
            private List<List<Cell>> _field;
            private IEnumerator<List<Cell>> rowEnumerator;
            private IEnumerator<Cell> cellEnumerator;

            public FieldEnumerator(List<List<Cell>> field)
            {
                _field = field;
                rowEnumerator = field.GetEnumerator();
                rowEnumerator.MoveNext();
                cellEnumerator = rowEnumerator.Current.GetEnumerator();
            }

            public bool MoveNext()
            {
                if (cellEnumerator.MoveNext())
                {
                    Current = cellEnumerator.Current;
                    return true;
                }

                if (!rowEnumerator.MoveNext()) return false;

                cellEnumerator = rowEnumerator.Current.GetEnumerator();

                if (!cellEnumerator.MoveNext()) return false;

                Current = cellEnumerator.Current;
                return true;

            }

            public void Reset()
            {
                rowEnumerator.Reset();
                cellEnumerator = rowEnumerator.Current.GetEnumerator();
            }

            public Cell Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                rowEnumerator.Dispose();
                cellEnumerator.Dispose();
            }
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return new FieldEnumerator(this._cellField);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public object Clone()
        {
            var buf = new List<List<Cell>>();
            foreach (var row in _cellField)
            {
                var bufRow = new List<Cell>();
                foreach (var cell in row)
                {
                    bufRow.Add((Cell)cell.Clone());
                }
                buf.Add(bufRow);
            }

            return new Field(buf);
        }

        public void Togle(int x, int y)
        {
            _cellField[x][y] = _cellField[x][y].Togle();
        }
    }
}

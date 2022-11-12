using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellaut.Domain
{
    public class Window
    {
        private List<List<Cell>> _cells;

        /// <summary>
        /// Кол-во клеток в окне
        /// </summary>
        public int Count => _cells.Sum(c => c.Count);
    }
}

namespace Cellaut.Presentation.MAUI.Models
{
    public class LiveGameAutomaton : IAutomaton
    {
        /// <summary>
        /// Высчитывает следующиее состояние field, и изменяет его
        /// </summary>
        /// <param name="field"></param>
        public void NextGeneration(Field field)
        {
            // 1. Если у клетки есть три живых соседей, то клетка выживает
            // 2. Если у клетки больше трех или меньше 2 живых соседей, то клетка умирает
            // 3. Если у клетки две живых сосдей, то в клетке зарождается жизнь 
            var toUpdateCells = new List<Cell>();
            foreach (var cell in field)
            {
                var window = field.GetWindow(cell);

                var aliveCount = window.Sum(r => r.Count(c => c.IsAlive));
                // aliveCount с учетом если cell тоже жива
                if (cell.IsAlive && aliveCount == 4)
                    continue;

                if (cell.IsAlive && (aliveCount > 4 || aliveCount < 3))
                {
                    toUpdateCells.Add(cell.Togle());
                }

                if (!cell.IsAlive && aliveCount == 3)
                {
                    toUpdateCells.Add(cell.Togle());
                }
            }

            field.UpdateField(toUpdateCells);
        }
    }
}

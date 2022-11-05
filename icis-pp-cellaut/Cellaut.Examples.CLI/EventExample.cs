using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellaut.Examples.CLI
{
    public class EventExample
    {
        public event Action<string> OnPrint;

        public void DoWork()
        {
            var str = "EventExample doing work";
            Console.WriteLine(str);

            OnPrint?.Invoke(str);
        }
    }
}

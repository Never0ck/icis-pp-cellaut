// See https://aka.ms/new-console-template for more information

using Cellaut.Examples.CLI;

Console.WriteLine("Hello, World!");

static void PrintWithDatetime(string str)
{
    Console.WriteLine($"{DateTime.Now} | {str}");
}

static void PrintWithRedColor(string str)
{
    Console.ForegroundColor = ConsoleColor.DarkGreen;
    Console.WriteLine($"{str}");
    Console.ResetColor();
}

var eventExampleClass = new EventExample();
// eventExampleClass.OnPrint += PrintWithDatetime;
// eventExampleClass.OnPrint += PrintWithRedColor;
eventExampleClass.DoWork();
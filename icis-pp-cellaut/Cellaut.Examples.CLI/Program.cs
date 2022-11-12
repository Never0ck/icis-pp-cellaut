// See https://aka.ms/new-console-template for more information

//using Cellaut.Examples.CLI;

//Console.WriteLine("Hello, World!");

//static void PrintWithDatetime(string str)
//{
//    Console.WriteLine($"{DateTime.Now} | {str}");
//}

//static void PrintWithRedColor(string str)
//{
//    Console.ForegroundColor = ConsoleColor.DarkGreen;
//    Console.WriteLine($"{str}");
//    Console.ResetColor();
//}

//var eventExampleClass = new EventExample();
//// eventExampleClass.OnPrint += PrintWithDatetime;
//// eventExampleClass.OnPrint += PrintWithRedColor;
//eventExampleClass.DoWork();


using Cellaut.Domain;


void Print(Field field)
{
    foreach (var row in field._cellField)
    {
        foreach (var cell in row)
        {
            Console.Write(cell.IsAlive);
            Console.Write($" {cell.Id.ToString()[..4]}");
            Console.Write("\t");
        }
        Console.WriteLine();
    }
}

var a = new Cell() {IsAlive = true};
var b = new Cell();
var c = new Cell() { IsAlive = true };
var d = new Cell();
var e = new Cell();
var f = new Cell() { IsAlive = true };
var g = new Cell();
var h = new Cell();
var j = new Cell() { IsAlive = true };
var cellField = new List<List<Cell>>()
{
    new List<Cell>() {a, b, c, new Cell()},
    new List<Cell>() {d, e, f, new Cell() {IsAlive = true}},
    new List<Cell>() {g, h, j, new Cell()},
    new List<Cell>() {new Cell(), new Cell(), new Cell(), new Cell() {IsAlive = true}},
};

var field = new Field(cellField);
var MAX = 3;

IAutomaton automaton = new LiveGameAutomaton();

for (int i = 0; i < MAX; i++)
{
    Print(field);
    Console.WriteLine(new string('-', 20));
    automaton.NextGeneration(field);
}

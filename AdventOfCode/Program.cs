// See https://aka.ms/new-console-template for more information
using AdventOfCode;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter the day's number : ");
        int day = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the problem's number : ");
        int number = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("If it exist, enter the version of the file : ");
        string version = Console.ReadLine() ?? string.Empty;

        Riddle riddle = new Riddle(day, number, version);



        Console.WriteLine("The solution is :" + riddle.Solution);
    }
}
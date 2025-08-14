using System;
using DungeonEscape.Entities;

namespace DungeonEscape
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Floor floor = new Floor(30);
            floor.PrintAll();

            while (true)
            {

            }
        }
    }
}
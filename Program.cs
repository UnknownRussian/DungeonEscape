using System;
using System.Threading;
using DungeonEscape.Entities;

namespace DungeonEscape
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Floor floor = new Floor(30);
            Floor floor1 = new Floor(30);
            Floor floor2 = new Floor(30);
            Floor floor3 = new Floor(30);
            Floor.Floors.ForEach(f =>
            {
                Console.Clear();
                f.PlaceStairs();
                f.PrintAll();
                Thread.Sleep(2000);
            });

            while (true)
            {

            }
        }
    }
}
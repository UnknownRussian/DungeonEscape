using System;
using System.Threading;
using DungeonEscape.Entities;

namespace DungeonEscape
{
    public class Program
    {
        public static Player player;

        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.WriteLine("Welcome to Dungeon Escape - Epilepsy Edition!");
            Console.WriteLine("Everytime you open a new game, it's truely a new game!");
            Console.WriteLine();
            Console.WriteLine("Icon Explanation: ");
            Console.WriteLine("X - Player");
            Console.WriteLine("≡ - Escape Ladders");
            Console.WriteLine("± - Key");
            Console.WriteLine("▲ - Stairs Up");
            Console.WriteLine("▼ - Stairs Down");
            Console.WriteLine();
            Console.WriteLine("Player movement:");
            Console.WriteLine("Arrow Key Up     - Move Up");
            Console.WriteLine("Arrow Key Down   - Move Down");
            Console.WriteLine("Arrow Key Left   - Move Left");
            Console.WriteLine("Arrow Key Right  - Move Right");
            Console.WriteLine();
            Console.WriteLine("The rules are simple... Find the key!");
            Console.WriteLine();
            Console.Write("How many floors do you want to explore? (int): ");
            int fA = int.Parse(Console.ReadLine()!);
            Console.Write("What about rooms on each floor? (int): ");
            int rA = int.Parse(Console.ReadLine()!);
            Console.Write("What is your name young one?: ");
            string name = Console.ReadLine()!;
            Console.WriteLine($"When you are ready {name}, press any key to start the game!");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("May the odds be ever in your favor!");
            Thread.Sleep(2000);
            Console.Clear();

            player = new Player(name);

            for (int i = 0; i < fA; i++)
                new Floor(rA);
            Floor.PlaceKey();
            Floor.Floors.ForEach(f =>
            {
                f.PlaceStairs();
            });
            Floor.Floors[player.Position.Floor].PrintAll();

            while (true)
            {
                player.MovePlayer(Floor.Floors[player.Position.Floor]);
            }
        }
    }
}
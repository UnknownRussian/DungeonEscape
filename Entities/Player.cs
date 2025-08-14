using System;
using System.Collections.Generic;
using System.Threading;

namespace DungeonEscape.Entities
{
    public class Player
    {
        public string Name { get; set; }
        private PlayerType type;
        public List<Item> Items { get; private set; }
        public List<Item> EquipedItems { get; private set; }
        public (int X, int Y, int Floor) Position { get; private set; }
        public int Level; // Create function for this
        public int Exp; // Same goes for this BS xD
        public int Health; // And this :P
        public int Lives; // Basically amount of lives until game is over.
        public Stats Stats;
        public bool IsKeyFound;

        public Player(string name)
        {
            this.Name = name;
            this.type = PlayerType.human;
            this.Level = 1;
            this.Exp = 0;
            this.Health = 100;
            this.Stats = new Stats(
                damage: 10,
                defence: 10
            );
            Position = (2, 2, 0);
            IsKeyFound = false;
        }

        public string Icon()
        {
            return type switch
            {
                PlayerType.princess => "👸",
                PlayerType.elf => "🧝",
                PlayerType.mage => "🧙",
                PlayerType.superhero => "🦸",
                PlayerType.human => "🙎",
                PlayerType.prince => "🤴",
                _ => throw new Exception("Error: PlayerType not found...")
            };
        }

        public void MovePlayer(Floor floor)
        {
            floor.UpdateTiles(true);
            floor.PrintAll();
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); 
            (int x, int y, int floor) newPos = Position;

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    newPos.y--;
                    break;
                case ConsoleKey.DownArrow:
                    newPos.y++;
                    break;
                case ConsoleKey.LeftArrow:
                    newPos.x--;
                    break;
                case ConsoleKey.RightArrow:
                    newPos.x++;
                    break;
            }
            //return newPos;

            Position = floor.CheckPlayerMove(newPos.x, newPos.y);

            // Checks if on stairs
            if (floor.Tiles[Position.Y][Position.X] == Tile.StairsUp)
            {
                Position = (Floor.Floors[Position.Floor + 1].StairsDown.X + 1, Floor.Floors[Position.Floor + 1].StairsDown.Y + 2, Position.Floor + 1);
            }
            else if (floor.Tiles[Position.Y][Position.X] == Tile.StairsDown)
            {
                Position = (Floor.Floors[Position.Floor - 1].StairsUp.X - 1, Floor.Floors[Position.Floor - 1].StairsUp.Y - 2, Position.Floor - 1);
            }

            // Check if on key
            if (floor.Tiles[Position.Y][Position.X] == Tile.Key)
            {
                floor.Tiles[Position.Y][Position.X] = Tile.Floor;
                IsKeyFound = true;
                Floor.PlaceLadders();
                Console.WriteLine();
                Console.WriteLine("You found the key! :D");
                Console.WriteLine("A ladder has been put down for you to escape!");
                Thread.Sleep(2000);
            }

            if (floor.Tiles[Position.Y][Position.X] == Tile.Ladder && IsKeyFound)
            {
                Console.WriteLine();
                Console.Clear();
                Console.WriteLine($"You did it {Name}!");
                Console.WriteLine("You WON!");
                Console.WriteLine("The Dungeon Escape - Epilepsy Edition!");
                Console.WriteLine("If you are brave again, open the game again... But remember everything changes and nothing is new!");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }

    public enum PlayerType
    {
        princess,
        prince,
        spy,
        superhero,
        elf,
        mage,
        human
    }
}
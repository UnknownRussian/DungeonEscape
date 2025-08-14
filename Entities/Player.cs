using System;
using System.Collections.Generic;

namespace DungeonEscape.Entities
{
    public class Player
    {
        public string Name { get; private set; }
        private PlayerType type;
        public List<Item> Items { get; private set; }
        public List<Item> EquipedItems { get; private set; }
        private (int X, int Y) Position;
        public int Level; // Create function for this
        public int Exp; // Same goes for this BS xD
        public int Health; // And this :P
        public int Lives; // Basically amount of lives until game is over.
        public Stats Stats;

        public Player(string name)
        {
            this.Name = name;
            this.type = PlayerType.human;
            this.Position.X = 0;
            this.Position.Y = 0;
            this.Level = 1;
            this.Exp = 0;
            this.Health = 100;
            this.Stats = new Stats(
                damage: 10,
                defence: 10
            );
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

        public (int x, int y) MovePlayer()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true = don't show key in console
            (int x, int y) newPos = Position;

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
            return newPos;
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
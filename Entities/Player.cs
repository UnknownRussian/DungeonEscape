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
        private int xPos, yPos;
        private int lastXPos, lastYPos;
        public int Level { get; private set; } // Create function for this
        public int Exp { get; private set; } // Same goes for this BS xD
        public int Health { get; private set; } // And this :P
        public int Lives { get; private set; } // Basically amount of lives until game is over.
        public Stats Stats { get; protected set; }

        public Player(string name)
        {
            this.Name = name;
            this.type = PlayerType.human;
            this.xPos = 0;
            this.yPos = 0;
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

        public void SetLastPos()
        {
            this.lastXPos = xPos;
            this.lastYPos = yPos;
        }

        public void SetPos(int x, int y)
        {
            this.xPos = x;
            this.yPos = y;
        }

        public (int x, int y) GetLastPos()
        {
            return (lastXPos, lastYPos);
        }

        public (int x, int y) GetPos()
        {
            return (xPos, yPos);
        }

        public void MovePlayer(Map map)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true = don't show key in console

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp(0);
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown(Map.LengthYLimit);
                    break;
                case ConsoleKey.LeftArrow:
                    MoveLeft(0);
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight(Map.LengthXLimit);
                    break;
            }

            map.MovePlayerOnMap(this);
        }

        public void MoveLeft(int borderLeft) { if (xPos - 1 != borderLeft) xPos--; }
        public void MoveRight(int borderRight) { if (xPos + 3 != borderRight) xPos++; }
        public void MoveUp(int borderTop) { if (yPos - 1 != borderTop) yPos--; }
        public void MoveDown(int borderBottom) { if (yPos + 2 != borderBottom) yPos++; }
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
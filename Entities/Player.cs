namespace DungeonEscape.Entities
{
    public class Player
    {
        public string Name { get; private set; }
        private PlayerType type;
        public List<Item> Items { get; private set; }
        public List<Item> EquipedItems { get; private set; }
        private int xPosition, yPosition;
        public int Level { get; private set; } // Create function for this
        public int Exp { get; private set; } // Same goes for this BS xD
        public int Health { get; private set; } // And this :P
        public int Lives { get; private set; } // Basically amount of lives until game is over.
        public Stats Stats { get; protected set; }

        public Player(string name)
        {
            this.Name = name;
            this.type = PlayerType.human;
            this.xPosition = 0;
            this.yPosition = 0;
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

        /// <summary>
        /// Manually override player position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPos(int x, int y)
        {
            this.xPosition = x;
            this.yPosition = y;
        }

        public void MoveLeft(int borderLeft) => xPosition = (xPosition == borderLeft) ? xPosition : xPosition--;
        public void MoveRight(int borderRight) => xPosition = (xPosition == borderRight) ? xPosition : xPosition++;
        public void MoveUp(int borderTop) => yPosition = (yPosition == borderTop) ? yPosition : yPosition--;
        public void MoveDown(int borderBottom) => yPosition = (yPosition == borderBottom) ? yPosition : yPosition++;
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
namespace DungeonEscape.Entities
{
    public class Player
    {
        public string Name { get; private set; }
        protected PlayerType type;
        protected List<Item> items;
        protected List<Item> equipedItems;
        private int xPosition, yPosition;
        protected int level; // Create function for this
        protected int xp; // Same goes for this BS xD
        protected int health; // And this :P
        protected int lives; // Basically amount of lives until game is over.
        public Stats Stats { get; protected set; }

        public Player(string name)
        {
            this.Name = name;
            this.type = PlayerType.human;
            this.xPosition = 0;
            this.yPosition = 0;
            this.level = 1;
            this.xp = 0;
            this.health = 100;
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
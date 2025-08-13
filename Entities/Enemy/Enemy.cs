using DungeonEscape.Enums;

namespace DungeonEscape.Entities.Enemy
{
    public class Enemy
    {
        protected EnemyType type;
        protected List<Item> items;
        protected List<Item> equipedItems;
        private int xPosition, yPosition;
        protected int level; // Create function for this
        protected int xp; // Same goes for this BS xD
        protected int health; // And this :P
        public Stats Stats { get; protected set; }

        public Enemy()
        {
            this.type = EnemyType.orc;
        }

        public string GetIcon()
        {
            return type switch
            {
                EnemyType.orc => "👹",
                EnemyType.zombie => "🧟",
                EnemyType.darkWizard => "🧛",
                _ => throw new Exception("Error: EnemyTpe not found...")
            };
        }

        protected string GetName()
        {
            return type switch
            {
                EnemyType.orc => "Orc",
                EnemyType.zombie => "Zombie",
                EnemyType.darkWizard => "Dark Wizard",
                _ => throw new Exception("Error: EnemyTpe not found...")
            };
        }

        public void SetPos(int x, int y)
        {
            this.xPosition = x;
            this.yPosition = y;
        }
    }
}
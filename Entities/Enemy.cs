using System;

namespace DungeonEscape.Entities
{
    public class Enemy
    {
        public EnemyType Type { get; private set; }
        public int Exp { get; private set; }
        public int Health { get; private set; } 
        private int xPosition, yPosition;
        public Stats Stats { get; protected set; }

        public Enemy()
        {
            this.Type = EnemyType.orc;
        }

        public string Icon()
        {
            return Type switch
            {
                EnemyType.orc => "👹",
                EnemyType.zombie => "🧟",
                EnemyType.darkWizard => "🧛",
                EnemyType.dragon => "🐉",
                EnemyType.snake => "🐍",
                EnemyType.spider => "🕷",
                EnemyType.scorpion => "🦂",
                _ => throw new Exception("Error: EnemyType not found...")
            };
        }

        public string Name()
        {
            return Type switch
            {
                EnemyType.orc => "Orc",
                EnemyType.zombie => "Zombie",
                EnemyType.darkWizard => "Dark Wizard",
                EnemyType.dragon => "Dragon",
                EnemyType.snake => "Snake",
                EnemyType.spider => "Huge Spider",
                EnemyType.scorpion => "Mega Scorpion",
                _ => throw new Exception("Error: EnemyType not found...")
            };
        }

        public void SetPos(int x, int y)
        {
            this.xPosition = x;
            this.yPosition = y;
        }
    }

    public enum EnemyType
    {
        orc,
        darkWizard,
        zombie,
        dragon,
        snake,
        spider,
        scorpion
    }
}
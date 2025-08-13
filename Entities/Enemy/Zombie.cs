using DungeonEscape.Enums;

namespace DungeonEscape.Entities.Enemy
{
    public class Zombie : Enemy
    {
        public Zombie() : base()
        {
            this.type = EnemyType.zombie;
        }
    }
}
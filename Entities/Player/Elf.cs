using DungeonEscape.Enums;

namespace DungeonEscape.Entities.Player
{
    public class Elf : Player
    {
        public Elf(string name) : base(name)
        {
            this.type = PlayerType.elf;
            this.health = 100;
            this.Stats = new Stats(
                damage: 20,
                defence: 10,
                speed: 30,
                ultimateDamage: 10
            );
        }
    }
}
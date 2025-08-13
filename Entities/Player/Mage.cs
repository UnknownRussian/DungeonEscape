namespace DungeonEscape.Entities.Player
{
    public class Mage : Player
    {
        public Mage(string name) : base(name)
        {
            this.type = Enums.PlayerType.mage;
            this.health = 100;
            this.Stats = new Stats(
                damage: 1,
                defence: 5,
                knowledge: 30,
                speed: 10,
                health: 10,
                ultimateDamage: 50
            );
        }
    }
}
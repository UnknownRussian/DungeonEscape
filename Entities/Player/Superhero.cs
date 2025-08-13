namespace DungeonEscape.Entities.Player
{
    public class Superhero : Player
    {
        public Superhero(string name) : base(name)
        {
            this.type = Enums.PlayerType.superhero;
            this.Stats = new Stats(
                damage: 100,
                defence: 100,
                speed: 100,
                health: 100,
                ultimateDamage: 100
            );
        }
    }
}
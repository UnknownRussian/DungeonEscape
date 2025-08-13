namespace DungeonEscape.Entities.Player
{
    public class Prince : Player
    {
        public Prince(string name) : base(name)
        {
            this.type = Enums.PlayerType.prince;
            this.Stats = new Stats(
                damage: 5,
                defence: 5,
                knowledge: 5,
                speed: 2,
                ultimateDamage: 20,
                expBonus: 2
            );
        }
    }
}
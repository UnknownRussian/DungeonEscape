namespace DungeonEscape.Entities.Player
{
    public class Princess : Player
    {
        public Princess(string name) : base(name)
        {
            this.type = Enums.PlayerType.princess;
            this.health = 100;
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
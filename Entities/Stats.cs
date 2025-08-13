namespace DungeonEscape.Entities
{
    public class Stats
    {
        public int Damage { get; private set; } = 0;
        public int Defence { get; private set; } = 0;
        public int Knowledge { get; private set; } = 0;
        public int Speed { get; private set; } = 0;
        public int Health { get; private set; } = 0;
        public int UltimateDamage { get; private set; } = 0;
        public int ExpBonus { get; private set; } = 0;

        public Stats()
        {
            
        }
    }
}
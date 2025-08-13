namespace DungeonEscape.Entities
{
    public class Stats
    {
        public int Damage { get; set; }
        public int Defence { get; set; }
        public int Knowledge { get; set; }
        public int Speed { get; set; }
        public int Health { get; set; }
        public int UltimateDamage { get; set; }
        public int ExpBonus { get; set; }

        public Stats(
            int damage = 0,
            int defence = 0,
            int knowledge = 0,
            int speed = 0,
            int health = 0,
            int ultimateDamage = 0,
            int expBonus = 0)
        {
            Damage = damage;
            Defence = defence;
            Knowledge = knowledge;
            Speed = speed;
            Health = health;
            UltimateDamage = ultimateDamage;
            ExpBonus = expBonus;
        }
    }
}
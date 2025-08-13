namespace DungeonEscape.Entities
{
    public class Elf : Player
    {
        public Elf(string name) : base (name)
        {
            this.type = PlayerType.elf;
        }
    }
}
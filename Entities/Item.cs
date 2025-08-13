using DungeonEscape.Enums;

namespace DungeonEscape.Entities
{
    public class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public Stats Stats { get; private set; }

        public Item(string name, ItemType type, Stats stats)
        {
            this.Name = name;
            this.Type = type;
            this.Stats = stats;
        }
    }
}
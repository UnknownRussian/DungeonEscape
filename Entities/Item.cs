namespace DungeonEscape.Entities
{
    public class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public Stats Stats { get; private set; }

        public Item(string name, string icon, ItemType type, Stats stats)
        {
            this.Type = type;
            this.Stats = stats;
        }

        public string Icon()
        {
            return "";
        }
    }
    
    public enum ItemType
    {
        weapon,
        accessory,
        key,
        spell
    }
}
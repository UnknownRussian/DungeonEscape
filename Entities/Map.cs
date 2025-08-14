using System;

namespace DungeonEscape.Entities
{
    public class Map
    {
        public Tile[][] map { get; private set; }
        public int floor { get; private set; } // Last floor should have an exit.
        public static int LengthXLimit { get; set; } = 60;
        public static int LengthYLimit { get; set; } = 20;

        public Map()
        {

        }

        private void GenerateEdges()
        {
            map = new Tile[LengthYLimit][];
            for (int y = 0; y < map.Length; y++)
            {
                map[y] = new Tile[LengthXLimit];
                for (int x = 0; x < map[y].Length; x++)
                    if (y == 0 || y == map.Length - 1 || x == 0 || x == map[y].Length - 1)
                        map[y][x] = Tile.Wall;
                    else
                        map[y][x] = Tile.Floor;
            }
        }

        private void GenerateRooms()
        {
            
        }
    }

    public enum Tile
    {
        Wall,
        Entrance,
        Floor,
        StairsUp,
        StairsDown
    }
}
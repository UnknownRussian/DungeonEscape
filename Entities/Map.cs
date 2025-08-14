using System;

namespace DungeonEscape.Entities
{
    public class Map
    {
        public string[][] map { get; private set; }
        public int floor { get; private set; } // Last floor should have an exit.
        public static int LengthXLimit { get; set; } = 60;
        public static int LengthYLimit { get; set; } = 20;

        public Map(Player player)
        {
            GenerateEdges();
            var pos = player.GetPos();
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write(player.Icon());
        }

        private void GenerateEdges()
        {
            map = new string[LengthYLimit][];
            for (int y = 0; y < map.Length; y++)
            {
                map[y] = new string[LengthXLimit];
                for (int x = 0; x < map[y].Length; x++)
                    if (y == 0 || y == map.Length - 1 || x == 0 || x == map[y].Length - 1)
                        map[y][x] = "█";
                    else
                        map[y][x] = " ";
            }
        }

        public void MovePlayerOnMap(Player player)
        {
            var pos = player.GetLastPos();
            map[pos.y][pos.x] = " ";

            pos = player.GetPos();
            map[pos.y][pos.x] = player.Icon();
        }

        public void PrintMap()
        {
            for (int y = 0; y < map.Length; y++)
                for (int x = 0; x < map[y].Length; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(map[y][x]);
                }
        }
    }
}
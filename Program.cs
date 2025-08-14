using System;
using DungeonEscape.Entities;

namespace DungeonEscape
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            Player player = new Player("Unknown");
            player.SetPos(1, 1);
            player.SetLastPos();
            Map map = new Map(player);
            map.PrintMap();

            while (true)
            {
                player.MovePlayer(map);
                player.SetLastPos();
                map.PrintMap();
            }
        }
    }
}
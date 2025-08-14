using System;

namespace DungeonEscape.Entities
{
    public class Room
    {
        public int X, Y, Width, Height;
        public (int x, int y) Entrance;

        public Room(int x, int y, int width, int height, Entrance entranceSide)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            Random rand = new Random();

            switch (entranceSide)
            {
                case Entities.Entrance.Top: Entrance = (X + rand.Next(1, Width - 1), Y); break;
                case Entities.Entrance.Right: Entrance = (X + Width - 1, Y + rand.Next(1, Height - 1)); break;
                case Entities.Entrance.Bottom: Entrance = (X + rand.Next(1, Width - 1), Y + Height - 1); break;
                case Entities.Entrance.Left: Entrance = (X, Y + rand.Next(1, Height - 1)); break;
            }
        }
    }

    public enum Entrance
    {
        Top,
        Bottom,
        Left,
        Right
    }
}
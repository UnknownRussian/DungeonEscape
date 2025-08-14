using System;
using System.Collections.Generic;

namespace DungeonEscape.Entities
{
    public class Room
    {
        // Static Values
        public static int MaxHeight = 20;
        public static int MaxWidth = 10;
        public static int MinHeight = 5;
        public static int MinWidth = 5;

        // Non-Static Values
        public int Width, Height;
        public List<(int X, int Y)> Doors { get; private set; }
        public (int X, int Y) StairsUp { get; private set; }
        public (int X, int Y) StairsDown { get; private set; }
        public (int X, int Y) EscapeLadder { get; private set; }
        public (int X, int Y) Key { get; private set; }


        public (int x, int y) CornerTopLeft, CornerTopRight, CornerBottomLeft, CornerBottomRight;

        public Room()
        {
            Doors = new List<(int X, int Y)>();
            Random r = new Random();
            Width = r.Next(MinWidth, MaxWidth + 1);
            Height = r.Next(MinHeight, MaxHeight + 1);
            StairsUp = (-1, -1);
            StairsDown = (-1, -1);
            Key = (-1, -1);
        }

        /// <summary>
        /// Add Room position (x, y) on the Floor. The position is the top left corner of the Room.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddCornerPositions(int x, int y)
        {
            CornerTopLeft = (x, y);
            CornerTopRight = (x + Width, y);
            CornerBottomLeft = (x, y + Height);
            CornerBottomRight = (x + Width, y + Height);
        }

        /// <summary>
        /// Add Door Position to Room.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddDoor(int x, int y)
        {
            Doors.Add((x, y));
        }

        /// <summary>
        /// Add Stairs Up. Should only be in the last room of the Floor.
        /// Bare in mind, the last Floor should not have Stairs Up.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddStairsUp(int x, int y)
        {
            StairsUp = (x, y);
        }

        /// <summary>
        /// Add Stairs Down. Should only be in the first room of the Floor.
        /// Bare in mind, the first Floor should not have Stairs Down.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddStairsDown(int x, int y)
        {
            StairsDown = (x, y);
        }

        /// <summary>
        /// Add Key to Room
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddKey(int x, int y)
        {
            Key = (x, y);
        }

        public void AddEscapeLadders(int x, int y)
        {
            EscapeLadder = (x, y);
        }
    }
}
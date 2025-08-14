using System;

namespace DungeonEscape.Entities
{
    public class Room
    {
        // Static Values
        public static int MaxHeight = 5;
        public static int MaxWidth = 5;
        public static int MinHeight = 3;
        public static int MinWidth = 3;

        // Non-Static Values
        public int Width, Height;
        public (int X, int Y) EntrenceDoor;
        public (int X, int Y) ExitDoor;
        public (int X, int Y) StairsUp;
        public (int X, int Y) StairsDown;


        public (int x, int y) CornerTopLeft, CornerTopRight, CornerBottomLeft, CornerBottomRight;

        public Room()
        {
            Random r = new Random();
            Width = r.Next(MinWidth, MaxWidth + 1);
            Height = r.Next(MinHeight, MaxHeight + 1);
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
        /// Add Entrence Postion. Should be facing on one of the Exit of another room.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddEntrencePosition(int x, int y)
        {
            EntrenceDoor = (x, y);
        }

        /// <summary>
        /// Add Exit Postion. Should be facing on one of the Entrences of another room.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void AddExitPosition(int x, int y)
        {
            ExitDoor = (x, y);
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
    }
}
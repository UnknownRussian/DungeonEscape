using System;
using System.Collections.Generic;

namespace DungeonEscape.Entities
{
    public class Floor
    {
        public Tile[][] Tiles { get; private set; }
        public List<Room> Rooms;
        public int Level { get; private set; } // Last floor should have an exit.
        public static int LengthXLimit { get; set; } = 60;
        public static int LengthYLimit { get; set; } = 20;

        public Floor(int amountOfRooms = 5)
        {
            Rooms = new List<Room>();
            GenerateEdges();
            GenerateRooms(amountOfRooms);
        }

        private void GenerateEdges()
        {
            Tiles = new Tile[LengthYLimit][];
            for (int y = 0; y < Tiles.Length; y++)
            {
                Tiles[y] = new Tile[LengthXLimit];
                for (int x = 0; x < Tiles[y].Length; x++)
                {
                    if (y == 0 || y == Tiles.Length - 1 || x == 0 || x == Tiles[y].Length - 1)
                    {
                        Tiles[y][x] = Tile.Edge;
                    }
                    else
                    {
                        Tiles[y][x] = Tile.None;
                    }
                }
            }
        }

        private void GenerateRooms(int amountOfRooms)
        {
            for (int i = 0; i < amountOfRooms; i++)
            {
                Room room = new Room();
                PlaceRoom(room);
            }
        }

        private void PlaceRoom(Room room)
        {
            if (Rooms.Count == 0)
            {
                room.AddCornerPositions(0, 0);
                Rooms.Add(room);
            }
            else if (Rooms.Count > 0)
            {
                var location = FindLocation(room);
            }
        }

        private void UpdateTiles()
        {
            Rooms.ForEach(room =>
            {
                for (int y = 0; y < Tiles.Length; y++)
                {
                    for (int x = 0; x < Tiles[y].Length; x++)
                    {
                        bool isTopWall = x >= room.CornerTopLeft.x && x <= room.CornerTopRight.x && y == room.CornerTopLeft.y && y == room.CornerTopRight.y;
                        bool isBottomWall = x >= room.CornerBottomLeft.x && x <= room.CornerBottomRight.x && y == room.CornerBottomLeft.y && y == room.CornerBottomRight.y;
                        bool isLeftWall = y >= room.CornerTopLeft.y && y <= room.CornerBottomLeft.y && x == room.CornerTopLeft.x && x == room.CornerBottomLeft.x;
                        bool isRightWall = y >= room.CornerTopRight.y && y <= room.CornerBottomRight.y && x == room.CornerTopRight.x && x == room.CornerBottomRight.x;

                        // Places Walls
                        if (isTopWall || isBottomWall || isLeftWall || isRightWall)
                        {
                            Tiles[y][x] = Tile.Wall;
                        }

                                                


                    }
                }
            });
        }

        private (int x, int y) FindLocation(Room room)
        {
            // May only place on none or edge tiles.
        }
    }

    public enum Tile
    {
        Wall,
        Entrance,
        Floor,
        StairsUp,
        StairsDown,
        None,
        Edge,
        Exit
    }
}
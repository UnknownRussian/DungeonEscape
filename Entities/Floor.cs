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
            PlaceDoors();
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
                if (room.CornerTopRight.y < LengthYLimit)
                {
                    Rooms.Add(room);
                    UpdateTiles();
                }
            }
            else if (Rooms.Count > 0)
            {
                var location = FindLocation(room);
                if (location.x != -1 || location.y != -1)
                {
                    room.AddCornerPositions(location.x, location.y);
                    Rooms.Add(room);
                    UpdateTiles();
                }
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

                        // Places Wall
                        if (isTopWall || isBottomWall || isLeftWall || isRightWall)
                        {
                            Tiles[y][x] = Tile.Wall;
                        }

                        bool isFloor = x > room.CornerTopLeft.x && x < room.CornerTopRight.x && y > room.CornerTopLeft.y && y < room.CornerBottomLeft.y;

                        // Places Floor
                        if (isFloor)
                        {
                            Tiles[y][x] = Tile.Floor;
                        }

                        // Places Door
                        if (room.Doors.Count > 0 && room.Doors.Contains((x, y)))
                        {
                            Tiles[y][x] = Tile.Door;
                        }
                    }
                }
            });
        }

        private (int x, int y) FindLocation(Room room)
        {
            room.AddCornerPositions(0, 0);

            for (int y = 0; y < Tiles.Length; y++)
            {
                for (int x = 0; x < Tiles[y].Length; x++)
                {
                    if (CanPlaceRoomAt(x, y, room))
                    {
                        return (x, y);
                    }
                }
            }

            return (-1, -1);
        }

        private bool CanPlaceRoomAt(int startX, int startY, Room room)
        {
            // Checks if out of bounds
            if (startY + room.CornerBottomRight.y >= LengthYLimit ||
                startX + room.CornerBottomRight.x >= LengthXLimit)
                return false;

            // Checks if room is overlapping
            for (int y = 0; y <= room.CornerBottomRight.y; y++)
            {
                for (int x = 0; x <= room.CornerBottomRight.x; x++)
                {
                    if (Tiles[startY + y][startX + x] != Tile.None &&
                        Tiles[startY + y][startX + x] != Tile.Edge)
                        return false;
                }
            }

            return true;
        }

        public void PrintAll()
        {
            for (int y = 0; y < Tiles.Length; y++)
            {
                for (int x = 0; x < Tiles[y].Length; x++)
                {
                    string icon = Tiles[y][x] switch
                    {
                        Tile.Wall => "█",  // brick
                        Tile.Door => " ",  // door
                        Tile.Floor => " ",  // small white square
                        Tile.StairsUp => "🔼", // up arrow
                        Tile.StairsDown => "🔽", // down arrow
                        Tile.None => "▚", // black large square
                        Tile.Edge => "░", // rock
                        Tile.Enemy => "👾", // alien monster
                        Tile.Item => "🎁", // wrapped gift
                        _ => " ",  // fallback
                    };

                    Console.Write(icon);
                }
                Console.WriteLine();
            }
        }

        public void PlaceDoors()
        {
            Random rnd = new Random();

            for (int i = 0; i < Rooms.Count; i++)
            {
                for (int j = i + 1; j < Rooms.Count; j++)
                {
                    var a = Rooms[i];
                    var b = Rooms[j];

                    // Right wall of a room (a on left, b on right)
                    if (a.CornerTopRight.x == b.CornerTopLeft.x - 1 && a.CornerBottomRight.x == b.CornerBottomLeft.x - 1)
                    {
                        int yRightTop = Math.Max(a.CornerTopRight.y, b.CornerTopLeft.y);
                        int yRightBottom = Math.Min(a.CornerBottomRight.y, b.CornerBottomLeft.y);
                        if (yRightTop < yRightBottom)
                        {
                            int yPosDoor = rnd.Next(yRightTop + 1, yRightBottom); // +1/-1 avoids corners
                            int xPosDoor = a.CornerTopRight.x;
                            a.AddDoor(xPosDoor, yPosDoor);
                            b.AddDoor(xPosDoor + 1, yPosDoor);
                        }
                    }

                    // Left wall of a room (a on right, b on left)
                    if (a.CornerTopLeft.x == b.CornerTopRight.x + 1 && a.CornerBottomLeft.x == b.CornerBottomRight.x + 1)
                    {
                        int yLeftTop = Math.Max(a.CornerTopLeft.y, b.CornerTopRight.y);
                        int yLeftBottom = Math.Min(a.CornerBottomLeft.y, b.CornerBottomRight.y);
                        if (yLeftTop < yLeftBottom)
                        {
                            int yPosDoor = rnd.Next(yLeftTop + 1, yLeftBottom);
                            int xPosDoor = a.CornerTopLeft.x;
                            a.AddDoor(xPosDoor, yPosDoor);
                            b.AddDoor(xPosDoor - 1, yPosDoor);
                        }
                    }

                    // Top wall of a room (a below, b above)
                    if (a.CornerTopLeft.y == b.CornerBottomLeft.y + 1 && a.CornerTopRight.y == b.CornerBottomRight.y + 1)
                    {
                        int xTopLeft = Math.Max(a.CornerTopLeft.x, b.CornerBottomLeft.x);
                        int xTopRight = Math.Min(a.CornerTopRight.x, b.CornerBottomRight.x);
                        if (xTopLeft < xTopRight)
                        {
                            int xPosDoor = rnd.Next(xTopLeft + 1, xTopRight);
                            int yPosDoor = a.CornerTopLeft.y;
                            a.AddDoor(xPosDoor, yPosDoor);
                            b.AddDoor(xPosDoor, yPosDoor - 1);
                        }
                    }

                    // Bottom wall of a room (a above, b below)
                    if (a.CornerBottomLeft.y == b.CornerTopLeft.y - 1 && a.CornerBottomRight.y == b.CornerTopRight.y - 1)
                    {
                        int xBottomLeft = Math.Max(a.CornerBottomLeft.x, b.CornerTopLeft.x);
                        int xBottomRight = Math.Min(a.CornerBottomRight.x, b.CornerTopRight.x);
                        if (xBottomLeft < xBottomRight)
                        {
                            int xPosDoor = rnd.Next(xBottomLeft + 1, xBottomRight);
                            int yPosDoor = a.CornerBottomLeft.y;
                            a.AddDoor(xPosDoor, yPosDoor);
                            b.AddDoor(xPosDoor, yPosDoor + 1);
                        }
                    }
                }
            }

            UpdateTiles();
        }
    }

    public enum Tile
    {
        Wall,
        Floor,
        StairsUp,
        StairsDown,
        None,
        Edge,
        Enemy,
        Item,
        Door
    }
}
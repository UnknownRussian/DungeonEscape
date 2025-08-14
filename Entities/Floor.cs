using System;
using System.Collections.Generic;

namespace DungeonEscape.Entities
{
    public class Floor
    {
        // Static
        public static List<Floor> Floors = new List<Floor>();
        public static int LengthXLimit { get; set; } = 60;
        public static int LengthYLimit { get; set; } = 20;

        // Non-static
        public Tile[][] Tiles { get; private set; }
        public List<Room> Rooms { get; private set; }
        public (int X, int Y) StairsUp { get; private set; }
        public (int X, int Y) StairsDown { get; private set; } 

        public Floor(int amountOfRooms = 5)
        {
            Rooms = new List<Room>();
            GenerateEdges();
            GenerateRooms(amountOfRooms);
            PlaceDoors();
            Floors.Add(this);
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

        public void UpdateTiles(bool updatePlayer = false)
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

                // Place Stairs Down
                if (room.StairsDown.X != -1 && room.StairsDown.Y != -1)
                {
                    Tiles[room.StairsDown.Y][room.StairsDown.X] = Tile.StairsDown;
                }

                // Place Stairs Down
                if (room.StairsUp.X != -1 && room.StairsUp.Y != -1)
                {
                    Tiles[room.StairsUp.Y][room.StairsUp.X] = Tile.StairsUp;
                }

                // Place Key
                if (room.Key.X != -1 && room.Key.Y != -1)
                {
                    Tiles[room.Key.Y][room.Key.X] = Tile.Key;
                }

                // Place Ladders (Only if key is found xD)
                if (Program.player.IsKeyFound && room.EscapeLadder.X != -1 && room.EscapeLadder.Y != -1)
                {
                    Tiles[room.EscapeLadder.Y][room.EscapeLadder.X] = Tile.Ladder;
                }

                if (updatePlayer)
                {
                    var pPos = Program.player.Position;
                    Tiles[pPos.Y][pPos.X] = Tile.Player;
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
            Console.Clear();
            for (int y = 0; y < Tiles.Length; y++)
            {
                for (int x = 0; x < Tiles[y].Length; x++)
                {
                    string icon = Tiles[y][x] switch
                    {
                        Tile.Wall => "█",  // brick
                        Tile.Door => " ",  // door
                        Tile.Floor => " ",  // small white square
                        Tile.StairsUp => "▲", // up arrow
                        Tile.StairsDown => "▼", // down arrow
                        Tile.None => "▚", // black large square
                        Tile.Edge => "░", // rock
                        Tile.Enemy => "👾", // alien monster
                        Tile.Player => "X",
                        Tile.Key => "±", // key
                        Tile.Ladder => "≡",
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

        public void PlaceStairs()
        {
            // Only place stairs if more than 1 floor
            if (Floors.Count != 1)
            {
                // Only Stairs Up
                if (Floors.IndexOf(this) == 0)
                {
                    int i = Rooms.Count - 1;
                    Rooms[i].AddStairsUp(Rooms[i].CornerBottomRight.x - 1, Rooms[i].CornerBottomRight.y - 1);
                    StairsUp = (Rooms[i].CornerBottomRight.x - 1, Rooms[i].CornerBottomRight.y - 1);
                }
                // Only Stairs Down
                if (Floors.IndexOf(this) == Floors.Count - 1)
                {
                    Rooms[0].AddStairsDown(Rooms[0].CornerTopLeft.x + 1, Rooms[0].CornerTopLeft.y + 1);
                    StairsDown = (Rooms[0].CornerTopLeft.x + 1, Rooms[0].CornerTopLeft.y + 1);
                }
                // Stairs Up and Down
                if (Floors.IndexOf(this) != 0 && Floors.IndexOf(this) != Floors.Count - 1)
                {
                    int i = Rooms.Count - 1;
                    Rooms[i].AddStairsUp(Rooms[i].CornerBottomRight.x - 1, Rooms[i].CornerBottomRight.y - 1);
                    Rooms[0].AddStairsDown(Rooms[0].CornerTopLeft.x + 1, Rooms[0].CornerTopLeft.y + 1);
                    StairsUp = (Rooms[i].CornerBottomRight.x - 1, Rooms[i].CornerBottomRight.y - 1);
                    StairsDown = (Rooms[0].CornerTopLeft.x + 1, Rooms[0].CornerTopLeft.y + 1);
                }

                UpdateTiles();
            }
        }

        public static void PlaceKey()
        {
            Random r = new Random();
            int floor = Floors.Count != 1 ? r.Next(0, Floors.Count) : 0;
            int room = Floors[floor].Rooms.Count != 1 ? r.Next(0, Floors[floor].Rooms.Count) : 0;

            Floors[floor].Rooms[room].AddKey(Floors[floor].Rooms[room].CornerBottomLeft.x + 1, Floors[floor].Rooms[room].CornerBottomLeft.y - 1);
        }

        public static void PlaceLadders()
        {
            Random r = new Random();
            int floor = Floors.Count != 1 ? r.Next(0, Floors.Count) : 0;
            int room = Floors[floor].Rooms.Count != 1 ? r.Next(0, Floors[floor].Rooms.Count) : 0;

            Floors[floor].Rooms[room].AddEscapeLadders(Floors[floor].Rooms[room].CornerTopRight.x - 1, Floors[floor].Rooms[room].CornerTopRight.y + 1);
        }

        public (int x, int y, int floor) CheckPlayerMove(int x, int y)
        {
            if (Tiles[y][x] == Tile.Floor || Tiles[y][x] == Tile.Key || Tiles[y][x] == Tile.Door || Tiles[y][x] == Tile.StairsDown || Tiles[y][x] == Tile.StairsUp || Tiles[y][x] == Tile.Ladder)
            {
                Tiles[y][x] = Tile.Player;
                UpdateTiles();
                return (x, y, Floors.IndexOf(this));
            }
            else
                return (Program.player.Position.X, Program.player.Position.Y, Floors.IndexOf(this));
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
        Key,
        Door,
        Player,
        Ladder
    }
}
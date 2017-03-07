using DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventures
{
    public class Room
    {
        private bool debug;
        private int doorCounter;
        private VectorInt roomPosition;
        private StringBuilder description = new StringBuilder();

        public string Description { get { return description.ToString(); } }
        public Dictionary<string, RoomDoors> Doors;

        public Room(VectorInt Position, string EntryDirection,bool initialRoom, bool Debug)
        {
            doorCounter = 0;
            this.debug = Debug;
            roomPosition = Position;
            Doors = new Dictionary<string, RoomDoors>();

            #region Generate doors
            double willHaveDoor;
            
            if (initialRoom)
            {
                #region Initial Room
                #region Doors
                willHaveDoor = Program.rnd.Next(100);
                if (debug) System.Console.Write("North: " + willHaveDoor + " ");
                if (willHaveDoor > 50) Doors["NORTH"] = new RoomDoors(true); else Doors["NORTH"] = new RoomDoors(false);

                willHaveDoor = Program.rnd.Next(100);
                if (debug) System.Console.Write("South: " + willHaveDoor + " ");
                if (willHaveDoor > 50) Doors["SOUTH"] = new RoomDoors(true); else Doors["SOUTH"] = new RoomDoors(false);

                willHaveDoor = Program.rnd.Next(100);
                if (debug) System.Console.Write("East: " + willHaveDoor + " ");
                if (willHaveDoor > 50) Doors["EAST"] = new RoomDoors(true); else Doors["EAST"] = new RoomDoors(false);

                willHaveDoor = Program.rnd.Next(100);
                if (debug) System.Console.Write("West: " + willHaveDoor + " ");
                if (willHaveDoor > 50) Doors["WEST"] = new RoomDoors(true); else Doors["WEST"] = new RoomDoors(false);

                willHaveDoor = Program.rnd.Next(100);
                if (debug) System.Console.Write("Up: " + willHaveDoor + " ");
                if (willHaveDoor > 50) Doors["UP"] = new RoomDoors(true); else Doors["UP"] = new RoomDoors(false);

                willHaveDoor = Program.rnd.Next(100);
                if (debug) System.Console.Write("Down: " + willHaveDoor + "\r\n");
                if (willHaveDoor > 50) Doors["DOWN"] = new RoomDoors(true); else Doors["DOWN"] = new RoomDoors(false);
                #endregion
                description.Append("One wall of the room is marked with the word Origo.\r\n");
                #endregion
            }
            else
            {
                #region North
                if (Program.engine.gameWorld.Rooms.ContainsKey(new VectorInt(roomPosition.X, roomPosition.Y - 1, roomPosition.Z).ToString()))
                {
                    Doors["NORTH"] = new RoomDoors(Program.engine.gameWorld.Rooms[new VectorInt(roomPosition.X, roomPosition.Y - 1, roomPosition.Z).ToString()].Doors["SOUTH"].IsDoor);
                }
                else
                {
                    willHaveDoor = Program.rnd.Next(100);
                    if (willHaveDoor > 50) Doors["NORTH"] = new RoomDoors(true); else Doors["NORTH"] = new RoomDoors(false);
                }
                #endregion
                #region East
                if (Program.engine.gameWorld.Rooms.ContainsKey(new VectorInt(roomPosition.X + 1, roomPosition.Y, roomPosition.Z).ToString()))
                {
                    Doors["EAST"] = new RoomDoors(Program.engine.gameWorld.Rooms[new VectorInt(roomPosition.X + 1, roomPosition.Y, roomPosition.Z).ToString()].Doors["WEST"].IsDoor);
                }
                else
                {
                    willHaveDoor = Program.rnd.Next(100);
                    if (willHaveDoor > 50) Doors["EAST"] = new RoomDoors(true); else Doors["EAST"] = new RoomDoors(false);
                }
                #endregion
                #region South
                if (Program.engine.gameWorld.Rooms.ContainsKey(new VectorInt(roomPosition.X, roomPosition.Y + 1, roomPosition.Z).ToString()))
                {
                    Doors["SOUTH"] = new RoomDoors(Program.engine.gameWorld.Rooms[new VectorInt(roomPosition.X, roomPosition.Y + 1, roomPosition.Z).ToString()].Doors["NORTH"].IsDoor);
                }
                else
                {
                    willHaveDoor = Program.rnd.Next(100);
                    if (willHaveDoor > 50) Doors["SOUTH"] = new RoomDoors(true); else Doors["SOUTH"] = new RoomDoors(false);
                }
                #endregion
                #region West
                if (Program.engine.gameWorld.Rooms.ContainsKey(new VectorInt(roomPosition.X - 1, roomPosition.Y, roomPosition.Z).ToString()))
                {
                    Doors["WEST"] = new RoomDoors(Program.engine.gameWorld.Rooms[new VectorInt(roomPosition.X - 1, roomPosition.Y, roomPosition.Z).ToString()].Doors["EAST"].IsDoor);
                }
                else
                {
                    willHaveDoor = Program.rnd.Next(100);
                    if (willHaveDoor > 50) Doors["WEST"] = new RoomDoors(true); else Doors["WEST"] = new RoomDoors(false);
                }
                #endregion
                #region Up
                if (Program.engine.gameWorld.Rooms.ContainsKey(new VectorInt(roomPosition.X, roomPosition.Y, roomPosition.Z + 1).ToString()))
                {
                    Doors["UP"] = new RoomDoors(Program.engine.gameWorld.Rooms[new VectorInt(roomPosition.X, roomPosition.Y, roomPosition.Z + 1).ToString()].Doors["DOWN"].IsDoor);
                }
                else
                {
                    willHaveDoor = Program.rnd.Next(100);
                    if (willHaveDoor > 50) Doors["UP"] = new RoomDoors(true); else Doors["UP"] = new RoomDoors(false);
                }
                #endregion
                #region Down
                if (Program.engine.gameWorld.Rooms.ContainsKey(new VectorInt(roomPosition.X, roomPosition.Y, roomPosition.Z - 1).ToString()))
                {
                    Doors["DOWN"] = new RoomDoors(Program.engine.gameWorld.Rooms[new VectorInt(roomPosition.X, roomPosition.Y, roomPosition.Z - 1).ToString()].Doors["UP"].IsDoor);
                }
                else
                {
                    willHaveDoor = Program.rnd.Next(100);
                    if (willHaveDoor > 50) Doors["DOWN"] = new RoomDoors(true); else Doors["DOWN"] = new RoomDoors(false);
                }
                #endregion
            }
            #region Debug-Doors
            if(Debug)
            {
                foreach (KeyValuePair<string, RoomDoors> door in Doors)
                {
                    door.Value.IsDoor = true;
                }
            }
            #endregion
            #region ReturnDoor
            //Make sure returndoor exists
            Doors[EntryDirection] = new RoomDoors(true);
            #endregion
            #endregion

            #region Special Rooms
            if (roomPosition.X == 3 && roomPosition.Y == 1 && roomPosition.Z == 4) description.Append("There is a pie drawn on one of the walls.\r\n");
            if (roomPosition.X > 1 && roomPosition.Y == Math.Pow(roomPosition.X, 2) && roomPosition.Z == Math.Pow(roomPosition.X, 3)) description.Append("All the walls are covered in squares.\r\n");
            if (roomPosition.X > 1 && roomPosition.Y == roomPosition.X * roomPosition.X && roomPosition.Z == roomPosition.Y * roomPosition.Y) description.Append("There is a cube drawn on the floor.\r\n");
            #endregion

            #region Describe Doors
            description.Append("The room has doors to:");
            foreach (KeyValuePair<string, RoomDoors> door in Doors)
            {
                if (door.Value.IsDoor) description.Append(" " + door.Key[0] + door.Key.Substring(1).ToLower());
            }
            description.Append("\r\n");

            foreach(KeyValuePair<string, RoomDoors> door in Doors)
            {
                if (door.Value.IsDoor) doorCounter++;
            }
            if (doorCounter == 1) description.Append("This room is a dead-end.\r\n");

            #endregion
        }

        public void UnlockDoor(string Direction)
        {
            Doors[Direction].Unlock();
        }

        public void EnableAllDoors()
        {
            foreach(KeyValuePair<string, RoomDoors> door in Doors)
            {
                Program.engine.messages.Append(door.Key + " enabled.\r\n");
                door.Value.IsDoor = true;
            }
        }
    }
}

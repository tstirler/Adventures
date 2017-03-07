using System.Collections.Generic;
using DataTypes;

namespace Adventures
{
    public class World
    {
        public Dictionary<string, Room> Rooms;
        
        public void AddRoom(VectorInt Position, string EntryDirection, bool Debug)
        {
            Rooms.Add(Position.ToString(), new Room(Position, EntryDirection, false, Debug));
        }

        public void Update()
        {

        }

        public World(bool Debug)
        {
            //Always at least one door out of first room
            string direction;
            double startingDoor = Program.rnd.Next(100);
            if (startingDoor < 25) direction = "NORTH";
            else if (startingDoor < 50) direction = "SOUTH";
            else if (startingDoor < 75) direction = "EAST";
            else direction = "WEST";
            
            Rooms = new Dictionary<string, Room>();
            Rooms.Add(new VectorInt(0, 0, 0).ToString(), new Room(new VectorInt(0,0, 0), direction, true, Debug));
        }
    }
}

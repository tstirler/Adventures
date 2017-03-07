using DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adventures
{
    public class GameEngine
    {
        private enum commands
        {
            Move
        }

        public static Dictionary<string, string> gameStrings = new Dictionary<string, string>()
        {
            {"enterName", "Please enter your name: " },
            { "noDoor", "There is no door in that direction."},
            {"doorUnlocked", "The door has been unlocked." }
        };
        public StringBuilder messages;
        private Room currentRoom;

        public bool IsRunning;

        private Player player;
        public World gameWorld;
        
        public GameEngine(bool Debug)
        {
            gameWorld = new World(Debug);
            Console.WriteLine(gameStrings["enterName"]);
            player = new Player(Console.ReadLine(), Debug);
            currentRoom = gameWorld.Rooms[player.position.ToString()];
            IsRunning = true;
        }

        public void Update(bool Debug)
        {
            player.Update(Debug);
            gameWorld.Update();
            currentRoom = gameWorld.Rooms[player.position.ToString()];
        }

        public void GetUserInteraction(bool Debug)
        {
            messages = new StringBuilder();

            Console.SetCursorPosition(0, Console.BufferHeight - 1);
            Console.Write($"Please enter action [{ListCommands()}]: ");
            string interaction = Console.ReadLine();
            Console.SetCursorPosition(0, 0);
            string[] action = interaction.Split(' ');
            switch (action[0].ToUpper())
            {
                #region Move
                case "MOVE":
                    gameWorld.Rooms[player.position.ToString()] = currentRoom;
                    switch (action[1].ToUpper())
                    {
                       case "NORTH":
                            if (currentRoom.Doors["NORTH"].IsDoor)
                            {
                                if (gameWorld.Rooms.ContainsKey(new VectorInt(player.position.X, player.position.Y - 1, player.position.Z).ToString()))
                                {
                                    player.position = new VectorInt(player.position.X, player.position.Y - 1, player.position.Z);
                                }
                                else
                                {
                                    AddRoom(new VectorInt(player.position.X, player.position.Y - 1, player.position.Z), "SOUTH", Debug);
                                    player.position = new VectorInt(player.position.X, player.position.Y - 1, player.position.Z);
                                }
                                messages.Append("You move north.");
                                player.IncrementRoomsMoved();
                            }
                            else
                            {
                                messages.Append(gameStrings["noDoor"]);
                            }
                            break;
                        case "EAST":
                            if (currentRoom.Doors["EAST"].IsDoor)
                            {
                                if (gameWorld.Rooms.ContainsKey(new VectorInt(player.position.X + 1, player.position.Y, player.position.Z).ToString()))
                                {
                                    player.position = new VectorInt(player.position.X + 1, player.position.Y, player.position.Z);
                                }
                                else
                                {
                                    AddRoom(new VectorInt(player.position.X + 1, player.position.Y, player.position.Z), "WEST", Debug);
                                    player.position = new VectorInt(player.position.X + 1, player.position.Y, player.position.Z);
                                }
                                messages.Append("You move east.");
                                player.IncrementRoomsMoved();
                            }
                            else
                            {
                                messages.Append(gameStrings["noDoor"]);
                            }
                            break;
                        case "SOUTH":
                            if (currentRoom.Doors["SOUTH"].IsDoor)
                            {
                                if (gameWorld.Rooms.ContainsKey(new VectorInt(player.position.X, player.position.Y + 1, player.position.Z).ToString()))
                                {
                                    player.position = new VectorInt(player.position.X, player.position.Y + 1, player.position.Z);
                                }
                                else
                                {
                                    AddRoom(new VectorInt(player.position.X, player.position.Y + 1, player.position.Z), "NORTH", Debug);
                                    player.position = new VectorInt(player.position.X, player.position.Y + 1, player.position.Z);
                                }
                                messages.Append("You move south.");
                                player.IncrementRoomsMoved();
                            }
                            else
                            {
                                messages.Append(gameStrings["noDoor"]);
                            }
                            break;
                        case "WEST":
                            if (currentRoom.Doors["WEST"].IsDoor)
                            {
                                if (gameWorld.Rooms.ContainsKey(new VectorInt(player.position.X - 1, player.position.Y, player.position.Z).ToString()))
                                {
                                    player.position = new VectorInt(player.position.X - 1, player.position.Y, player.position.Z);
                                }
                                else
                                {
                                    AddRoom(new VectorInt(player.position.X - 1, player.position.Y, player.position.Z), "EAST", Debug);
                                    player.position = new VectorInt(player.position.X - 1, player.position.Y, player.position.Z);
                                }
                                messages.Append("You move west.");
                                player.IncrementRoomsMoved();
                            }
                            else
                            {
                                messages.Append(gameStrings["noDoor"]);
                            }
                            break;
                        case "UP":
                            if (currentRoom.Doors["UP"].IsDoor)
                            {
                                if (gameWorld.Rooms.ContainsKey(new VectorInt(player.position.X, player.position.Y, player.position.Z + 1).ToString()))
                                {
                                    player.position = new VectorInt(player.position.X, player.position.Y, player.position.Z + 1);
                                }
                                else
                                {
                                    AddRoom(new VectorInt(player.position.X, player.position.Y, player.position.Z + 1), "DOWN", Debug);
                                    player.position = new VectorInt(player.position.X, player.position.Y, player.position.Z + 1);
                                }
                                messages.Append("You climb up.");
                                player.IncrementRoomsMoved();
                            }
                            else
                            {
                                messages.Append(gameStrings["noDoor"]);
                            }
                            break;
                        case "DOWN":
                            if (currentRoom.Doors["DOWN"].IsDoor)
                            {
                                if (gameWorld.Rooms.ContainsKey(new VectorInt(player.position.X, player.position.Y, player.position.Z - 1).ToString()))
                                {
                                    player.position = new VectorInt(player.position.X, player.position.Y, player.position.Z - 1);
                                }
                                else
                                {
                                    AddRoom(new VectorInt(player.position.X, player.position.Y, player.position.Z - 1), "UP", Debug);
                                    player.position = new VectorInt(player.position.X, player.position.Y, player.position.Z - 1);
                                }
                                messages.Append("You drop down.");
                                player.IncrementRoomsMoved();
                            }
                            else
                            {
                                messages.Append(gameStrings["noDoor"]);
                            }
                            break;
                        default:
                            if (Debug)
                            {
                                string[] coords = action[1].Split(',');
                                AddRoom(new VectorInt(Int32.Parse(coords[0]), Int32.Parse(coords[1]), Int32.Parse(coords[2])), "UP", Debug);
                                player.position = new VectorInt(Int32.Parse(coords[0]), Int32.Parse(coords[1]), Int32.Parse(coords[2]));
                            }
                            break;
                    }
                    break;
                #endregion
                case "UNLOCK":
                    currentRoom.Doors[action[1].ToUpper()].Unlock();
                    break;

                    //Debug-commands
                case "TESTINGCHEATSENABLE":
                    Program.Debug = true;
                    messages.Append("Debug enabled.");
                    break;
                case "ENABLEALLDOORS":
                    messages.Append("Enabling doors.");
                    gameWorld.Rooms[player.position.ToString()].EnableAllDoors();
                    break;
                default:
                    break;
            }
        }

        public void AddRoom(VectorInt Position, string EntryDirection, bool Debug)
        {
            gameWorld.AddRoom(Position, EntryDirection, Debug);
        }

        private string ListCommands()
        {
            StringBuilder availableCommands = new StringBuilder();

            int enumCounter = 0;
            foreach (var command in Enum.GetValues(typeof(commands)))
            {
                availableCommands.Append(command);
                if (enumCounter < Enum.GetValues(typeof(commands)).Length - 1)
                {
                    availableCommands.Append(" ");
                }
                enumCounter++;
            }

            return availableCommands.ToString();
        }

        private void WriteHorizontalLine()
        {
            StringBuilder hrLine = new StringBuilder();
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                hrLine.Append("-");
            }
            Console.Write(hrLine);
        }

        public void Draw(bool Debug)
        {
            //PlayerInfo:
            Console.Write($"Name: {player.Name}");
            Console.SetCursorPosition(26 ,0);
            Console.Write($"Rooms moved through: {player.RoomsMovedThrough} Exp: {player.XP}");

            Console.SetCursorPosition(0, 2);
            WriteHorizontalLine();
            //Actions
            Console.SetCursorPosition(0, 3);
            Console.WriteLine(messages);

            //Descriptions:
            Console.SetCursorPosition(0, 10);
            Console.Write(gameWorld.Rooms[player.position.ToString()].Description);
            if(Debug)
            {
                Console.SetCursorPosition(80 - player.position.ToString().Length, 0);
                Console.Write(player.position);
            }

            //Horizonal line above commands:
            Console.SetCursorPosition(0, Console.BufferHeight - 2);
            WriteHorizontalLine();
        }
    }
}

namespace Adventures
{
    public class RoomDoors
    {
        public bool IsDoor;
        public bool IsLocked { get; private set; }

        public RoomDoors(bool IsDoor)
        {
            this.IsDoor = IsDoor;
            IsLocked = true;
        }

        public void Unlock()
        {
            IsLocked = false;
            Program.engine.messages.Append(GameEngine.gameStrings["doorUnlocked"]);
        }
    }
}
using DataTypes;
using System.Text;

namespace Adventures
{
    internal class Player
    {
        private bool Debug;
        public VectorInt position;
        public StringBuilder Description { get; private set; }
        public string Name { get; private set; }
        public int RoomsMovedThrough { get; private set; }
        public int XP
        {
            get
            {
                return (int)xp;
            }
        }
        private double xp;

        public Player(string PlayerName, bool Debug)
        {
            Description = new StringBuilder();
            this.Name = PlayerName;
            this.Debug = Debug;
            position = new VectorInt(0, 0, 0);
            RoomsMovedThrough = 0;
            xp = 0;
        }

        public void IncrementRoomsMoved()
        {
            RoomsMovedThrough++;
        }

        public void AwardXP(double Amount)
        {
            xp += Amount;
        }

        public void Update(bool Debug)
        {
            Description = new StringBuilder();
        }
    }
}

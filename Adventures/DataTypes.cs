namespace DataTypes
{
    public class VectorInt
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return $"{X.ToString()},{Y.ToString()},{Z.ToString()}";
        }

        public VectorInt(int X, int Y, int Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        } 
    }
}

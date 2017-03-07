using System;

namespace Adventures
{
    class Program
    {
        public static bool Debug = false;

        public static Random rnd = new Random();
        public static GameEngine engine;

        static void Main(string[] args)
        {
            Console.WindowHeight = 25;
            Console.BufferHeight = 25;
            engine = new GameEngine(Debug);
            
            do
            {
                Console.Clear();
                engine.Update(Debug);
                engine.Draw(Debug);
                engine.GetUserInteraction(Debug);
            } while (engine.IsRunning);
        }
    }
}

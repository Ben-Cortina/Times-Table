using System;

namespace Project3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MathGame game = new MathGame())
            {
                game.Run();
            }
        }
    }
}


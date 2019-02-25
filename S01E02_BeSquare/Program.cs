using System;

namespace S01E02_BeSquare
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new SquareFactory();

            factory.SquareSize = 3;
            factory.AddMaterial(30, 30);
            factory.AddMaterial(3, 30);

            factory.SquareSize = 10;
            factory.AddMaterial(100, 30);

            var totalSquares = 0;

            foreach (var square in factory.GetSquares())
            {
                totalSquares++;
                Console.WriteLine($"Created square with size {square.Width}x{square.Height}");
            }

            Console.WriteLine($"Created a total of {totalSquares} squares.");
            }
    }
}

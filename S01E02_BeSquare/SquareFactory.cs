using System;
using System.Collections.Generic;

namespace S01E02_BeSquare
{
    public sealed class SquareFactory : ISquareFactory
    {
        public int SquareSize { get; set; }

        public void AddMaterial(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Width of material must be positive.");

            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), "Height of material must be positive.");

            // Added a new condition here, so it's not possible to add material, that is smaller than the size of wanted squares.
            if (height < SquareSize || width < SquareSize)
                throw new ArgumentOutOfRangeException(nameof(SquareSize), "Material must have at least the same width and height as the square size.");

            if (SquareSize <= 0)
                throw new InvalidOperationException("The square factory will not work unless the square size is a positive integer.");

            var horizontalPieces = (int)Math.Ceiling(1d * width / SquareSize);
            var verticalPieces = (int)Math.Ceiling(1d * height / SquareSize);

            for (var xIndex = 0; xIndex < horizontalPieces; xIndex++)
                for (var yIndex = 0; yIndex < verticalPieces; yIndex++)
                {
                    var xStart = xIndex * SquareSize;
                    var yStart = yIndex * SquareSize;

                    // This square starts at (xStart, ySTart) coordinates in the material and is up to SquareSize big.
                    // If we are at the edges of the material, it might be smaller (if we have not enough material).
                    // Here we calculate the correct size of the material that will make this square.
                    var remainingWidth = width - xStart;
                    var remainingHeight = height - yStart;

                    var w = Math.Min(SquareSize, remainingWidth);
                    var h = Math.Min(SquareSize, remainingHeight);

                    // Added a little check here, so that the leftovers from the edges of material are not piled up in the square enqueue.
                    if (SquareSize == w && SquareSize == h)
                    {
                        _ready.Enqueue(new Rectangle(w, h));
                    }
                }
        }

        public int SquaresReadyForDelivery => _ready.Count;

        public IEnumerable<Rectangle> GetSquares()
        {
            while (_ready.Count != 0)
            {
                var square = _ready.Dequeue();
                yield return square;
            }
        }

        // All the squares that are waiting to be handed over to the caller.
        private readonly Queue<Rectangle> _ready = new Queue<Rectangle>();
    }
}

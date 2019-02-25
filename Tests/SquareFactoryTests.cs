using Microsoft.VisualStudio.TestTools.UnitTesting;
using S01E02_BeSquare;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public sealed class SquareFactoryTests
    {
        public SquareFactoryTests()
        {
            _factory = new SquareFactory();
        }

        private readonly ISquareFactory _factory;

        [TestMethod]
        public void GetSquares_WhenNoSquaresReady_ReturnsNoSquares()
        {
            var squares = _factory.GetSquares().ToList();

            Assert.AreEqual(0, squares.Count);
        }

        [TestMethod]
        public void AddMaterial_WithNegativeMaterialSize_Fails()
        {
            _factory.SquareSize = 1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _factory.AddMaterial(-1, -1));
        }

        [TestMethod]
        public void AddMaterial_WithZeroSquareSize_Fails()
        {
            _factory.SquareSize = 0;

            Assert.ThrowsException<InvalidOperationException>(() => _factory.AddMaterial(1, 1));
        }

        [DataTestMethod]
        // Cut a 100x100 material into squares of various size.
        [DataRow(100, 100, 10)]
        [DataRow(100, 100, 25)]
        [DataRow(100, 100, 50)]
        [DataRow(100, 100, 100)]
        // Try with a tall/wide material.
        [DataRow(10, 1, 1)]
        [DataRow(1, 10, 1)]
        // Try with material of weird size.
        [DataRow(123, 123, 10)]
        // Try with too small material to make any squares.
        [DataRow(10, 1, 10)]
        // And huge material.
        [DataRow(10000, 10000, 100)]
        public void AddMaterial_WithVariousSizes_CreatesSquaresOfExpectedSize(int materialWidth, int materialHeight, int squareSize)
        {
            _factory.SquareSize = squareSize;

            _factory.AddMaterial(materialWidth, materialHeight);

            foreach (var square in _factory.GetSquares())
            {
                // Obviously, a square factory is going to have to return squares.
                Assert.IsTrue(square.IsSquare);
                
                // The contract says that every square will be of the same size as we had
                // in .SquareSize when we called .AddMaterial(), so verify this is always true.
                Assert.AreEqual(squareSize, square.Width);
                Assert.AreEqual(squareSize, square.Height);
            }
        }
    }
}

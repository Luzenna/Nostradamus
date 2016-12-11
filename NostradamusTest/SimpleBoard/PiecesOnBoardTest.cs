using Microsoft.VisualStudio.TestTools.UnitTesting;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusTest.SimpleBoard
{
    [TestClass]
    public class PiecesOnBoardTest
    {
        private Board _board;

        [TestInitialize]
        public void Init()
        {
            _board = new Board();    
        }

        [TestMethod]
        public void AddOnePieceToA1()
        {
            Knight knight = SetKnightOnA1();
            Assert.AreEqual(1, knight.Square.File);
            Assert.AreEqual(0, knight.Square.Rank);
        }

        [TestMethod]
        public void RemovePiece()
        {
            var knight = SetKnightOnA1();
            _board.RemovePiece(new BareSquare(1,0),knight );
            Assert.IsNull(knight.Square);
        }

        [TestMethod]
        public void AddAndRemovePiece()
        {
            var knight = SetKnightOnA1();
            _board.RemovePiece(new BareSquare(1, 0), knight);
            _board.SetPiece(new BareSquare(1,0), knight);
            Assert.AreEqual(1, knight.Square.File);
            Assert.AreEqual(0, knight.Square.Rank);
        }

        private  Knight SetKnightOnA1()
        {
            var knight = new Knight(Color.White, _board);
            _board.SetPiece(new BareSquare(1, 0), knight);
            return knight;
        }
    }
}

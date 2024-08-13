using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Moves;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusEngine.Pieces
{
    public class Rook : DirectionalMovingPiece
    {

        public Rook(Color  color, IBoard board)
            : base(color, board)
        {

        }

        public override string FullName => "Rook";

        public override string ShortName => "R";

        public override IEnumerable<ISquare> FindCoveredSquares()
        {
            var squares = new List<ISquare>();

            // Raycast +1 +1
            squares.AddRange(FindCoveredSquaresInDirection(1, 0));
            // Raycast +1 -1
            squares.AddRange(FindCoveredSquaresInDirection(0, 1));
            // Raycast -1 -1
            squares.AddRange(FindCoveredSquaresInDirection(-1, 0));
            // Raycast -1 +1
            squares.AddRange(FindCoveredSquaresInDirection(0, -1));
            return squares;
        }

        public override IEnumerable<NormalMove> CalculateAllMoves(int ply)
        {
            var allMoves = new List<NormalMove>();

            // Raycast +1 +1
            allMoves.AddRange(CalculateMoveInDirection(1, 0, ply));
            // Raycast +1 -1
            allMoves.AddRange(CalculateMoveInDirection(0, 1, ply));
            // Raycast -1 -1
            allMoves.AddRange(CalculateMoveInDirection(-1, 0, ply));
            // Raycast -1 +1
            allMoves.AddRange(CalculateMoveInDirection(0, -1, ply));
            return allMoves;
        }
    }
}

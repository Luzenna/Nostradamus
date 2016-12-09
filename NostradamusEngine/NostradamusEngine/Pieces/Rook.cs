using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class Rook : DirectionalMovingPiece
    {

        public Rook(Color  color, Square square, ChessEngine game)
            : base(color, square, game)
        {

        }

        public override String FullName
        {
            get
            {
                return "Rook";
            }
        }

        public override String ShortName
        {
            get
            {
                return "R";
            }
        }

        public override IEnumerable<Square> FindCoveredSquares()
        {
            var squares = new List<Square>();

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

        public override IEnumerable<Rules.Move> CalculateAllMoves(int ply)
        {
            var allMoves = new List<Move>();

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

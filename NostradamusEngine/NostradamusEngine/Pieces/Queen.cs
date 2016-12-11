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
    public class Queen : DirectionalMovingPiece
    {

        public Queen(Color color, IBoard board)
            : base(color, board)
        {

        }

        
        public override string FullName => "Queen";

        public override String ShortName => "Q";

        public override IEnumerable<ISquare> FindCoveredSquares()
        {
            var allMoves = new List<ISquare>();

            // Raycast +1 +1
            allMoves.AddRange(FindCoveredSquaresInDirection(1, 0));
            // Raycast +1 -1
            allMoves.AddRange(FindCoveredSquaresInDirection(0, 1));
            // Raycast -1 -1
            allMoves.AddRange(FindCoveredSquaresInDirection(-1, 0));
            // Raycast -1 +1
            allMoves.AddRange(FindCoveredSquaresInDirection(0, -1));
            // Raycast +1 +1
            allMoves.AddRange(FindCoveredSquaresInDirection(1, 1));
            // Raycast +1 -1
            allMoves.AddRange(FindCoveredSquaresInDirection(1, -1));
            // Raycast -1 -1
            allMoves.AddRange(FindCoveredSquaresInDirection(-1, -1));
            // Raycast -1 +1
            allMoves.AddRange(FindCoveredSquaresInDirection(-1, 1));
            return allMoves;
        }

        public override IEnumerable<NormalMove> CalculateAllMoves(int ply)
        {
            List<NormalMove> allMoves = new List<NormalMove>();

            // Raycast +1 +1
            allMoves.AddRange(CalculateMoveInDirection(1, 0, ply));
            // Raycast +1 -1
            allMoves.AddRange(CalculateMoveInDirection(0, 1, ply));
            // Raycast -1 -1
            allMoves.AddRange(CalculateMoveInDirection(-1, 0, ply));
            // Raycast -1 +1
            allMoves.AddRange(CalculateMoveInDirection(0, -1, ply));
            // Raycast +1 +1
            allMoves.AddRange(CalculateMoveInDirection(1, 1, ply));
            // Raycast +1 -1
            allMoves.AddRange(CalculateMoveInDirection(1, -1, ply));
            // Raycast -1 -1
            allMoves.AddRange(CalculateMoveInDirection(-1, -1, ply));
            // Raycast -1 +1
            allMoves.AddRange(CalculateMoveInDirection(-1, 1, ply));
            return allMoves;
        }

    }
}

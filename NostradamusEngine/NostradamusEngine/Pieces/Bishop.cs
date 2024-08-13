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
    public class Bishop : DirectionalMovingPiece
    {

        public Bishop(Color color, IBoard board)
            : base(color, board)
        {

        }

        public override String FullName
        {
            get
            {
                return "Bishop";
            }
        }

        public override string ShortName => "B";

        public override IEnumerable<ISquare> FindCoveredSquares()
        {
            var allMoves = new List<ISquare>();

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
            allMoves.AddRange(CalculateMoveInDirection(1, 1,ply));
            // Raycast +1 -1
            allMoves.AddRange(CalculateMoveInDirection(1, -1,ply));
            // Raycast -1 -1
            allMoves.AddRange(CalculateMoveInDirection(-1, -1,ply));
            // Raycast -1 +1
            allMoves.AddRange(CalculateMoveInDirection(-1, 1,ply));
            return allMoves;
        }
    }
}

using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class Bishop : DirectionalMovingPiece
    {

        public Bishop(Color color, Square square, ChessEngine game)
            : base(color, square, game)
        {

        }

        public override String FullName
        {
            get
            {
                return "Bishop";
            }
        }

        public override String ShortName
        {
            get
            {
                return "B";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            List<Rules.Move> allMoves = new List<Move>();

            // Raycast +1 +1
            allMoves.AddRange(CalculateMoveInDirection(1, 1));
            // Raycast +1 -1
            allMoves.AddRange(CalculateMoveInDirection(1, -1));
            // Raycast -1 -1
            allMoves.AddRange(CalculateMoveInDirection(-1, -1));
            // Raycast -1 +1
            allMoves.AddRange(CalculateMoveInDirection(-1, 1));
            return allMoves;
        }
    }
}

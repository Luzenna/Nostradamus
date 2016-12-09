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

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            List<Rules.Move> allMoves = new List<Move>();

            // Raycast +1 +1
            allMoves.AddRange(CalculateMoveInDirection(1, 0));
            // Raycast +1 -1
            allMoves.AddRange(CalculateMoveInDirection(0, 1));
            // Raycast -1 -1
            allMoves.AddRange(CalculateMoveInDirection(-1, 0));
            // Raycast -1 +1
            allMoves.AddRange(CalculateMoveInDirection(0, -1));
            return allMoves;
        }



        public override bool IsLegalMove(Rules.Move move)
        {
            foreach (Move m in CalculateAllMoves())
            {
                if (move == m)
                    return true;
            }
            return false;
        }


    }
}

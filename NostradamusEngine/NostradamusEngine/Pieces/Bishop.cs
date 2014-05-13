using NostradamusEngine.Board;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Bishop : Piece
    {

        public Bishop(Boolean isWhite, Square square, ChessEngine game)
            : base(isWhite, square, game)
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

        private IEnumerable<Rules.Move> CalculateMoveInDirection(Int32 fileAddition, Int32 rankAddition)
        {
            var stoppedSearching = false;
            Int32 fileAdder = fileAddition, rankAdder = rankAddition;
            while (!stoppedSearching)
            {
                var squareToCheck = Game.Board[Square.File + fileAdder, Square.Rank + rankAdder];
                // Blocked
                if (squareToCheck == null)
                {
                    stoppedSearching = true;
                }
                else if (squareToCheck.Piece == null)
                {
                    yield return new Move(this, Square, squareToCheck, null);
                }
                else if (squareToCheck.Piece.IsWhite == IsWhite)
                    stoppedSearching = true;
                else if (squareToCheck.Piece.IsWhite != IsWhite)
                {
                    stoppedSearching = true;
                    yield return new Move(this, Square, squareToCheck, squareToCheck.Piece);
                }
                fileAdder += fileAddition;
                rankAdder += rankAddition;
            }
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

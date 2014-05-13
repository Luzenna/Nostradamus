using NostradamusEngine.Board;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class King : Piece
    {
        public King(Boolean isWhite, Square square, ChessEngine game)
            : base(isWhite, square, game)
        {

        }


        public override String FullName
        {
            get
            {
                return "King";
            }
        }

        public override String ShortName
        {
            get
            {
                return "K";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            List<Move> allMoves = new List<Move>();
            allMoves.AddRange(CheckSquare(-1, -1));
            allMoves.AddRange(CheckSquare(0, -1));
            allMoves.AddRange(CheckSquare(1, -1));
            allMoves.AddRange(CheckSquare(-1, 0));
            allMoves.AddRange( CheckSquare(1, 0));
            allMoves.AddRange(CheckSquare(-1, 1));
            allMoves.AddRange(CheckSquare(0, 1));
            allMoves.AddRange(CheckSquare(1, 1));
            return allMoves;
        }

        private IEnumerable<Rules.Move> CheckSquare(Int32 fileAdder, Int32 rankAdder)
        {
            var squareToCheck = Game.Board[Square.File + fileAdder, Square.Rank + rankAdder];
            if (squareToCheck == null)
                yield break;    
            if (squareToCheck.Piece == null)
            {
                yield return new Move(this, Square, squareToCheck, null);
            }
            else if (squareToCheck.Piece.IsWhite != IsWhite)
            {
                yield return new Move(this, Square, squareToCheck, squareToCheck.Piece);
            }
        }
    }
}

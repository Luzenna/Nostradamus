using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class Knight : Piece
    {

        public Knight(Color color, Square square, ChessEngine game)
            : base(color, square, game)
        {

        }


        public override String FullName
        {
            get
            {
                return "Knight";
            }
        }

        public override String ShortName
        {
            get
            {
                return "N";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            List<Move> allMoves = new List<Move>();
            allMoves.AddRange(CheckSquare(2, 1));
            allMoves.AddRange(CheckSquare(-2, 1));
            allMoves.AddRange(CheckSquare(2, -1));
            allMoves.AddRange(CheckSquare(-2, -1));
            allMoves.AddRange(CheckSquare(1, 2));
            allMoves.AddRange(CheckSquare(1, -2));
            allMoves.AddRange(CheckSquare(-1, 2));
            allMoves.AddRange(CheckSquare(-1, -2));
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
            else if (squareToCheck.Piece.Color != this.Color)
            {
                yield return new Move(this, Square, squareToCheck, squareToCheck.Piece);
            }
        }

    }
}

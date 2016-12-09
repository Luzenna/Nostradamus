using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public abstract class DirectionalMovingPiece : Piece
    {
        protected DirectionalMovingPiece(Color color, Square square, ChessEngine game )
            :base(color,square,game)
        { }
        

        protected IEnumerable<Rules.Move> CalculateMoveInDirection(Int32 fileAddition, Int32 rankAddition)
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
                else if (squareToCheck.Piece.Color == Color)
                    stoppedSearching = true;
                else if (squareToCheck.Piece.Color != Color)
                {
                    stoppedSearching = true;
                    yield return new Move(this, Square, squareToCheck, squareToCheck.Piece);
                }
                fileAdder += fileAddition;
                rankAdder += rankAddition;
            }
        }
    }
}

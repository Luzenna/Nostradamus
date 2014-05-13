using NostradamusEngine.Board;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public abstract class DirectionalMovingPiece : Piece
    {
        public DirectionalMovingPiece(Boolean isWhite, Square square, ChessEngine game )
            :base(isWhite,square,game)
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
    }
}

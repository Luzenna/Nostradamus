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

        protected IEnumerable<Square> FindCoveredSquaresInDirection(int fileAddition, int rankAddition)
        {
            var stoppedSearching = false;
            int fileAdder = fileAddition, rankAdder = rankAddition;
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
                    yield return squareToCheck;
                }
                else if (squareToCheck.Piece != null)
                    stoppedSearching = true;
                fileAdder += fileAddition;
                rankAdder += rankAddition;
            }
        }

        protected IEnumerable<Rules.Move> CalculateMoveInDirection(Int32 fileAddition, Int32 rankAddition, int ply)
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
                    yield return new Move(this, Square, squareToCheck, null,ply);
                }
                else if (squareToCheck.Piece.Color == Color)
                    stoppedSearching = true;
                else if (squareToCheck.Piece.Color != Color)
                {
                    stoppedSearching = true;
                    yield return new Move(this, Square, squareToCheck, squareToCheck.Piece,ply);
                }
                fileAdder += fileAddition;
                rankAdder += rankAddition;
            }
        }
    }
}

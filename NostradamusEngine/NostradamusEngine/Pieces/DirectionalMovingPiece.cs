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
    public abstract class DirectionalMovingPiece : Piece
    {
        private static readonly log4net.ILog Log =
log4net.LogManager.GetLogger(typeof(DirectionalMovingPiece));

        protected DirectionalMovingPiece(Color color, IBoard board )
            :base(color,board)
        { }

        protected IEnumerable<ISquare> FindCoveredSquaresInDirection(int fileAddition, int rankAddition)
        {
            var stoppedSearching = false;
            int fileAdder = fileAddition, rankAdder = rankAddition;
            while (!stoppedSearching)
            {
                var squareToCheck = new BareSquare(Square.File + fileAdder, Square.Rank + rankAdder);
                var squareStatus = Board.GetSquareStatus(squareToCheck);
                switch (squareStatus)
                {
                    case SquareStatus.Illegal:
                        stoppedSearching = true;
                        break;
                    case SquareStatus.Empty:
                        yield return squareToCheck;
                        break;
                    case SquareStatus.Occupied:
                        yield return squareToCheck;
                        stoppedSearching = true;
                        break;
                }
                fileAdder += fileAddition;
                rankAdder += rankAddition;
            }
        }

        protected IEnumerable<NormalMove> CalculateMoveInDirection(Int32 fileAddition, Int32 rankAddition, int ply)
        {
            var stoppedSearching = false;
            int fileAdder = fileAddition, rankAdder = rankAddition;
            while (!stoppedSearching)
            {
                var squareToCheck = new BareSquare(Square.File + fileAdder, Square.Rank + rankAdder);
                var squareStatus = Board.GetSquareStatus(squareToCheck);
                switch (squareStatus)
                {
                    case SquareStatus.Illegal:
                        stoppedSearching = true;
                        break;
                    case SquareStatus.Empty:
                        yield return new NormalMove(this,Square,squareToCheck,null,ply);
                        break;
                    case SquareStatus.Occupied:
                        var piece = Board.GetPieceOn(squareToCheck);
                        if (piece.Color!=Color)
                            yield return new NormalMove(this,Square,squareToCheck,piece,ply);
                        stoppedSearching = true;
                        break;
                }
                fileAdder += fileAddition;
                rankAdder += rankAddition;
            }
        }
    }
}

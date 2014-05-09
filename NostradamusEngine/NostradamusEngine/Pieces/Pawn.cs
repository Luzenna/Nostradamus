using NostradamusEngine.Board;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Pawn : Piece
    {

        public Pawn(Boolean isWhite, Square square, NostradamusEngine game)
            : base(isWhite, square, game)
        {

        }


        public override String FullName
        {
            get
            {
                return "Pawn";
            }
        }

        public override String ShortName
        {
            get
            {
                return "P";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            var doubleMoveSquare = Game.Board[Square.File,Square.Rank+(MoveForward*2)];
            var ordinaryMoveSquare = Game.Board[Square.File, Square.Rank + MoveForward];
            var captureqs = Game.Board[Square.File+1, Square.Rank + MoveForward];
            var captureks = Game.Board[Square.File-1, Square.Rank + MoveForward];

            if (!HasMoved && doubleMoveSquare.Piece == null)
                yield return new Move(this, Square, doubleMoveSquare,null);
            if (ordinaryMoveSquare.Piece == null)
                yield return new Move(this, Square, ordinaryMoveSquare,null);
            if (captureqs.Piece != null && captureqs.Piece.IsWhite!=this.IsWhite)
                yield return new Move(this, Square, captureqs, captureqs.Piece);
            if (captureks.Piece != null && captureks.Piece.IsWhite != this.IsWhite)
                yield return new Move(this, Square, captureks, captureks.Piece);
        }

        private Boolean HasMoved
        {
            get
            {
                return !((IsWhite && this.Square.Rank == 1) || (!IsWhite && this.Square.Rank == 7));
            }
        }

        private Int32 MoveForward
        {
            get
            {
                return IsWhite ? 1 : -1;
            }
        }

    }
}

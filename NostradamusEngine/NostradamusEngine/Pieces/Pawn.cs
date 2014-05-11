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

        private Boolean isPromoted;

        public Pawn(Boolean isWhite, Square square, ChessEngine game)
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

            if (!HasMoved && doubleMoveSquare!=null && doubleMoveSquare.Piece == null)
                yield return new Move(this, Square, doubleMoveSquare,null);
            if (ordinaryMoveSquare!=null && ordinaryMoveSquare.Piece == null)
                yield return new Move(this, Square, ordinaryMoveSquare,null);
            if (captureqs!=null && captureqs.Piece != null && captureqs.Piece.IsWhite!=this.IsWhite)
                yield return new Move(this, Square, captureqs, captureqs.Piece);
            if (captureks!=null && captureks.Piece != null && captureks.Piece.IsWhite != this.IsWhite)
                yield return new Move(this, Square, captureks, captureks.Piece);
        }

        private Boolean HasMoved
        {
            get
            {
                return !((IsWhite && this.Square.Rank == 1) || (!IsWhite && this.Square.Rank == 6));
            }
        }

        private Int32 MoveForward
        {
            get
            {
                return IsWhite ? 1 : -1;
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

        public Boolean IsPromoted
        {
            get
            {
                return (Square.Rank==7 && IsWhite) || (Square.Rank==0 && !IsWhite);
            }
        }

    }
}

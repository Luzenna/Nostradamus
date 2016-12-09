using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class Pawn : Piece
    {

        private Boolean isPromoted;

        public Pawn(Color color, Square square, ChessEngine game)
            : base(color, square, game)
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

            if (Moves.Count==0 && doubleMoveSquare!=null && doubleMoveSquare.Piece == null)
                yield return new Move(this, Square, doubleMoveSquare,null);
            if (ordinaryMoveSquare!=null && ordinaryMoveSquare.Piece == null)
                yield return new Move(this, Square, ordinaryMoveSquare,null);
            if (captureqs!=null && captureqs.Piece != null && captureqs.Piece.Color!=this.Color)
                yield return new Move(this, Square, captureqs, captureqs.Piece);
            if (captureks!=null && captureks.Piece != null && captureks.Piece.Color != this.Color)
                yield return new Move(this, Square, captureks, captureks.Piece);
        }


        private Int32 MoveForward => Color==Color.White ? 1 : -1;

        public override bool IsLegalMove(Rules.Move move)
        {
            foreach (Move m in CalculateAllMoves())
            {
                if (move == m)
                    return true;
            }
            return false;
        }

        public Boolean IsPromoted => (Square.Rank==7 && Color==Color.White) || (Square.Rank==0 && Color==Color.Black);
    }
}

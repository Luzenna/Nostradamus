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
        private int _startRank;

        public Pawn(Color color, Square square, ChessEngine game)
            : base(color, square, game)
        {
            _startRank = color==Color.White?1:6;
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

        public override IEnumerable<Square> FindCoveredSquares()
        {
            var square = Game.Board[Square.File + 1, Square.Rank + MoveForward];
            if (square != null && square.Piece == null) yield return square;
            square = Game.Board[Square.File - 1, Square.Rank + MoveForward];
            if (square != null && square.Piece == null) yield return square;
        }

        //What a mess
        public override IEnumerable<Rules.Move> CalculateAllMoves(int ply)
        {
            var doubleMoveSquare = Game.Board[Square.File,Square.Rank+(MoveForward*2)];
            var ordinaryMoveSquare = Game.Board[Square.File, Square.Rank + MoveForward];
            var captureqs = Game.Board[Square.File+1, Square.Rank + MoveForward];
            var captureks = Game.Board[Square.File-1, Square.Rank + MoveForward];

            if (Square.Rank == _startRank + (MoveForward*3))
            {
                var pawn = Game.Board[Square.File + 1, Square.Rank].Piece as Pawn;
                if (pawn!=null && pawn.Moves.Count>0 && pawn.Moves?.Last()?.Ply==ply-1)
                    yield return new EnPassantMove(this,Square, Game.Board[Square.File + 1, Square.Rank+MoveForward],pawn,ply);
                pawn = Game.Board[Square.File - 1, Square.Rank].Piece as Pawn;
                if (pawn != null && pawn.Moves.Count>0 && pawn.Moves?.Last()?.Ply == ply - 1)
                    yield return new EnPassantMove(this, Square, Game.Board[Square.File -1 , Square.Rank + MoveForward], pawn, ply);
            }

            if (Moves.Count==0 && doubleMoveSquare!=null && doubleMoveSquare.Piece == null)
                yield return new Move(this, Square, doubleMoveSquare,null,ply);
            if (ordinaryMoveSquare!=null && ordinaryMoveSquare.Piece == null)
                yield return new Move(this, Square, ordinaryMoveSquare,null, ply);
            if (captureqs!=null && captureqs.Piece != null && captureqs.Piece.Color!=this.Color)
                yield return new Move(this, Square, captureqs, captureqs.Piece, ply);
            if (captureks!=null && captureks.Piece != null && captureks.Piece.Color != this.Color)
                yield return new Move(this, Square, captureks, captureks.Piece, ply);
        }


        private Int32 MoveForward => Color==Color.White ? 1 : -1;

        public Boolean IsPromoted => (Square.Rank==7 && Color==Color.White) || (Square.Rank==0 && Color==Color.Black);
    }
}

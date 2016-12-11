using System.Collections.Generic;
using System.Linq;
using NostradamusEngine.Moves;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class Pawn : Piece
    {
        private readonly int _startRank;

        public Pawn(Color color, IBoard board)
            : base(color, board)
        {
            _startRank = color==Color.White?1:6;
        }

        public override void Move(NormalMove m)
        {
            var enpassant = m as EnPassantMove;
            if (enpassant != null)
            {
                Moves.Add(m);
                Board.SetPiece(m.To,m.Piece);
                Board.RemovePiece(m.From,m.Piece);
                Board.RemovePiece(m.Capture.Square,m.Capture);
            }
            else
                base.Move(m);
        }

        public override void UndoMove(NormalMove m)
        {
            var enpassant = m as EnPassantMove;
            if (enpassant != null)
            {
                Moves.Remove(m);
                Board.RemovePiece(m.To,m.Piece);
                Board.SetPiece(m.From,m.Piece);
                Board.SetPiece(m.Capture.Square,m.Capture);
            }
            else
                base.UndoMove(m);
        }

        public override string FullName => "Pawn";

        public override string ShortName => "P";

        public override IEnumerable<ISquare> FindCoveredSquares()
        {
            var square1 = CheckIfSquareIsCovered(Square.File + 1, Square.Rank + MoveForward);
            if (square1 != null) yield return square1;
            var square2 = CheckIfSquareIsCovered(Square.File - 1, Square.Rank + MoveForward);
            if (square2 != null) yield return square2;
        }

        private ISquare CheckIfSquareIsCovered(int file, int rank)
        {
            var square = new BareSquare(file, rank);
            var squareStatus = Board.GetSquareStatus(square);
            if (squareStatus == SquareStatus.Empty)
                return square;
            else
                return null;
        }

        //What a mess
        public override IEnumerable<NormalMove> CalculateAllMoves(int ply)
        {
            var firstSquare = new BareSquare(Square.File,Square.Rank+MoveForward);
            var doubleSquare = new BareSquare(Square.File,Square.Rank+(MoveForward*2));
            var captureSquare1 = new BareSquare(Square.File - 1, Square.Rank + MoveForward);
            var captureSquare2 = new BareSquare(Square.File + 1, Square.Rank + MoveForward);
            var enpassantPieceSquare1 = new BareSquare(Square.File - 1,Square.Rank);
            var enpassantPieceSquare2 = new BareSquare(Square.File + 1, Square.Rank);
            if (Board.GetSquareStatus(firstSquare) == SquareStatus.Empty)
            {
                yield return new NormalMove(this,Square,firstSquare,null,ply);
                if (Board.GetSquareStatus(doubleSquare)==SquareStatus.Empty && Square.Rank==_startRank)
                {
                    yield return new PawnDoubleMove(this,Square,doubleSquare,ply);
                }
            }
            var captureMove1 = GetCaptureMove(captureSquare1, ply);
            if (captureMove1 != null)
                yield return captureMove1;
            var captureMove2 = GetCaptureMove(captureSquare2, ply);
            if (captureMove2 != null)
                yield return captureMove2;

            var enPassantMove1 = GetEnPassantMove(enpassantPieceSquare1, captureSquare1, ply);
            if (enPassantMove1 != null)
                yield return enPassantMove1;

            var enPassantMove2 = GetEnPassantMove(enpassantPieceSquare2, captureSquare2, ply);
            if (enPassantMove2 != null)
                yield return enPassantMove2;

        }

        private NormalMove GetCaptureMove(ISquare toCheck,int ply)
        {
            if (Board.GetSquareStatus(toCheck) == SquareStatus.Occupied)
            {
                var piece = Board.GetPieceOn(toCheck);
                if (piece.Color != Color)
                    return new NormalMove(this, Square, toCheck, piece, ply);
            }
            return null;
        }

        private NormalMove GetEnPassantMove(ISquare enPassantSquare, ISquare destinationSquare, int ply)
        {
            if (Square.Rank != _startRank + (MoveForward*3)) return null;
            if (Board.GetSquareStatus(enPassantSquare) != SquareStatus.Occupied) return null;
            var piece = Board.GetPieceOn(enPassantSquare) as Pawn;
            if (piece == null || piece.Moves.Count <= 0) return null;
            var lastMove = piece.Moves.Last();
            if (lastMove is PawnDoubleMove && lastMove.Ply==ply-1)
                return new EnPassantMove(this,Square,destinationSquare,piece,ply);
            return null;
        }


        private int MoveForward => Color==Color.White ? 1 : -1;

        public bool IsPromoted => (Square.Rank==7 && Color==Color.White) || (Square.Rank==0 && Color==Color.Black);
    }
}

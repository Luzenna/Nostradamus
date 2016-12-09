using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Rules
{
    public class EnPassantMove : Move
    {
        public EnPassantMove(Piece piece, Square from, Square to, Piece capture, int ply) : base(piece, @from, to, capture,ply)
        {
        }

        public override void Do()
        {
            Piece.Move(this);
            To.Piece = Piece;
            To.Piece.Square = To;
            From.Piece = null;
            Capture.Square.Piece = null;
        }

        public override void Undo()
        {
            Piece.UndoMove(this);
            From.Piece = Piece;
            From.Piece.Square = To;
            To.Piece = Capture;
            Capture.Square.Piece = Capture;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Rules
{
    public class CastlingMove : Move
    {
        private readonly Piece _rook;
        private readonly Square _rookfrom;
        private readonly Square _rookto;

        public CastlingMove(Piece king, Square from, Square to, Piece rook,Square rookfrom, Square rookto) : base(king, from, to, null)
        {
            _rook = rook;
            _rookfrom = rookfrom;
            _rookto = rookto;
        }

        public override void Do()
        {
            DoForOne(Piece,From,To);
            DoForOne(_rook,_rookfrom,_rookto);
        }

        public override void Undo()
        {
            UndoForOne(Piece,From,To);
            UndoForOne(_rook,_rookfrom,_rookto);
        }

        public void DoForOne(Piece piece, Square from, Square to)
        {
            from.Piece = null;
            to.Piece = piece;
            to.Piece.Square = To;
        }

        public void UndoForOne(Piece piece, Square from, Square to)
        {
            from.Piece = piece;
            from.Piece.Square = from;
            to.Piece = null;
        }

        public override string ToString()
        {
            return $"Castling from {From} to {To}";
        }
    }
}

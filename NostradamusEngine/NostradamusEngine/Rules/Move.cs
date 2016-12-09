using NostradamusEngine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Rules
{
    public class Move
    {
        private readonly Square _from;
        private readonly Square _to;
        private readonly Piece _capture;

        public Move(Piece piece, Square from, Square to, Piece capture)
        {
            Piece = piece;
            _from = from;
            _to = to;
            _capture = capture;
        }

        public virtual void Do()
        {
            To.Piece = Piece;
            To.Piece.Square = To;
            From.Piece = null;
        }

        public virtual void Undo()
        {
            From.Piece = Piece;
            From.Piece.Square = To;
            To.Piece = Capture;
        }


        public override string ToString()
        {
            return
                $"{Piece.FullName} from {_from.Name} to {_to.Name}.  Capture : {(_capture == null ? "None" : Capture.FullName)}";
        }

        public Piece Piece { get; }

        public Square From => _from;

        public Square To => _to;

        public Piece Capture => _capture;

        public static bool operator ==(Move a, Move b)
        {
            // This is not enough
            if (object.ReferenceEquals(b,null) && object.ReferenceEquals(a,null)) return true;
            if (object.ReferenceEquals(b, null)) return false;
            if (object.ReferenceEquals(a, null)) return false;

            // For now we could compare by ref, as it is the same squares and pieces.
            return (a.Piece == b.Piece && a.From == b.From && a.To == b.To && a.Capture==b.Capture);
        }

        public static bool operator !=(Move a, Move b)
        {
            return !(a == b);
        }
    }
}

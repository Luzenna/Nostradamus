using NostradamusEngine.Board;
using NostradamusEngine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Rules
{
    public class Move
    {
        private Piece piece;
        private Square from;
        private Square to;
        private Piece capture;

        public Move(Piece piece, Square from, Square to, Piece capture)
        {
            this.piece = piece;
            this.from = from;
            this.to = to;
            this.capture = capture;
        }

        public override string ToString()
        {
            return String.Format("{0} from {1} to {2}.  Capture : {3}", piece.FullName, from.Name, to.Name, capture==null?"None":Capture.FullName);
        }

        public Piece Piece
        {
            get
            {
                return piece;
            }
        }

        public Square From
        {
            get
            {
                return from;
            }
        }

        public Square To
        {
            get
            {
                return to;
            }
        }

        public Piece Capture
        {
            get
            {
                return capture;
            }
        }

        public static bool operator ==(Move a, Move b)
        {
            // For now we could compare by ref, as it is the same squares and pieces.
            return (a.Piece == b.Piece && a.From == b.From && a.To == b.To && a.Capture==b.Capture);
        }

        public static bool operator !=(Move a, Move b)
        {
            return !(a == b);
        }
    }
}

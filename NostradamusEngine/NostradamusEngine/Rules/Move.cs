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
            return String.Format("{0} from {1} to {2}.  Capture : {3}", piece.FullName, from.Name, to.Name, capture==null?"None":piece.FullName);
        }

    }
}

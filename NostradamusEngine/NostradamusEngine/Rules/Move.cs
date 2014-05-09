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

        public Move(Piece piece, Square from, Square to)
        {
            this.piece = piece;
            this.from = from;
            this.to = to;
        }
    }
}

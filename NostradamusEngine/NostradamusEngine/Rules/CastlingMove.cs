using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Rules
{
    class CastlingMove : Move
    {
        public CastlingMove(Piece piece, Square from, Square to, Piece capture) : base(piece, @from, to, capture)
        {
        }
    }
}

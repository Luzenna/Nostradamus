using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Moves
{
    public class EnPassantMove : NormalMove
    {
        public EnPassantMove(Piece piece, ISquare from, ISquare to, Piece capture, int ply) : base(piece, @from, to, capture,ply)
        {
        }


    }
}

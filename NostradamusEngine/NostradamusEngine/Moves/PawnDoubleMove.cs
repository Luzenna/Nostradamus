using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Moves
{
    public class PawnDoubleMove:NormalMove
    {
        public PawnDoubleMove(Piece piece, ISquare from, ISquare to, int ply)
            : base(piece, from, to, null, ply)
        {
            
        }
    }
}

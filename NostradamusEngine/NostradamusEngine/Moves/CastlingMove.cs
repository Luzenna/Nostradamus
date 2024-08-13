using NostradamusEngine.Pieces;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusEngine.Moves
{
    public class CastlingMove : NormalMove
    {
        public Piece CastlingRook { get; }
        public ISquare CastlingRookFrom { get; }
        public ISquare CastlingRookTo { get; }

        public CastlingMove(Piece king, ISquare from, ISquare to, Piece rook,ISquare castlingRookFrom, ISquare castlingRookTo,int ply) : base(king, from, to, null,ply)
        {
            CastlingRook = rook;
            CastlingRookFrom = castlingRookFrom;
            CastlingRookTo = castlingRookTo;
        }

        

        public override string ToString()
        {
            return $"Castling from {From} to {To}";
        }
    }
}

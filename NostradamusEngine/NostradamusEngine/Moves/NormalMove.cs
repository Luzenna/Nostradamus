using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Moves
{
    public class NormalMove
    {
        private readonly ISquare _from;
        private readonly ISquare _to;
        private readonly Piece _capture;

        public NormalMove(Piece piece, ISquare from, ISquare to, Piece capture, int ply)
        {
            Piece = piece;
            Ply = ply;
            _from = from;
            _to = to;
            _capture = capture;
        }

        // Will be removed, just kept as part of refactoring
        public void Do()
        {
            Piece.Move(this);
        }

        // Will be removed, just kept as part of refactoring
        public void Undo()
        {
            Piece.UndoMove(this);
        }

        public override string ToString()
        {
            return
                $"{Piece.Color} {Piece.FullName} from {_from} to {_to}.  Capture : {(_capture == null ? "None" : Capture.FullName)}";
        }

        public Piece Piece { get; }
        public int Ply { get; set; }

        public ISquare From => _from;

        public ISquare To => _to;

        public Piece Capture => _capture;

        public static bool operator ==(NormalMove a, NormalMove b)
        {
            // This is not enough
            if (object.ReferenceEquals(b,null) && object.ReferenceEquals(a,null)) return true;
            if (object.ReferenceEquals(b, null)) return false;
            if (object.ReferenceEquals(a, null)) return false;

            // For now we could compare by ref, as it is the same squares and pieces.
            return (a.Piece == b.Piece && a.From == b.From && a.To == b.To);
        }

        public static bool operator !=(NormalMove a, NormalMove b)
        {
            return !(a == b);
        }
    }
}

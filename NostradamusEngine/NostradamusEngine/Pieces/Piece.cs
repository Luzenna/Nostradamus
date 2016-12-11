using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Moves;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusEngine.Pieces
{
    public abstract class Piece
    {
        // Ugly
        public readonly  List<NormalMove> Moves;
        public readonly List<NormalMove> undoedMoves = new List<NormalMove>();
        protected readonly IBoard Board;

        protected Piece(Color color, IBoard board)
        {
            Color = color;
            Board = board;
            Moves = new List<NormalMove>();
        }

        public virtual void Move(NormalMove m)
        {
            if (m.Capture!=null)
                Board.RemovePiece(m.To,m.Capture);
            Board.RemovePiece(m.From, this);
            Board.SetPiece(m.To,this);
            Moves.Add(m);
        }

        public virtual void UndoMove(NormalMove m)
        {
            Board.RemovePiece(m.To, this);
            if (m.Capture != null)
            {
                Board.SetPiece(m.To,m.Capture);
            }
            Board.SetPiece(m.From,this);
            Moves.Remove(m);
            undoedMoves.Add(m);
        }



        public abstract string FullName
        {
            get;
        }

        public abstract string ShortName
        {
            get;
        }

        public abstract IEnumerable<ISquare> FindCoveredSquares();

        public abstract IEnumerable<NormalMove> CalculateAllMoves(int ply);

        public virtual NormalMove IsLegalMove(NormalMove normalMove, int ply)
        {
            foreach (var m in CalculateAllMoves(ply))
            {
                if (normalMove == m)
                    return m;
            }
            return null;
        }

        public ISquare Square
        {
            get;
            set;
        }

        public Color Color { get; private set; }

    }

}

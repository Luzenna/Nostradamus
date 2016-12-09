using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public abstract class Piece
    {
        protected readonly  List<Move> Moves;
        public Piece(Color color, Square square, ChessEngine game)
        {
            Color = color;
            Square = square;
            Game = game;
            Moves = new List<Move>();
        }


        public void Move(Move m)
        {
            Moves.Add(m);
        }

        public void UndoMove(Move m)
        {
            Moves.Remove(m);
        }

        public ChessEngine Game
        {
            get;
            private set;
        }

        public abstract String FullName
        {
            get;
        }

        public abstract String ShortName
        {
            get;
        }

        public abstract IEnumerable<Move> CalculateAllMoves();

        public virtual Boolean IsLegalMove(Move move)
        {
            foreach (Move m in CalculateAllMoves())
            {
                if (move == m)
                    return true;
            }
            return false;
        }


        public Square Square
        {
            get;
            set;
        }

        public Color Color { get; private set; }

    }

}

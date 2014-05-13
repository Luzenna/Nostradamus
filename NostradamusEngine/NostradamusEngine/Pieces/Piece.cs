using NostradamusEngine.Board;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public abstract class Piece
    {
        
        public Piece(Boolean isWhite, Square square, ChessEngine game)
        {
            IsWhite = isWhite;
            Square = square;
            Game = game;
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

        public Boolean IsWhite
        {
            get;
            private set;
        }

        public Square Square
        {
            get;
            set;
        }
    }

}

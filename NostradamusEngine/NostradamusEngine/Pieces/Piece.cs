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
        
        public Piece(Boolean isWhite, Square square)
        {
            IsWhite = isWhite;
            Square = square;
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

        public Boolean IsWhite
        {
            get;
            set;
        }

        public Square Square
        {
            get;
            set;
        }
    }

}

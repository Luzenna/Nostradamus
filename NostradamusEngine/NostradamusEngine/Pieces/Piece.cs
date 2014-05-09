using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public abstract class Piece
    {
        
        public Piece(Boolean isWhite)
        {
            IsWhite = isWhite;
        }

        public abstract String FullName
        {
            get;
        }

        public abstract String ShortName
        {
            get;
        }

        public Boolean IsWhite
        {
            get;
            set;
        }
    }

}

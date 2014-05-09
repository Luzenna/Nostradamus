using NostradamusEngine.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Board
{
    public class Square
    {
        private Boolean isWhite;
        private Int32 file, row;
        private Piece piece;

        public Square(Boolean isWhite, Int32 file, Int32 row)
        {
            this.isWhite = isWhite;
            this.row = row;
            this.file = file;
        }

        public Boolean IsWhite
        {
            get
            {
                return isWhite;
            }
        }

        public Piece Piece
        {
            get
            {
                return piece;
            }
            set
            {
                piece = value;
            }
        }

    }
}

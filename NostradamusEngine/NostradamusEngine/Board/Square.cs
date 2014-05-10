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
        private Int32 file, rank;
        private Piece piece;

        public Square(Boolean isWhite, Int32 file, Int32 row)
        {
            this.isWhite = isWhite;
            this.rank = row;
            this.file = file;
        }

        public Int32 Rank
        {
            get
            {
                return rank;
            }
        }

        public Int32 File
        {
            get
            {
                return file;
            }
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

        public String Name
        {
            get
            {
                char fileName = (char)(file + 97);
                return String.Format("{0}{1}", fileName, rank);
            }
        }

    }
}

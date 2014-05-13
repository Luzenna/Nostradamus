using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Board
{
    public class Table
    {
        private const Int32 files = 8;
        private const Int32 ranks = 8;

        private Square[,] squares;

        public Table()
        {
            squares = new Square[files, ranks];
            // a1 is black.
            for (var r=0;r<ranks;r++)
            {
                var currentIsWhite = (r%2)!=0;

                for (var f=0;f<files;f++)
                {
                    squares[f, r] = new Square(currentIsWhite,f,r);
                    currentIsWhite = !currentIsWhite;
                }
            }
        }

        public Square this[Int32 file, Int32 rank]
        {
            get
            {
                if (file >= files || rank >= ranks || file < 0 || rank < 0)
                    return null;
                return squares[file, rank];
            }
        }

        public Square this[String name]
        {
            get
            {
                Int32 rank = Int32.Parse(name[1].ToString());
                Int32 file = name[0].ToString().ToLower()[0]-97;
                return this[file, rank-1];
            }
        }

        public Int32 Files
        {
            get
            {
                return files;
            }
        }

        public Int32 Ranks
        {
            get
            {
                return ranks;
            }
        }

    }
}

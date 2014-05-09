using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Queen : Piece
    {

        public Queen(Boolean isWhite)
            : base(isWhite)
        {

        }


        public override String FullName
        {
            get
            {
                return "Quuen";
            }
        }

        public override String ShortName
        {
            get
            {
                return "Q";
            }
        }
    }
}

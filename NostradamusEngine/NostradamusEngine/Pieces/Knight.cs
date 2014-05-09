using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Knight : Piece
    {

        public Knight(Boolean isWhite)
            : base(isWhite)
        {

        }


        public override String FullName
        {
            get
            {
                return "Knight";
            }
        }

        public override String ShortName
        {
            get
            {
                return "N";
            }
        }
    }
}

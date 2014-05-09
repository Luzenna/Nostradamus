using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Rook : Piece
    {

        public Rook(Boolean isWhite)
            : base(isWhite)
        {

        }

        public override String FullName
        {
            get
            {
                return "Rook";
            }
        }

        public override String ShortName
        {
            get
            {
                return "R";
            }
        }
    }
}

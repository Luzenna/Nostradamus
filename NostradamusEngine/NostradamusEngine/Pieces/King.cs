using NostradamusEngine.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class King : Piece
    {
        public King(Boolean isWhite, Square square)
            : base(isWhite, square)
        {

        }


        public override String FullName
        {
            get
            {
                return "King";
            }
        }

        public override String ShortName
        {
            get
            {
                return "K";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            throw new NotImplementedException();
        }

    }
}

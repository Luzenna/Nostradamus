using NostradamusEngine.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Knight : Piece
    {

        public Knight(Boolean isWhite, Square square)
            : base(isWhite, square)
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

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            throw new NotImplementedException();
        }

    }
}

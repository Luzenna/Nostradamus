using NostradamusEngine.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Queen : Piece
    {

        public Queen(Boolean isWhite, Square square)
            : base(isWhite, square)
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

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            throw new NotImplementedException();
        }

    }
}

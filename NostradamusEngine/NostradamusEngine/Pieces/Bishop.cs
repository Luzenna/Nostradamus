using NostradamusEngine.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Bishop : Piece
    {

        public Bishop(Boolean isWhite, Square square)
            : base(isWhite, square)
        {

        }

        public override String FullName
        {
            get
            {
                return "Bishop";
            }
        }

        public override String ShortName
        {
            get
            {
                return "B";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            throw new NotImplementedException();
        }
    }
}

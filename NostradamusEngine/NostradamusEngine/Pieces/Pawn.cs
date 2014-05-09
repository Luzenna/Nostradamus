using NostradamusEngine.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Pieces
{
    public class Pawn : Piece
    {

        public Pawn(Boolean isWhite, Square square)
            : base(isWhite, square)
        {

        }


        public override String FullName
        {
            get
            {
                return "Pawn";
            }
        }

        public override String ShortName
        {
            get
            {
                return "P";
            }
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            yield return null;
        }

    }
}

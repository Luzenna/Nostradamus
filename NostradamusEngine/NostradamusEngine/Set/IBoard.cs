using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Moves;
using NostradamusEngine.Pieces;

namespace NostradamusEngine.Set
{
    public interface IBoard
    {
        Piece GetPieceOn(ISquare square);
        void SetPiece(ISquare square, Piece piece);
        void RemovePiece(ISquare square,Piece piece);
        SquareStatus GetSquareStatus(ISquare square);
        bool PiecesCoverOneOrMore(Color color,IEnumerable<ISquare> squares);

        IEnumerable<NormalMove> GetAllMovesFor(Color color, int ply);

        // Should be moved, but here as part 1 of refactoring
        Boolean PlayingIsInCheck(Color color);
        King GetKing(Color color);
    }
}

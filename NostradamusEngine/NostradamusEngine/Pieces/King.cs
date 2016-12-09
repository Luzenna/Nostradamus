using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine.Pieces
{
    public class King : Piece
    {
        public bool CanCastleQueenSide { get; set; }
        public bool CanCastleKingSide { get; set; }

        public King(Color color, Square square, ChessEngine game)
            : base(color, square, game)
        {

        }


        public override string FullName => "King";

        public override string ShortName => "K";

        public override IEnumerable<Rules.Move> CalculateAllMoves()
        {
            var allMoves = new List<Move>();
            allMoves.AddRange(CheckSquare(-1, -1));
            allMoves.AddRange(CheckSquare(0, -1));
            allMoves.AddRange(CheckSquare(1, -1));
            allMoves.AddRange(CheckSquare(-1, 0));
            allMoves.AddRange( CheckSquare(1, 0));
            allMoves.AddRange(CheckSquare(-1, 1));
            allMoves.AddRange(CheckSquare(0, 1));
            allMoves.AddRange(CheckSquare(1, 1));
            if (IsKingSideCastlePossible)
                allMoves.Add(new CastlingMove(this,Square,Game.Board[6,Square.Rank],Game.Board[7,Square.Rank].Piece,Game.Board[7,Square.Rank],Game.Board[5,Square.Rank]));
            if (IsQueenSideCastlePossible)
                allMoves.Add(new CastlingMove(this, Square, Game.Board[2, Square.Rank], Game.Board[0, Square.Rank].Piece, Game.Board[0, Square.Rank], Game.Board[3, Square.Rank]));

            return allMoves;
        }

        private bool IsQueenSideCastlePossible
        {
            get
            {
                if (!CanCastleQueenSide) return false;
                if (Game.Board[0, Square.Rank].Piece!=null && Game.Board[0, Square.Rank].Piece.Moves.Count == 0 && Game.Board[1,Square.Rank].Piece==null && Game.Board[2,Square.Rank].Piece==null) return true;
                return false;
            }
        }

        private bool IsKingSideCastlePossible
        {
            get
            {
                if (!CanCastleQueenSide) return false;
                if (Game.Board[7, Square.Rank].Piece!=null && Game.Board[7, Square.Rank].Piece.Moves.Count == 0 && Game.Board[5, Square.Rank].Piece == null && Game.Board[6, Square.Rank].Piece == null) return true;
                return false;
            }
        }

        private IEnumerable<Rules.Move> CheckSquare(int fileAdder, int rankAdder)
        {
            var squareToCheck = Game.Board[Square.File + fileAdder, Square.Rank + rankAdder];
            if (squareToCheck == null)
                yield break;    
            if (squareToCheck.Piece == null)
            {
                yield return new Move(this, Square, squareToCheck, null);
            }
            else if (squareToCheck.Piece.Color != this.Color)
            {
                yield return new Move(this, Square, squareToCheck, squareToCheck.Piece);
            }
        }
    }
}

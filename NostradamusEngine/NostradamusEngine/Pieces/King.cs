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

        public override IEnumerable<Square> FindCoveredSquares()
        {
            var coveredSquares = new List<Square>();
            coveredSquares.AddRange(CheckIfSquaredIsCovered(-1, -1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(0, -1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(1, -1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(-1, 0));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(1, 0));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(-1, 1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(0, 1));
            coveredSquares.AddRange(CheckIfSquaredIsCovered(1, 1));
            return coveredSquares;
        }

        public override IEnumerable<Rules.Move> CalculateAllMoves(int ply)
        {
            var allMoves = new List<Move>();
            allMoves.AddRange(CheckMove(-1, -1, ply));
            allMoves.AddRange(CheckMove(0, -1, ply));
            allMoves.AddRange(CheckMove(1, -1, ply));
            allMoves.AddRange(CheckMove(-1, 0, ply));
            allMoves.AddRange( CheckMove(1, 0, ply));
            allMoves.AddRange(CheckMove(-1, 1, ply));
            allMoves.AddRange(CheckMove(0, 1, ply));
            allMoves.AddRange(CheckMove(1, 1, ply));
            if (IsKingSideCastlePossible)
                allMoves.Add(new CastlingMove(this,Square,Game.Board[6,Square.Rank],Game.Board[7,Square.Rank].Piece,Game.Board[7,Square.Rank],Game.Board[5,Square.Rank],ply));
            if (IsQueenSideCastlePossible)
                allMoves.Add(new CastlingMove(this, Square, Game.Board[2, Square.Rank], Game.Board[0, Square.Rank].Piece, Game.Board[0, Square.Rank], Game.Board[3, Square.Rank],ply));

            return allMoves;
        }

        private bool IsQueenSideCastlePossible
        {
            get
            {
                if (!CanCastleQueenSide) return false;
                if (Game.SquareIsCoveredByOpponentPiece(Color,CastleSquare(3,Square.Rank)) || Game.SquareIsCoveredByOpponentPiece(Color,CastleSquare(2, Square.Rank)) || Game.SquareIsCoveredByOpponentPiece(Color,CastleSquare(4, Square.Rank))) return false;
                if (Game.Board[0, Square.Rank].Piece!=null && Game.Board[0, Square.Rank].Piece.Moves.Count == 0 && Game.Board[1,Square.Rank].Piece==null && Game.Board[2,Square.Rank].Piece==null && Game.Board[3, Square.Rank].Piece == null) return true;
                return false;
            }
        }


        private bool IsKingSideCastlePossible
        {
            get
            {
                if (!CanCastleQueenSide) return false;
                if (Game.SquareIsCoveredByOpponentPiece(Color,CastleSquare(6, Square.Rank)) || Game.SquareIsCoveredByOpponentPiece(Color,CastleSquare(5, Square.Rank)) || Game.SquareIsCoveredByOpponentPiece(Color,CastleSquare(4, Square.Rank))) return false;
                if (Game.Board[7, Square.Rank].Piece!=null && Game.Board[7, Square.Rank].Piece.Moves.Count == 0 && Game.Board[5, Square.Rank].Piece == null && Game.Board[6, Square.Rank].Piece == null) return true;
                return false;
            }
        }

        private Square CastleSquare(int p0, int rank)
        {
            return new Square(Square.IsWhite, p0, rank);
        }


        private IEnumerable<Square> CheckIfSquaredIsCovered(int fileAdder, int rankAdder)
        {
            var squareToCheck = Game.Board[Square.File + fileAdder, Square.Rank + rankAdder];
            if (squareToCheck == null)
                yield break;
            if (squareToCheck.Piece == null || squareToCheck.Piece.Color == Color)
            {
                yield return squareToCheck;
            }
        }

        private IEnumerable<Rules.Move> CheckMove(int fileAdder, int rankAdder, int ply)
        {
            var squareToCheck = Game.Board[Square.File + fileAdder, Square.Rank + rankAdder];
            if (squareToCheck == null)
                yield break;    
            if (squareToCheck.Piece == null)
            {
                yield return new Move(this, Square, squareToCheck, null,ply);
            }
            else if (squareToCheck.Piece.Color != this.Color)
            {
                yield return new Move(this, Square, squareToCheck, squareToCheck.Piece,ply);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NostradamusEngine.Moves;
using NostradamusEngine.Pieces;

namespace NostradamusEngine.Set.SimpleBoard
{
    public class Board : IBoard
    {
        private const int files = 8;
        private const int ranks = 8;
        private readonly List<Piece> pieces;
        private readonly List<Piece> captured;

        private readonly Square[,] _squares;

        private static readonly log4net.ILog Log =
log4net.LogManager.GetLogger(typeof(Board));


        public Board()
        {
            pieces = new List<Piece>();
            captured = new List<Piece>();
            _squares = new Square[files, ranks];
            SetupBoardLook();
        }

        private void SetupBoardLook()
        {
            for (var r = 0; r < ranks; r++)
            {
                var currentIsWhite = (r % 2) != 0;

                currentIsWhite = ColorSquareOnRank(r, currentIsWhite);
            }
        }

        private bool ColorSquareOnRank(int r, bool currentIsWhite)
        {
            for (var f = 0; f < files; f++)
            {
                _squares[f, r] = new Square(currentIsWhite, f, r);
                currentIsWhite = !currentIsWhite;
            }

            return currentIsWhite;
        }

        public Piece GetPieceOn(ISquare square)
        {
            var piece = GetSquare(square.File, square.Rank)?.Piece;
            return piece;
        }

        public void SetPiece(ISquare square, Piece piece)
        {
            pieces.Add(piece);
            var internalSquare = GetSquare(square.File, square.Rank);
            if (internalSquare == null) throw new IllegalSquareException();
            piece.Square = internalSquare;
            internalSquare.Piece = piece;
        }

        public void RemovePiece(ISquare square, Piece piece)
        {
            pieces.Remove(piece);
            var internalSquare = GetSquare(square.File, square.Rank);
            if (internalSquare == null) throw new IllegalSquareException();
            piece.Square = null;
            internalSquare.Piece = null;
        }

        public SquareStatus GetSquareStatus(ISquare square)
        {
            var internalSquare = GetSquare(square.File, square.Rank);
            if (internalSquare == null) return SquareStatus.Illegal;
            else if (internalSquare.Piece!=null) return SquareStatus.Occupied;
            return SquareStatus.Empty;
        }

        private Square GetSquare(int f, int r)
        {
                if (f >= files || f < 0) return null;
                if (r >= ranks || r < 0) return null;
                return _squares[f, r];
        }

        public bool PiecesCoverOneOrMore(Color color, IEnumerable<ISquare> squares)
        {
            return squares.Any(square => SquareIsCoveredByOpponentPiece(color, ToInternalSquare(square)));
        }

        private Square ToInternalSquare(ISquare square)
        {
            return GetSquare(square.File, square.Rank);
        }

        public IEnumerable<NormalMove> GetAllMovesFor(Color color, int currentPly)
        {
            var piecesThatCanMove = pieces.Where(x => x.Color == color && x.Square != null).ToList();
            foreach (var piece in piecesThatCanMove)
            {
                var allMoves = piece.CalculateAllMoves(currentPly + 1).ToList();
                foreach (var move in allMoves)
                {
                    piece.Move(move);
                    if (!PlayingIsInCheck(color))
                    {
                        yield return move;
                    }
                    piece.UndoMove(move);

                }
            }
        }

        private bool SquareIsCoveredByOpponentPiece(Color color, ISquare square, bool checkCheck = false)
        {
            return
                pieces.Where(x => x.Color != color)
                    .Any(
                        opponentPiece =>
                        {
                            return
                                opponentPiece.FindCoveredSquares()
                                    .Count(x =>
                                    {
                                        var value = x.File == square.File && x.Rank == square.Rank;
                                        return value;
                                    }) > 0;
                        });
        }
        // Checks wether a move is valid or if one is in check after the move.
        public Boolean PlayingIsInCheck(Color color)
        {
            var playingKing = GetKing(color);
            return SquareIsCoveredByOpponentPiece(color, playingKing.Square, true);
        }

        public IEnumerable<Piece> GetPiecesOfType<T>(Color color) where T : Piece
        {
            return pieces.Where(x => x is T && x.Color == color);
        }

        public King GetKing(Color color)
        {
            return (King)GetPiecesOfType<King>(color).First();
        }

        public int Files => files;

        public int Ranks => ranks;

    }

    public class IllegalSquareException : Exception
    {
    }
}

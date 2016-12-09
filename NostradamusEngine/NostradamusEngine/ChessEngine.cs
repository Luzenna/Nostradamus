using NostradamusEngine.IO;
using NostradamusEngine.Pieces;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Set;

namespace NostradamusEngine
{
    public class ChessEngine
    {
        private readonly List<Move> moves;
        private readonly List<Piece> pieces;
        private readonly List<Piece> captured;
        private int _currentPly = 0;

        private static readonly log4net.ILog Log =
    log4net.LogManager.GetLogger(typeof(ChessEngine));

        public ChessEngine()
        {
            Board = new Board();
            moves = new List<Move>();
            pieces = new List<Piece>();
            captured = new List<Piece>();
        }

        public void LoadFEN(String fen)
        {
            FENParser.LoadFEN(this, fen);
        }

        public void AddPiece(Piece piece)
        {
            pieces.Add(piece);
        }


        public void Move(Move suggestedMove)
        {
            PromotionHappened = false;
            var correctMove = IsLegalMove(suggestedMove);
            if (correctMove!=null)
            {
                moves.Add(correctMove);
                correctMove.Do();
                _currentPly++;
                if (correctMove.Capture != null)
                {
                    pieces.Remove(correctMove.Capture);
                    captured.Add(correctMove.Capture);
                    correctMove.Capture.Square = null;
                }
                if (PlayingIsInCheck)
                {
                    _currentPly--;
                    correctMove.Undo();
                    moves.Remove(correctMove);
                    return;
                }
                ToMove = SwitchColor(ToMove);
                var pawn = correctMove.To.Piece as Pawn;
                if (pawn != null && pawn.IsPromoted)
                {
                    PromotionHappened = true;
                    PromotedPawn = pawn;
                }
            }
        }

        public IEnumerable<Piece> GetPiecesOfType<T>(Color color) where T : Piece
        {
            return pieces.Where(x => x is T && x.Color == color);
        }

        public King GetKing(Color color)
        {
            return (King) GetPiecesOfType<King>(color).First();
        }

        private Color SwitchColor(Color toMove)
        {
            return toMove == Color.White ? Color.Black : Color.White;
        }

        public bool SquareIsCoveredByOpponentPiece(Color color, Square square)
        {
            Log.Debug($"Check if square {square} is covered.");
            return
                pieces.Where(x => x.Color != color)
                    .Any(
                        opponentPiece =>
                        {
                            Log.Debug($" - Piece {opponentPiece}");
                            return
                                opponentPiece.FindCoveredSquares()
                                    .Count(x =>
                                    {
                                        Log.Debug($" -- Square : {x}");
                                        return x.File == square.File && x.Rank == square.Rank;
                                    }) > 0;
                        });
        }

        // Checks wether a move is valid or if one is in check after the move.
        private Boolean PlayingIsInCheck
        {
            get
            {
                var playingKing = GetKing(ToMove);
                return SquareIsCoveredByOpponentPiece(Opponent, playingKing.Square);
            }
        }

        // Supposed to check for checks and other stuff.
        private Move IsLegalMove(Move move)
        {
            // uh-oh
            var correctMove = move.Piece.IsLegalMove(move,_currentPly);
            if (move.Piece.Color==ToMove && correctMove!=null)
                return correctMove;
            return null;
        }

        public Color ToMove
        {
            get;
            set;
        }

        public Color Opponent
        {
            get
            {
                if (ToMove == Color.White) return Color.Black;
                return Color.White;
            }
        }

        public Board Board { get; }

        public bool PromotionHappened
        {
            get;
            private set;
        }

        public Pawn PromotedPawn
        {
            get;
            private set;
        }

    }
}

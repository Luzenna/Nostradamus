using NostradamusEngine.IO;
using NostradamusEngine.Pieces;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Evaluators;
using NostradamusEngine.Set;

namespace NostradamusEngine
{
    public class ChessEngine
    {
        private readonly IEvaluator _evaluator;
        private readonly List<Move> moves;
        private readonly List<Piece> pieces;
        private readonly List<Piece> captured;
        private int _currentPly = 0;

        private static readonly log4net.ILog Log =
    log4net.LogManager.GetLogger(typeof(ChessEngine));

        public ChessEngine(IEvaluator evaluator)
        {
            _evaluator = evaluator;
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
                if (PlayingIsInCheck(ToMove))
                {
                    _currentPly--;
                    correctMove.Undo();
                    moves.Remove(correctMove);
                    return;
                }
                var pawn = correctMove.To.Piece as Pawn;
                if (pawn != null && pawn.IsPromoted)
                {
                    PromotionHappened = true;
                    PromotedPawn = pawn;
                }
                _evaluator.EvaluateGame(this, ToMove);
                ToMove = ColorHelper.Reverse(ToMove);
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

        public bool SquareIsCoveredByOpponentPiece(Color color, Square square, bool checkCheck=false)
        {
            Log.Debug($"Check if square {square} is covered by opponent to {color} pieces.");
            return
                pieces.Where(x => x.Color != color)
                    .Any(
                        opponentPiece =>
                        {
                            Log.Debug($" - Piece {opponentPiece} on {opponentPiece.Square} covers");
                            return
                                opponentPiece.FindCoveredSquares()
                                    .Count(x =>
                                    {
                                        var debugInfo = x.Piece?.ToString() ?? "<None>";
                                        Log.Debug($" -- Square : {x} : {debugInfo}");
                                        var value = x.File == square.File && x.Rank == square.Rank;
                                        return value;
                                    }) > 0;
                        });
        }

        public bool KingIsInCheck(Color color)
        {
            var king = GetKing(ColorHelper.Reverse(color));
            // give moving color one extra move, can anyone capture the king?
            return pieces.Where(x => x.Color == color).ToList()
                .Any(piece => piece.CalculateAllMoves(-1).Count(move => move.Capture == king)>0);
        }

        public IEnumerable<Move> GetAllValidMoves(Color color)
        {
            var clock = new Stopwatch();
            clock.Start();
            foreach (var piece in pieces.Where(x => x.Color == color))
            {
                foreach (var move in piece.CalculateAllMoves(_currentPly + 1))
                {
                    Move(move);
                    if (!PlayingIsInCheck(Opponent))
                        yield return move;

                }
            }
            clock.Stop();
            Log.Info($"GetAllValidMoves took {clock.ElapsedMilliseconds} ms");
        }

        // Checks wether a move is valid or if one is in check after the move.
        private Boolean PlayingIsInCheck(Color color)
        {
                var playingKing = GetKing(color);
                return SquareIsCoveredByOpponentPiece(color, playingKing.Square,true);
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

        public Color Opponent => ColorHelper.Reverse(ToMove);

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

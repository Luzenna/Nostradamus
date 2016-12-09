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
                if (correctMove.Capture != null)
                {
                    pieces.Remove(correctMove.Capture);
                    captured.Add(correctMove.Capture);
                    correctMove.Capture.Square = null;
                }
                if (PlayingIsInCheck)
                {
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

        public bool SquareIsCoveredByOpponentPiece(Color color,Square square)
        {
            return pieces.Where(x => x.Color != color).Any(opponentPiece => opponentPiece.FindCoveredSquares().Count(x => x.File==square.File && x.Rank==square.Rank) > 0);
        }

        // Checks wether a move is valid or if one is in check after the move.
        private Boolean PlayingIsInCheck
        {
            get
            {
                foreach (var piece in pieces)
                {
                    foreach (var move in piece.CalculateAllMoves() )
                    {
                        if (move.Capture!=null && move.Capture.ShortName == "K" && move.Capture.Color == this.ToMove)
                            return true;
                    }
                }
                return false;
            }
        }

        // Supposed to check for checks and other stuff.
        private Move IsLegalMove(Move move)
        {
            // uh-oh
            var correctMove = move.Piece.IsLegalMove(move);
            if (move.Piece.Color==ToMove && correctMove!=null)
                return correctMove;
            return null;
        }

        public Color ToMove
        {
            get;
            set;
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

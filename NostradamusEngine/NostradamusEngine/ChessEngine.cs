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
        private Board board;
        private List<Move> moves;
        private List<Piece> pieces;
        private List<Piece> captured;

        public ChessEngine()
        {
            board = new Board();
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


        public void Move(Move move)
        {
            PromotionHappened = false;
            if (IsLegalMove(move))
            {
                moves.Add(move);
                move.To.Piece = move.Piece;
                move.To.Piece.Square = move.To;
                move.From.Piece = null;
                if (move.Capture != null)
                {
                    pieces.Remove(move.Capture);
                    captured.Add(move.Capture);
                    move.Capture.Square = null;
                }
                if (PlayingIsInCheck)
                {
                    UndoMove(move);
                    return;
                }
                ToMove = SwitchColor(ToMove);
                var pawn = move.To.Piece as Pawn;
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

        public void UndoMove(Move move)
        {
            move.From.Piece = move.Piece;
            move.From.Piece.Square = move.To;
            move.To.Piece = move.Capture;
            moves.Remove(move);
        }


        // Checks wether a move is valid or if one is in check after the move.
        private Boolean PlayingIsInCheck
        {
            get
            {
                foreach (Piece piece in pieces)
                {
                    foreach (Move move in piece.CalculateAllMoves() )
                    {
                        if (move.Capture!=null && move.Capture.ShortName == "K" && move.Capture.Color == this.ToMove)
                            return true;
                    }
                }
                return false;
            }
        }

        // Supposed to check for checks and other stuff.
        private bool IsLegalMove(Move move)
        {
            // uh-oh
            if (move.Piece.Color==ToMove && move.Piece.IsLegalMove(move))
                return true;
            return false;
        }

        public Color ToMove
        {
            get;
            set;
        }

        public Board Board
        {
            get
            {
                return board;
            }
        }

        public Boolean PromotionHappened
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

using NostradamusEngine.Board;
using NostradamusEngine.IO;
using NostradamusEngine.Pieces;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine
{
    public class ChessEngine
    {
        private Table board;
        private Castling whiteCastling, blackCastling;
        private List<Move> moves;
        private List<Piece> pieces;
        private List<Piece> captured;

        public ChessEngine()
        {
            board = new Table();
            whiteCastling = new Castling();
            blackCastling = new Castling();
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
                IsWhiteToMove = !IsWhiteToMove;
                var pawn = move.To.Piece as Pawn;
                if (pawn != null && pawn.IsPromoted)
                {
                    PromotionHappened = true;
                    PromotedPawn = pawn;
                }
            }
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
                        if (move.Capture!=null && move.Capture.ShortName == "K" && move.Capture.IsWhite == this.IsWhiteToMove)
                            return true;
                    }
                }
                return false;
            }
        }

        // Supposed to check for checks and other stuff.
        private Boolean IsLegalMove(Move move)
        {
            // uh-oh
            if (move.Piece.IsWhite==IsWhiteToMove && move.Piece.IsLegalMove(move))
                return true;
            return false;
        }

        public Boolean IsWhiteToMove
        {
            get;
            set;
        }

        public Table Board
        {
            get
            {
                return board;
            }
        }

        public Castling BlackCastling
        {
            get
            {
                return blackCastling;
            }
        }

        public Castling WhiteCastling
        {
            get
            {
                return whiteCastling;
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

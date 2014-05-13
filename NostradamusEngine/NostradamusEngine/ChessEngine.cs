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

        public ChessEngine()
        {
            board = new Table();
            whiteCastling = new Castling();
            blackCastling = new Castling();
            moves = new List<Move>();
        }

        public void LoadFEN(String fen)
        {
            FENParser.LoadFEN(this, fen);
        }

        public void Move(Move move)
        {
            PromotionHappened = false;
            if (IsLegalMove(move))
            {
                moves.Add(move);
                move.To.Piece = move.From.Piece;
                move.From.Piece = null;
                move.To.Piece.Square = move.To;
                // Some of this logic is probably better to put in the piece classes.

                IsWhiteToMove = !IsWhiteToMove;
                var pawn = move.To.Piece as Pawn;
                if (pawn != null && pawn.IsPromoted)
                {
                    PromotionHappened = true;
                    PromotedPawn = pawn;
                }
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

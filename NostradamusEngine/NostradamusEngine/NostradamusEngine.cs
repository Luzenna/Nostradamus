using NostradamusEngine.Board;
using NostradamusEngine.IO;
using NostradamusEngine.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine
{
    public class NostradamusEngine
    {
        private Table board;
        private Castling whiteCastling, blackCastling;

        public NostradamusEngine()
        {
            board = new Table();
            whiteCastling = new Castling();
            blackCastling = new Castling();
        }

        public void LoadFEN(String fen)
        {
            FENParser.LoadFEN(this, fen);
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

    }
}

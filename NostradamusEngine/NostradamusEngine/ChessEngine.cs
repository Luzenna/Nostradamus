using NostradamusEngine.IO;
using NostradamusEngine.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Evaluators;
using NostradamusEngine.Moves;
using NostradamusEngine.Set;
using NostradamusEngine.Set.SimpleBoard;

namespace NostradamusEngine
{
    public class ChessEngine
    {
        private readonly IEvaluator _evaluator;
        private readonly IBoard _board;
        private readonly List<NormalMove> moves;
        private int _currentPly = 0;

        private static readonly log4net.ILog Log =
    log4net.LogManager.GetLogger(typeof(ChessEngine));

        public ChessEngine(IEvaluator evaluator, IBoard board)
        {
            _evaluator = evaluator;
            _board = board;
            moves = new List<NormalMove>();
        }

        public void LoadFEN(String fen)
        {
            FENParser.LoadFEN(this,_board, fen);
        }

        public int Perft(int depth,Color color, NormalMove lastmove=null)
        {
            if (depth == 0)
            {
                Log.Debug($"Came from {lastmove}");
                return 0;
            }
            var clock = new Stopwatch();
            clock.Start();
            var allValidMoves = _board.GetAllMovesFor(color,depth).ToList();
            var subMoves = 0;
            foreach (var move in allValidMoves)
            {
                move.Do();
                subMoves+=Perft(depth-1,ColorHelper.Reverse(color),move);
                move.Undo();
            }
            clock.Stop();
            if (depth>4)
                Log.Info($"Depth {depth} took {clock.ElapsedMilliseconds}.  Looked at {lastmove} {allValidMoves.Count} moves, which resulted in {subMoves} submoves");
            return allValidMoves.Count+subMoves;
        }



        // Supposed to check for checks and other stuff.
        private NormalMove IsLegalMove(NormalMove normalMove)
        {
            // uh-oh
            var correctMove = normalMove.Piece.IsLegalMove(normalMove,_currentPly);
            if (normalMove.Piece.Color==ToMove && correctMove!=null)
                return correctMove;
            return null;
        }

        public Color ToMove
        {
            get;
            set;
        }

        public Color Opponent => ColorHelper.Reverse(ToMove);


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

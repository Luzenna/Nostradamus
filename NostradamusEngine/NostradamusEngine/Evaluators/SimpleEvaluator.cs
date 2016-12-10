using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Pieces;

namespace NostradamusEngine.Evaluators
{
    public class SimpleEvaluator : IEvaluator
    {
        private static readonly log4net.ILog Log =
    log4net.LogManager.GetLogger(typeof(SimpleEvaluator));

        public GameEvaluation EvaluateGame(ChessEngine game, Color lastMover)
        {
            var clock = new Stopwatch();
            clock.Start();
            var eval = new GameEvaluation()
            {
                IsCheck = game.KingIsInCheck(lastMover),
                NumberOfValidMoves = game.GetAllValidMoves(ColorHelper.Reverse(lastMover)).ToList().Count
            };
            clock.Stop();
            Log.Info($"Evaluation after {lastMover}'s move (took {clock.ElapsedMilliseconds} ms) :");
            Log.Info(eval);

            return eval;
        }


    }
}

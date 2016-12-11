using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Evaluators
{
    public class SimpleEvaluator : IEvaluator
    {
        private static readonly log4net.ILog Log =
    log4net.LogManager.GetLogger(typeof(SimpleEvaluator));

        public GameEvaluation EvaluateGame(IBoard board, Color lastMover)
        {
            var eval = new GameEvaluation()
            {
                IsCheck = board.PlayingIsInCheck(lastMover),
                NumberOfValidMoves = board.GetAllMovesFor(ColorHelper.Reverse(lastMover),0).ToList().Count
            };
            return eval;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NostradamusEngine.Pieces;
using NostradamusEngine.Set;

namespace NostradamusEngine.Evaluators
{
    public interface IEvaluator
    {
        GameEvaluation EvaluateGame(ChessEngine game, Color lastMover);

    }
}

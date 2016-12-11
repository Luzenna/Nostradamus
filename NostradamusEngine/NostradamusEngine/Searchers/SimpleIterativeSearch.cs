using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NostradamusEngine.Searchers
{
    public class SimpleIterativeSearch : ISearcher
    {
        private readonly int _maxNumberOfIterations;

        public SimpleIterativeSearch(int maxNumberOfIterations)
        {
            _maxNumberOfIterations = maxNumberOfIterations;
        }

        public int SearchAndEvaluate()
        {
            throw new NotImplementedException();
        }
    }
}

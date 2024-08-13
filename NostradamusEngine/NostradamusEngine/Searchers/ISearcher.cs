namespace NostradamusEngine.Searchers
{
    public interface ISearcher
    {
        /// <summary>
        /// Search and evaluate the positions found.
        /// </summary>
        /// <returns>Number of moves checked.</returns>
        int SearchAndEvaluate();
    }
}

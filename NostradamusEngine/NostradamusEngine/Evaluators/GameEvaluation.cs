namespace NostradamusEngine.Evaluators
{
    public class GameEvaluation
    {
        public decimal Centipawns { get; set; }
        public bool IsMate => IsCheck && NumberOfValidMoves == 0;
        public bool IsStaleMate => !IsCheck && NumberOfValidMoves == 0;
        public bool IsCheck { get; set; }
        public int NumberOfValidMoves { get; set; }

        public override string ToString()
        {
            return
                $"Mate : {IsMate}, StaleMate: {IsStaleMate}, Check: {IsCheck}, NumberOfValidMoves: {NumberOfValidMoves}";
        }
    }
}
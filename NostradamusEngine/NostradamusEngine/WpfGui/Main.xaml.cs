
namespace NostradamusEngine.WpfGui
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {
        private ChessEngine _game;

        public Main()
        {
            InitializeComponent();
            _game = new ChessEngine();
            _game.LoadFEN("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
            //_game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            BoardControl.SetGame(_game);
        }

        public void Update()
        {
            BoardControl.Update();
        }

    }
}

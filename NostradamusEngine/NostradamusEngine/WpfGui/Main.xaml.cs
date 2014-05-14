
using System.Windows;

namespace NostradamusEngine.WpfGui
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main
    {
        private ChessEngine _game;

        private GuiControl _control;

        public Main()
        {
            InitializeComponent();
            _control = new GuiControl(this, BoardControl);
            _game = new ChessEngine();
            _game.LoadFEN("rnbqkbnr/ppp1pppp/8/3p4/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 1");
            //_game.LoadFEN("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            BoardControl.SetGame(_game, _control);
        }

        public void Update()
        {
            BoardControl.Update();
        }

        /// <summary>
        /// Routing event to controller... might be a better way to do this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _control.Main_OnSizeChanged(sender, e);
        }

        private void Flip_Click(object sender, RoutedEventArgs e)
        {
            _control.FlipBoard();
        }
    }
}

using System;
using System.Windows.Media;

namespace NostradamusEngine.WpfGui
{
    public class GuiControl
    {

        private Main _main;
        private Board _board;

        public GuiControl(Main main, Board board)
        {
            _main = main;
            _board = board;
        }

        public Double BoardWidth { get; set; }

        public Double BoardHeight { get; set; }

        internal void Main_OnSizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            BoardWidth = _main.BoardViewbox.ActualWidth;
            BoardHeight = _main.BoardViewbox.ActualHeight;
        }

        internal void FlipBoard()
        {
            RotateTransform rotateTransform1 = new RotateTransform(180, _board.ActualWidth/2, _board.ActualHeight/2);
            //_main.BoardControl.RenderTransform = rotateTransform1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NostradamusEngine.WpfGui
{

    public struct Cell
    {
        public short Row;
        public short Column;
        public UserControl PieceControl;
    }

    /// <summary>
    /// First version of the board where all logic happens in this view. Lazyness prevails thus far!
    /// ToDo: Move to a MVC or MVVM design pattern
    /// </summary>
    public partial class Board
    {

        private List<Cell> _cellList = new List<Cell>(24);

        private Double _cellSize;

        private const Single CellSizePercentage = 0.8F;

        private ChessEngine _game;

        public Board()
        {
            InitializeComponent();
            var white = true;
            // This SHOULD probably be moved into the Board.xaml 
            // Add background colors

            for (var i = 1; i <= 8; i++)
            {
                for (var j = 1; j <= 8; j++)
                {
                    // We use borders for colors... because it worked
                    var border = new Border
                    {
                        Background = new SolidColorBrush(white ? Color.FromRgb(240, 217, 181) : Color.FromRgb(173, 120, 82))
                    };
                    if (j != 8)
                    {
                        white = !white;
                    }
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    BoardGrid.Children.Add(border);
                }

                /*
                 * LABELS HAVE BEEN MOVED INTO THE XAML
                 * 
                var label = new TextBlock
                {
                    Text = i.ToString(CultureInfo.InvariantCulture),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetColumn(label, 0);
                Grid.SetRow(label, 9-i);
                BoardGrid.Children.Add(label);

                const char startChar = 'a';
                label = new TextBlock
                {
                    Text = ((Char)(startChar - 1 + i)).ToString(CultureInfo.InvariantCulture),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(label, 9);
                Grid.SetColumn(label, i);
                BoardGrid.Children.Add(label);
                */

            }

        }

        /// <summary>
        /// Sets the ChessEngine to be used for this game
        /// </summary>
        public void SetGame(ChessEngine game)
        {
            _game = game;
        }

        /// <summary>
        /// Updates the board with the piece positions. This method sucks and should be rewritten.
        /// </summary>
        public void Update()
        {
            // ToDo: Better WPF logic where we reuse instead of remove and add... this is probably horrible performance wise
            foreach (var cell in _cellList)
            {
                BoardGrid.Children.Remove(cell.PieceControl);
            }
            _cellList.Clear();

            // Draw the pieces!
            for (var r = _game.Board.Ranks - 1; r >= 0; r--)
            {
                for (var f = 0; f < _game.Board.Files; f++)
                {
                    var piece = _game.Board[f, r].Piece;

                    // As of now we only have logic on board tiles with pieces.. if no piece is found we continue
                    if (piece == null)
                    {
                        continue;
                    }

                    // ToDo: Find another method of adding the pieces. This is ugly.
                    var pieceTypeName = "NostradamusEngine.WpfGui.Pieces.";
                    if (piece.IsWhite)
                    {
                        pieceTypeName += "White";
                    }
                    else
                    {
                        pieceTypeName += "Black";
                    }
                    pieceTypeName += piece.FullName;

                    // Nooo! Reflection :( I MUST FIND ANOTHER WAY
                    var pieceUiType = Type.GetType(pieceTypeName);
                    if (pieceUiType != null)
                    { 
                        var pieceUiElement = (UserControl)Activator.CreateInstance(pieceUiType);
                        Grid.SetColumn(pieceUiElement, f + 1);
                        Grid.SetRow(pieceUiElement, 8 - r);
                        pieceUiElement.Width = _cellSize*CellSizePercentage;
                        pieceUiElement.Height = _cellSize*CellSizePercentage;

                        // Basic drag and drop. Still pretty buggy
                        pieceUiElement.MouseMove += pieceUiElement_MouseMove;
                        pieceUiElement.MouseLeftButtonUp += pieceUiElement_MouseLeftButtonUp;
                        pieceUiElement.MouseLeftButtonDown += pieceUiElement_MouseLeftButtonDown;


                        BoardGrid.Children.Add(pieceUiElement);

                        _cellList.Add(new Cell { PieceControl = pieceUiElement });
                    }
                }
            }
        }

        // When bugs are fixed, move these. 
        Point _anchorPoint;
        Point _currentPoint;
        bool _isInDrag;
        private TranslateTransform transform = new TranslateTransform();

        /// <summary>
        /// Basic drag and drop. Still pretty buggy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceUiElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            _anchorPoint = e.GetPosition(null);
            element.CaptureMouse();
            _isInDrag = true;
            e.Handled = true;
        }

        /// <summary>
        /// Basic drag and drop. Still pretty buggy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceUiElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isInDrag)
            {
                var element = sender as FrameworkElement;
                element.ReleaseMouseCapture();
                _isInDrag = false;
                e.Handled = true;
                transform = new TranslateTransform();
            }
        }

        /// <summary>
        /// Basic drag and drop. Still pretty buggy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceUiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInDrag)
            {
                var element = sender as FrameworkElement;
                _currentPoint = e.GetPosition(null);

                transform.X += _currentPoint.X - _anchorPoint.X;
                transform.Y += (_currentPoint.Y - _anchorPoint.Y);
                element.RenderTransform = transform;
                _anchorPoint = _currentPoint;
            }
        }

        
        protected override void OnRender(DrawingContext dc)
        {
            _cellSize = (BoardGrid.ActualWidth - 40) / 8;
            base.OnRender(dc);

            // Call on update if we have a game (to draw the pieces)
            if (_game != null)
            {
                Update();
            }
        }
        
    }
}

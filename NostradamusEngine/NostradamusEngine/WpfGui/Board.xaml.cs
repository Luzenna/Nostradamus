using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using NostradamusEngine.Moves;
using NostradamusEngine.Pieces;
using Color = System.Windows.Media.Color;

namespace NostradamusEngine.WpfGui
{
    
    /// <summary>
    /// First version of the board where all logic happens in this view. Lazyness prevails thus far!
    /// ToDo: NormalMove to a MVC or MVVM design pattern. This class will be a pain to work with unless a design pattern is picked! wow. single class. much code. such lazy. wow.
    /// </summary>
    public partial class Board
    {

        /// <summary>
        /// Used to keep track of the UI pieces.
        /// </summary>
        private readonly List<UserControl> _pieceUserControls = new List<UserControl>(24);

        /// <summary>
        /// Cell Size (Grid cell) in pixels. Used when drawing up the UI Pieces
        /// </summary>
        private Double _cellSize;

        /// <summary>
        /// How much space of the grid cell do we want the piece to ocupy?
        /// </summary>
        private const Single CellSizePercentage = 0.8F;

        /// <summary>
        /// The actual game!
        /// </summary>
        private ChessEngine _game;

        /// <summary>
        /// Drag and drop anchor point
        /// </summary>
        Point _anchorPoint;

        /// <summary>
        /// Drag and drop current point
        /// </summary>
        Point _currentPoint;

        /// <summary>
        /// Are we currently dragging a UI piece?
        /// </summary>
        bool _isInDrag;

        /// <summary>
        /// Transform used in piece UI movement (following the mouse)
        /// </summary>
        private TranslateTransform _transform = new TranslateTransform();

        /// <summary>
        /// Keep track of what column and row (file and rank) the mouse is currenly at
        /// </summary>
        private Int32 _dropColumn, _dropRow;


        private GuiControl _control;

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
        public void SetGame(ChessEngine game, GuiControl control)
        {
            _game = game;
            _control = control;
        }

        /// <summary>
        /// Updates the board with the piece positions. This method sucks and should be rewritten.
        /// </summary>
        public void Update()
        {
            // ToDo: Better WPF logic where we reuse instead of remove and add... this is probably horrible performance wise
            foreach (var piece in _pieceUserControls)
            {
                BoardGrid.Children.Remove(piece);
            }
            _pieceUserControls.Clear();

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
                    var pieceTypeName = $"NostradamusEngine.WpfGui.Pieces.{piece.Color}";
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
                        pieceUiElement.Tag = piece;

                        // Basic drag and drop.
                        pieceUiElement.MouseLeftButtonDown += pieceUiElement_MouseLeftButtonDown;
                        pieceUiElement.MouseMove += pieceUiElement_MouseMove;
                        pieceUiElement.MouseLeftButtonUp += pieceUiElement_MouseLeftButtonUp;


                        BoardGrid.Children.Add(pieceUiElement);

                        _pieceUserControls.Add(pieceUiElement);
                    }
                }
            }
        }
        

        /// <summary>
        /// Basic drag and drop. Mouse down on a piece starts the drag and drop action and puts the Board into drag'n'drop mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceUiElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null)
            {
                return;
            }
            _anchorPoint = e.GetPosition(null);
            element.CaptureMouse();
            _isInDrag = true;
            e.Handled = true;
        }

        /// <summary>
        /// Piece drag and drop mouse button release event. Ends drag'n'drop mode and tries to move the piece. Then updates the board.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceUiElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isInDrag)
            {
                var element = sender as FrameworkElement;
                if (element == null)
                {
                    return;
                }
                element.ReleaseMouseCapture();
                _isInDrag = false;
                e.Handled = true;
                _transform = new TranslateTransform();

                // Where on the board are we? Figure it out by mouse position
                Point currentPosRelBoard = e.GetPosition(BoardGrid);
                double start = 0.0;

                // grid has top left origin vs chess board which have buttom left origin. 
                // There are also two extra columns and two extra rows we have to take into account.
                _dropColumn = -1;
                _dropRow = 8;
                foreach (var rowDef in BoardGrid.RowDefinitions)
                {
                    start += rowDef.ActualHeight;
                    if (currentPosRelBoard.Y < start)
                    {
                        break;
                    }
                    _dropRow--;
                }

                start = 0.0;
                foreach (var columnDef in BoardGrid.ColumnDefinitions)
                {
                    start += columnDef.ActualWidth;
                    if (currentPosRelBoard.X < start)
                    {
                        break;
                    }
                    _dropColumn++;
                }

                // Do the chess move logic
                var piece = (Piece) element.Tag;
                var toSquare = _game.Board[_dropColumn, _dropRow];
                // Check if tosquare is valid
                if (toSquare != null)
                {
                    _game.Move(new NormalMove(piece, piece.Square, toSquare, toSquare.Piece,0));
                    if (_game.PromotionHappened)
                        _game.PromotedPawn.Square.Piece = new Queen(_game.PromotedPawn.Color, _game.PromotedPawn.Square, _game);
                }
                Update();
            }
        }

        /// <summary>
        /// Mouse movement where we
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pieceUiElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isInDrag)
            {
                var element = sender as FrameworkElement;
                if (element == null)
                {
                    return;
                }
                _currentPoint = e.GetPosition(null);

                _transform.X += (_currentPoint.X - _anchorPoint.X) * BoardGrid.ActualWidth / _control.BoardWidth;
                _transform.Y += (_currentPoint.Y - _anchorPoint.Y) * BoardGrid.ActualHeight / _control.BoardHeight;
                element.RenderTransform = _transform;
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

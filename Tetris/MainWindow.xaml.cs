using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TetrisBlock _currentTetrisBlock;
        private DispatcherTimer _timer;
        private List<UIElement> _placedBlocks;
        private Dictionary<double, List<UIElement>> _rowDictionary;
        private double _lowestRow;
        private bool[] _canClearRow;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _placedBlocks = new List<UIElement>();
            _lowestRow = 19;
            _canClearRow = new bool[20];
            _rowDictionary = new Dictionary<double, List<UIElement>>
            {
                [0] = new List<UIElement>(),
                [1] = new List<UIElement>(),
                [2] = new List<UIElement>(),
                [3] = new List<UIElement>(),
                [4] = new List<UIElement>(),
                [5] = new List<UIElement>(),
                [6] = new List<UIElement>(),
                [7] = new List<UIElement>(),
                [8] = new List<UIElement>(),
                [9] = new List<UIElement>(),
                [10] = new List<UIElement>(),
                [11] = new List<UIElement>(),
                [12] = new List<UIElement>(),
                [13] = new List<UIElement>(),
                [14] = new List<UIElement>(),
                [15] = new List<UIElement>(),
                [16] = new List<UIElement>(),
                [17] = new List<UIElement>(),
                [18] = new List<UIElement>(),
                [19] = new List<UIElement>()
            };
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)StartButton.Content == "Start")
            {
                StartButton.Content = "Stop";

                // Setup timer
                _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                _timer.Tick += Timer_Tick;

                // Random start block
                var values = Enum.GetValues(typeof(BlockType));
                var random = new Random();
                var randomBlock = (BlockType)values.GetValue(random.Next(values.Length));
                _currentTetrisBlock = new TetrisBlock(randomBlock, ref PlaySpaceCanvas);

                _timer.Start();
            }
            else
            {
                StartButton.Content = "Start";
                _timer.Stop();
                PlaySpaceCanvas.Children.Clear();
                _placedBlocks.Clear();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //_timer.Stop();
            // Run boolean collision check helper function
            // If collision returns true, prevent from dropping further and place block
            if (_currentTetrisBlock.WillCollideBottom(ref PlaySpaceCanvas) || _currentTetrisBlock.WillCollideBelowBlock(_placedBlocks))
            {
                // Add blocks to list of placed blocks
                foreach (var block in _currentTetrisBlock.GetBlockArray)
                {
                    _placedBlocks.Add(block);
                }

                // Row Clear Check
                foreach (var currentBlock in _placedBlocks)
                {
                    RowChecker(currentBlock);
                }

                // Clear rows based on above
                ClearRows();

                // Create new random block
                var values = Enum.GetValues(typeof(BlockType));
                var random = new Random();
                var randomBlock = (BlockType)values.GetValue(random.Next(values.Length));

                _currentTetrisBlock = new TetrisBlock(randomBlock, ref PlaySpaceCanvas);
            }
            else
            {
                // Move block down 1 block if space is available
                _currentTetrisBlock.MoveBlock(0, 1, ref PlaySpaceCanvas);
            }
            //_timer.Start();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Make sure the block doesn't phase through walls
            switch (e.Key)
            {
                // Left
                case Key.A:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 0 && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0)
                        _currentTetrisBlock.MoveBlock(-1, 0, ref PlaySpaceCanvas);
                    break;
                // Right
                case Key.D:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 1 && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 1)
                        _currentTetrisBlock.MoveBlock(1, 0, ref PlaySpaceCanvas);
                    break;
                // Down
                case Key.S:
                    if (!_currentTetrisBlock.WillCollideBottom(ref PlaySpaceCanvas) && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0 && !_currentTetrisBlock.WillCollideBelowBlock(_placedBlocks))
                        _currentTetrisBlock.MoveBlock(0, 1, ref PlaySpaceCanvas);
                    break;
                // Counter Clock Wise
                case Key.Q:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 0 && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0)
                        _currentTetrisBlock.RotateBlock(false);
                    break;
                // Counter Clock Wise Right Hand
                case Key.J:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 0 && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0)
                        _currentTetrisBlock.RotateBlock(false);
                    break;
                // Clock Wise
                case Key.E:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 0 && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0)
                        _currentTetrisBlock.RotateBlock(true);
                    break;
                // Clock Wise Right Hand
                case Key.L:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 0 && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0)
                        _currentTetrisBlock.RotateBlock(true);
                    break;
            }

        }

        private void RowChecker(UIElement currentBlock)
        {
            // Only add if new
            if (!_rowDictionary.Values.Any(row => row.Contains(currentBlock)))
            {
                // We use this to just track where blocks are positioned (change this code later if we lag)
                var currentRow = Canvas.GetTop(currentBlock) / 25;
                _rowDictionary[currentRow].Add(currentBlock);

                // Mark for delete if 10 exist
                if (_rowDictionary[currentRow].Count == 10)
                    _canClearRow[(int)currentRow] = true;
            }
        }

        private void ClearRows()
        {
            if (_canClearRow.Contains(true))
            {
                for (double i = 0; i < 20; i++)
                {
                    // If row is marked
                    if (_canClearRow[(int)i])
                    {
                        foreach (var element in _rowDictionary[i])
                        {
                            PlaySpaceCanvas.Children.Remove(element);
                            _placedBlocks.Remove(element);
                        }

                        // Get the current lowest row on the grid so we know what we can move down.
                        _lowestRow = i;

                        // Update row to nothing
                        _rowDictionary[i] = new List<UIElement>();
                    }
                }

                // Move down rows until lowest cleared row is hit
                for (double i = 0; i < _lowestRow; i++)
                {
                    foreach (var block in _rowDictionary[i])
                    {
                        Canvas.SetTop(block, Canvas.GetTop(block) + 25);
                    }
                }

                // Shift keys down 1
                for (var i = _lowestRow; i > 0; i--)
                {
                    // Set current row to above row
                    _rowDictionary[i] = _rowDictionary[i - 1];
                }

                // Reset rows that can be cleared
                _canClearRow = new bool[20];
            }
        }
    }
}

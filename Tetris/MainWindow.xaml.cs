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
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _placedBlocks = new List<UIElement>();
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
                foreach (var placedBlock in _placedBlocks)
                {
                    var dictOfRows = new Dictionary<double, int[]>();
                    //dictOfRows[0] = new int[];
                    //dictOfRows[1] = new int[];
                }

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
                    if (!_currentTetrisBlock.WillCollideBottom(ref PlaySpaceCanvas) && _currentTetrisBlock.WillCollideSideBlock(_placedBlocks) != 0)
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
    }
}

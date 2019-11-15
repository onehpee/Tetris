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
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);

            // Test Block
            _currentTetrisBlock = new TetrisBlock(BlockType.I, ref PlaySpaceCanvas);
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)StartButton.Content == "Start")
            {
                StartButton.Content = "Stop";

                _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };

                _timer.Tick += Timer_Tick;

                _timer.Start();
            }
            else
            {
                StartButton.Content = "Start";
                _timer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Run boolean collision check helper function
            // If collision returns true, prevent from dropping further and place block
            if (_currentTetrisBlock.WillCollideBottom(ref PlaySpaceCanvas))
            {
                // Create new random block
                _currentTetrisBlock = new TetrisBlock(BlockType.L, ref PlaySpaceCanvas);
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
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 0) 
                        _currentTetrisBlock.MoveBlock(-1, 0, ref PlaySpaceCanvas);
                    break;
                // Right
                case Key.D:
                    if (_currentTetrisBlock.WillCollideWall(ref PlaySpaceCanvas) != 1)
                        _currentTetrisBlock.MoveBlock(1, 0, ref PlaySpaceCanvas);
                    break;
            }
        }
    }
}

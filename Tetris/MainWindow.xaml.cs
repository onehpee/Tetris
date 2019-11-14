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
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _currentTetrisBlock = new TetrisBlock(BlockType.L, ref PlaySpaceCanvas);
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            var timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 1)};

            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Run boolean collision check helper function
            // Move block down 50px if space is available
            _currentTetrisBlock.MoveBlock(1, 1, ref PlaySpaceCanvas);
        }
    }
}

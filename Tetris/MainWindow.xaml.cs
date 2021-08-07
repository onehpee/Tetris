﻿using System;
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
using System.Resources;
using System.IO;
using System.Media;
using System.Diagnostics;
using System.Threading;




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
        private int _lowestRow;
        private bool[] _canClearRow;

        
        
        



        private int _totalRowsCleared;
        private int _totalPointsEarned;
        private int _totalSecondsPlayed;
        private string _ticingclck;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            var sp = new SoundPlayer {SoundLocation = "Music/tetris-gameboy-02.wav"};
            sp.PlayLooping();
        }

        /// <summary>
        /// Button click event to start a new game or stop current game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch Stopwatch = new Stopwatch();
            
            
            if ((string)StartButton.Content == "Start")
            {
               
                Thread.Sleep(1000);
                StartButton.Content = "Stop";
                Timelabel.Content = "";
                //Stopwatch.Stop();
                TimeSpan ts = Stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);

                Timelabel.Content += elapsedTime;
                ScoreLabel.Content = _totalPointsEarned;
                MessageBox.Show("Score: " + _totalPointsEarned + "\n" + "Rows Cleared" + _totalRowsCleared + "\n" + elapsedTime);
                // Reset Variables 
                _placedBlocks = new List<UIElement>();
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

                // Setup timer
                _timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                _timer.Tick += Timer_Tick;

               

                // Random start block
                var values = Enum.GetValues(typeof(BlockType));
                var random = new Random();
                var randomBlock = (BlockType)values.GetValue(random.Next(values.Length));
                _currentTetrisBlock = new TetrisBlock(randomBlock, ref PlaySpaceCanvas);

                _timer.Start();
                Stopwatch.Start();
                Thread.Sleep(1000);
            }
            else
            {

                StartButton.Content = "Start";
                _timer.Stop();
                PlaySpaceCanvas.Children.Clear();
                _placedBlocks.Clear();
               
                // Console.WriteLine("RunTime " + elapsedTime);
                // var currentime = elapsedTime;
                //_ticingclck += currentime;

                
            }
            
        }

        /// <summary>
        /// Checks to perform every timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Track time
            _totalSecondsPlayed++;

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

                if (GameOverCheck())
                {
                    _timer.Stop();
                    var endGameWindow = new EndGameWindow(_totalSecondsPlayed, _totalPointsEarned, _totalRowsCleared);
                    endGameWindow.ShowDialog();
                }
                    
            }
            else
            {
                // Move block down 1 block if space is available
                _currentTetrisBlock.MoveBlock(0, 1, ref PlaySpaceCanvas);
            }
        }

        /// <summary>
        /// All keypress listeners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Checks if a row is ready to be marked for deletion
        /// </summary>
        /// <param name="currentBlock">Current block in the foreach that will call this method</param>
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

        /// <summary>
        /// Helper function that handles row clearing
        /// </summary>
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

                        // Update lowest row
                        _lowestRow = (int)i;
                        // Update row to nothing
                        _rowDictionary[i] = new List<UIElement>();
                    }
                }
                
                // Get number of rows cleared

               // var rowsCleared = _canClearRow.Count(x => x);
                //MessageBox.Show("Rows cleared" + rowsCleared);
                //var indexesToClear = Enumerable.Range(0, _canClearRow.Length)
                //    .Where(i => _canClearRow[i])
                //    .ToList();

                var rowsCleared = _canClearRow.Count(x => x);
                _totalRowsCleared += rowsCleared;
                _totalPointsEarned += rowsCleared * 50;
                ScoreLabel.Content = _totalPointsEarned;
                

                // Scan for row that you can shift down to
                for (var i = 0; i < 20; i++)
                {
                    if (_rowDictionary[i].Count == 0)
                    {
                        // When empty row is found, pull above rows down 1;
                        for (var j = i; j >= 0; j--)
                        {
                            // Move blocks
                            foreach (var block in _rowDictionary[j])
                            {
                                Canvas.SetTop(block, Canvas.GetTop(block) + 25);
                            }

                            // If we're not at the top row, we shift the keys
                            if (j != 0)
                                _rowDictionary[j] = _rowDictionary[j - 1];
                            else
                                _rowDictionary[j] = new List<UIElement>();
                        }
                    }
                }

                // Reset rows that can be cleared
                _canClearRow = new bool[20];
            }
        }

        /// <summary>
        /// Checks if the game has completed by checking zones where
        /// block collision will occur
        /// </summary>
        /// <returns>true or false depending of if game is ready to complete</returns>
        private bool GameOverCheck()
        {
            for (var i = 0; i < 3; i++)
            {
                foreach (var block in _rowDictionary[i])
                {
                    if (Canvas.GetLeft(block) > 125 || Canvas.GetLeft(block) < 175)
                        return true;
                }
            }

            return false;
        }

        
    }
}

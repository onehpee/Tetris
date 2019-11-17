﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for EndGameWindow.xaml
    /// </summary>
    public partial class EndGameWindow : Window
    {
        public EndGameWindow(int timePlayed, int totalScore, int rowsCleared)
        {
            InitializeComponent();
            EndGameTextBlock.Text =
                $"The current game has ended. Time played is {timePlayed} seconds, score is {totalScore}, and rows cleared was {rowsCleared}. {Environment.NewLine}If you wish to play again, close this window and press stop and then start again.";
        }
    }
}

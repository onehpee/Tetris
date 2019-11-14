using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris
{
    public enum BlockType
    {
        I = 0,
        L = 1,
        T = 2,
        Cube = 3,
        Z = 4,
        ReverseZ = 5
    }

    public class TetrisBlock
    {
        private BlockType _blockType;
        private int _currentX;
        private int _currentY;
        private readonly Color _blockColor;
        private bool[] _cellFill;

        public TetrisBlock(BlockType blockType, ref Grid playSpaceGrid)
        {
            _blockType = blockType;
            _blockColor = new Color();
            _cellFill = new bool[12];

            switch (blockType)
            {
                case BlockType.I:
                    _blockColor.R = 50;
                    _blockColor.G = 244;
                    _blockColor.B = 100;
                    break;
                case BlockType.L:
                    _blockColor.R = 50;
                    _blockColor.G = 244;
                    _blockColor.B = 100;
                    break;
                case BlockType.T:
                    _blockColor.R = 50;
                    _blockColor.G = 244;
                    _blockColor.B = 100;
                    break;
                case BlockType.Cube:
                    _blockColor.R = 50;
                    _blockColor.G = 244;
                    _blockColor.B = 100;
                    break;
                case BlockType.Z:
                    _blockColor.R = 50;
                    _blockColor.G = 244;
                    _blockColor.B = 100;
                    break;
                case BlockType.ReverseZ:
                    _blockColor.R = 50;
                    _blockColor.G = 244;
                    _blockColor.B = 100;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockType), blockType, null);
            }
            for (var i = 0; i < 10; i++)
                playSpaceGrid.RowDefinitions.Add(new RowDefinition());
            for (var i = 0; i < 20; i++)
                playSpaceGrid.ColumnDefinitions.Add(new ColumnDefinition());

            DrawBlock(ref playSpaceGrid);
        }

        /// <summary>
        /// Draws the block onto the grid.
        /// </summary>
        private void DrawBlock(ref Grid playSpaceGrid)
        {
            for (var i = 0; i < 200; i++)
            {
                // Create a single block of the set color
                var singleBlock = new Rectangle {Width = 50, Height = 50, Fill = new SolidColorBrush(_blockColor)};
                
                playSpaceGrid.Children.Add(singleBlock);
            }
        }

        /// <summary>
        /// Move the block 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveBlock(int x, int y)
        {
            _currentX = x;
            _currentY = y;

            // Redraw/refresh drawing
        }

        /// <summary>
        /// Rotates the block
        /// </summary>
        /// <param name="clockwise">When true, rotates clockwise. Else, counter clockwise.</param>
        public void RotateBlock(bool clockwise)
        {
            if (clockwise)
            {
                // When rotating clockwise
            }
            else
            {
                // When rotating counter clockwise
            }
        }
    }
}
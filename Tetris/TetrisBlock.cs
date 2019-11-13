using System;
using System.Windows.Media;

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
        private Color _blockColor;

        public TetrisBlock(BlockType blockType)
        {
            _blockType = blockType;
            _blockColor = new Color();

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
            }
        }

        /// <summary>
        /// Draws the block onto the grid.
        /// </summary>
        private void DrawBlock()
        {
            // Draw the block onto the grid
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
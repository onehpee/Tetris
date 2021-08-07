using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

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
        private readonly BlockType _blockType;
        private readonly Color _blockColor;
        private readonly Rectangle[] _blockArray;
        private int _blockRotationState;

        // Dictionaries for each rotation
        private Dictionary<int, int[]> _iRotationDictionary;
        private Dictionary<int, int[]> _lRotationDictionary;
        private Dictionary<int, int[]> _tRotationDictionary;
        private Dictionary<int, int[]> _zRotationDictionary;
        private Dictionary<int, int[]> _rZRotationDictionary;

        /// <summary>
        /// Creates a new Tetris Block based on the block type enum
        /// </summary>
        /// <param name="blockType">I,L,T,Z,RZ</param>
        /// <param name="playSpaceCanvas">Current play space</param>
        public TetrisBlock(BlockType blockType, ref Canvas playSpaceCanvas)
        {
            _blockType = blockType;
            _blockColor = new Color();
            _blockArray = new Rectangle[4];
            _blockRotationState = 0;
           



            // Create instance of block rotation dictionaries
            _iRotationDictionary = new Dictionary<int, int[]>();
            _lRotationDictionary = new Dictionary<int, int[]>();
            _tRotationDictionary = new Dictionary<int, int[]>();
            _zRotationDictionary = new Dictionary<int, int[]>();
            _rZRotationDictionary = new Dictionary<int, int[]>();

            // Set color based on block type
            switch (blockType)
            {
                case BlockType.I:
                    _blockColor = Colors.Gold;
                    break;
                case BlockType.L:
                    _blockColor = Colors.BlueViolet;
                    break;
                case BlockType.T:
                    _blockColor = Colors.Chartreuse;
                    break;
                case BlockType.Cube:
                    _blockColor = Colors.DarkGreen;
                    break;
                case BlockType.Z:
                    _blockColor = Colors.DarkOrange;
                    break;
                case BlockType.ReverseZ:
                    _blockColor = Colors.Red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(blockType), blockType, null);
            }

            // Draw our block on the canvas
            DrawBlock(ref playSpaceCanvas);
        }

        /// <summary>
        /// Public get accessor for the block array
        /// </summary>
        public Rectangle[] GetBlockArray => _blockArray;

        /// <summary>
        /// Draws the block onto the grid.
        /// </summary>
        private void DrawBlock(ref Canvas playSpaceCanvas)
        {

            // Create our 4 blocks
            var block0 = new Rectangle { Width = 25, Height = 25, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua};
            var block1 = new Rectangle { Width = 25, Height = 25, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua };
            var block2 = new Rectangle { Width = 25, Height = 25, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua };
            var block3 = new Rectangle { Width = 25, Height = 25, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua };

            // Assign to array
            _blockArray[0] = block0;
            _blockArray[1] = block1;
            _blockArray[2] = block2;
            _blockArray[3] = block3;

            // Draw to canvas
            playSpaceCanvas.Children.Add(_blockArray[0]);
            playSpaceCanvas.Children.Add(_blockArray[1]);
            playSpaceCanvas.Children.Add(_blockArray[2]);
            playSpaceCanvas.Children.Add(_blockArray[3]);

            switch (_blockType)
            {
                case BlockType.I:
                    Canvas.SetLeft(_blockArray[0], 125);
                    Canvas.SetTop(_blockArray[0], 0);

                    Canvas.SetLeft(_blockArray[1], 125);
                    Canvas.SetTop(_blockArray[1], 25);

                    Canvas.SetLeft(_blockArray[2], 125);
                    Canvas.SetTop(_blockArray[2], 50);

                    Canvas.SetLeft(_blockArray[3], 125);
                    Canvas.SetTop(_blockArray[3], 75);
                    break;
                case BlockType.L:
                    Canvas.SetLeft(_blockArray[0], 125);
                    Canvas.SetTop(_blockArray[0], 0);

                    Canvas.SetLeft(_blockArray[1], 125);
                    Canvas.SetTop(_blockArray[1], 25);

                    Canvas.SetLeft(_blockArray[2], 125);
                    Canvas.SetTop(_blockArray[2], 50);

                    Canvas.SetLeft(_blockArray[3], 150);
                    Canvas.SetTop(_blockArray[3], 50);
                    break;
                case BlockType.T:
                    Canvas.SetLeft(_blockArray[0], 125);
                    Canvas.SetTop(_blockArray[0], 0);

                    Canvas.SetLeft(_blockArray[1], 125);
                    Canvas.SetTop(_blockArray[1], 25);

                    Canvas.SetLeft(_blockArray[2], 100);
                    Canvas.SetTop(_blockArray[2], 25);

                    Canvas.SetLeft(_blockArray[3], 150);
                    Canvas.SetTop(_blockArray[3], 25);
                    break;
                case BlockType.Cube:
                    Canvas.SetLeft(_blockArray[0], 125);
                    Canvas.SetTop(_blockArray[0], 0);

                    Canvas.SetLeft(_blockArray[1], 150);
                    Canvas.SetTop(_blockArray[1], 0);

                    Canvas.SetLeft(_blockArray[2], 125);
                    Canvas.SetTop(_blockArray[2], 25);

                    Canvas.SetLeft(_blockArray[3], 150);
                    Canvas.SetTop(_blockArray[3], 25);
                    break;
                case BlockType.Z:
                    Canvas.SetLeft(_blockArray[0], 100);
                    Canvas.SetTop(_blockArray[0], 0);

                    Canvas.SetLeft(_blockArray[1], 125);
                    Canvas.SetTop(_blockArray[1], 0);

                    Canvas.SetLeft(_blockArray[2], 125);
                    Canvas.SetTop(_blockArray[2], 25);

                    Canvas.SetLeft(_blockArray[3], 150);
                    Canvas.SetTop(_blockArray[3], 25);
                    break;
                case BlockType.ReverseZ:
                    Canvas.SetLeft(_blockArray[0], 150);
                    Canvas.SetTop(_blockArray[0], 0);

                    Canvas.SetLeft(_blockArray[1], 125);
                    Canvas.SetTop(_blockArray[1], 0);

                    Canvas.SetLeft(_blockArray[2], 125);
                    Canvas.SetTop(_blockArray[2], 25);

                    Canvas.SetLeft(_blockArray[3], 100);
                    Canvas.SetTop(_blockArray[3], 25);
                    break;
            }

        }

        /// <summary>
        /// Move the block 
        /// </summary>
        /// <param name="x"># blocks across (pos values down | neg values up)</param>
        /// <param name="y"># blocks down (pos values down | neg values up)</param>
        /// <param name="playSpaceCanvas">Represents the canvas that blocks exist in</param>
        public void MoveBlock(int x, int y, ref Canvas playSpaceCanvas)
        {
            var xOffset = (x * 25);
            var yOffset = (y * 25);

            Canvas.SetLeft(_blockArray[0], Canvas.GetLeft(_blockArray[0]) + xOffset);
            Canvas.SetTop(_blockArray[0], Canvas.GetTop(_blockArray[0]) + yOffset);

            Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + xOffset);
            Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + yOffset);

            Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + xOffset);
            Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + yOffset);

            Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + xOffset);
            Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + yOffset);
        }

        /// <summary>
        /// Rotates the block
        /// </summary>
        /// <param name="clockwise">When true, rotates clockwise. Else, counter clockwise.</param>
        public void RotateBlock(bool clockwise)
        {
            CreateRotationPositions();
            if (clockwise)
            {
                // Update state
                if (_blockRotationState == 3)
                    _blockRotationState = 0;
                else
                    _blockRotationState++;

                // Rotate blocks clockwise
                switch (_blockType)
                {
                    case BlockType.I:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _iRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _iRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _iRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _iRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _iRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _iRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.L:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _lRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _lRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _lRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _lRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _lRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _lRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.T:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _tRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _tRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _tRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _tRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _tRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _tRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.Z:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _zRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _zRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _zRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _zRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _zRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _zRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.ReverseZ:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _rZRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _rZRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _rZRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _rZRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _rZRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _rZRotationDictionary[_blockRotationState][5]);
                        break;
                }
            }
            else
            {
                // Update state
                // Update state
                if (_blockRotationState == 3)
                    _blockRotationState = 0;
                else
                    _blockRotationState++;

                // Rotate blocks clockwise
                switch (_blockType)
                {
                    case BlockType.I:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _iRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _iRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _iRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _iRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _iRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _iRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.L:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _lRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _lRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _lRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _lRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _lRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _lRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.T:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _tRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _tRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _tRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _tRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _tRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _tRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.Z:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _zRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _zRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _zRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _zRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _zRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _zRotationDictionary[_blockRotationState][5]);
                        break;
                    case BlockType.ReverseZ:
                        Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[1]) + _rZRotationDictionary[_blockRotationState][0]);
                        Canvas.SetTop(_blockArray[1], Canvas.GetTop(_blockArray[1]) + _rZRotationDictionary[_blockRotationState][1]);

                        Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[2]) + _rZRotationDictionary[_blockRotationState][2]);
                        Canvas.SetTop(_blockArray[2], Canvas.GetTop(_blockArray[2]) + _rZRotationDictionary[_blockRotationState][3]);

                        Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[3]) + _rZRotationDictionary[_blockRotationState][4]);
                        Canvas.SetTop(_blockArray[3], Canvas.GetTop(_blockArray[3]) + _rZRotationDictionary[_blockRotationState][5]);
                        break;
                }
            }
        }

        /// <summary>
        /// Check collision status for left (0), right (1), none(2)
        /// </summary>
        /// <param name="playSpaceCanvas">Reference to the canvas containing the blocks</param>
        public int WillCollideWall(ref Canvas playSpaceCanvas)
        {
            // Get block positions
            foreach (var block in _blockArray)
            {
                // Is left wall
                if (Canvas.GetLeft(block) == 0)
                    return 0;

                // Is right wall
                if (Canvas.GetLeft(block) == playSpaceCanvas.ActualWidth - 25)
                    return 1;
            }

            return 2;
        }

        /// <summary>
        /// Check collision status for bottom of canvas
        /// </summary>
        /// <param name="playSpaceCanvas">Current canvas</param>
        /// <returns>true/false</returns>
        public bool WillCollideBottom(ref Canvas playSpaceCanvas)
        {
            foreach (var block in _blockArray)
            {
                if (Canvas.GetTop(block) == playSpaceCanvas.ActualHeight - 25)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check collision status for below blocks
        /// </summary>
        /// <param name="placedBlocks">List of blocks that have been placed on the canvas</param>
        /// <returns>true/false</returns>
        public bool WillCollideBelowBlock(List<UIElement> placedBlocks)
        {
            foreach (var block in _blockArray)
            {
                foreach (var uiElement in placedBlocks)
                {
                    // Check for block collision for each block
                    if (Canvas.GetLeft(block) == Canvas.GetLeft(uiElement) && Canvas.GetTop(block) == Canvas.GetTop(uiElement) - 25)
                        return true;

                    //if (Canvas.GetLeft(block) == Canvas.GetLeft(uiElement) - 25 && Canvas.GetTop(block) == Canvas.GetTop(uiElement) - 25)
                    //    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check collision status for hitting the side of a block
        /// </summary>
        /// <param name="placedBlocks">List of blocks that have been placed on the canvas</param>
        /// <returns>true/false</returns>
        public int WillCollideSideBlock(List<UIElement> placedBlocks)
        {
            foreach (var block in _blockArray)
            {
                foreach (var uiElement in placedBlocks)
                {
                    // Check for block collision for each block

                    // Left Check
                    if (Canvas.GetLeft(block) == Canvas.GetLeft(uiElement) + 25 && Canvas.GetTop(block) == Canvas.GetTop(uiElement))
                        return 0;

                    // Right Check
                    if (Canvas.GetLeft(block) == Canvas.GetLeft(uiElement) - 25 && Canvas.GetTop(block) == Canvas.GetTop(uiElement))
                        return 1;
                }
            }

            return 2;
        }

        /// <summary>
        /// Setup for all rotation positions
        /// </summary>
        private void CreateRotationPositions()
        {
            //_iRotationDictionary
            _iRotationDictionary[0] = new int[] { -25, 25, -50, 50, -75, 75 };
            _iRotationDictionary[1] = new int[] { -25, -25, -50, -50, -75, -75};
            _iRotationDictionary[2] = new int[] { 25,-25, 50, -50, 75, -75 };
            _iRotationDictionary[3] = new int[] { 25, 25, 50, 50, 75, 75 };
            //_lRotationDictionary
            _lRotationDictionary[0] = new int[] { -25, 25, -50, 50, -25, 75 };
            _lRotationDictionary[1] = new int[] { -25, -25, -50, -50, -75, -25 };
            _lRotationDictionary[2] = new int[] { 25, -25, 50, -50, 25, -75 };
            _lRotationDictionary[3] = new int[] { 25, 25, 50, 50, 75, 25 };
            //_tRotationDictionary
            _tRotationDictionary[0] = new int[] { -25, 25, -50, 0, 0, 50 };
            _tRotationDictionary[1] = new int[] { -25, -25, 0, -50 ,-50, 0 };
            _tRotationDictionary[2] = new int[] { 25, -25, 50, 0, 0, -50 };
            _tRotationDictionary[3] = new int[] { 25, 25, 0, 50, 50, 0 };
            //_zRotationDictionary
            _zRotationDictionary[0] = new int[] { 25, 25, 0, 50, 25, 75 };
            _zRotationDictionary[1] = new int[] { -25, 25, -50, 0, -75, 25 };
            _zRotationDictionary[2] = new int[] { -25, -25, 0, -50, -25, -75 };
            _zRotationDictionary[3] = new int[] { 25, -25, 50, 0, 75, -25 };
            //_rZRotationDictionary
            _rZRotationDictionary[0] = new int[] { -25, 50, 0, 50, -25, 25 };
            _rZRotationDictionary[1] = new int[] { 25, -25, 0, -50, 25, -75 };
            _rZRotationDictionary[2] = new int[] { 25, 25, 0, 50, 25, 75 };
            _rZRotationDictionary[3] = new int[] { -25, 25, 0, 0, -25, 25 };
        }
    }
}
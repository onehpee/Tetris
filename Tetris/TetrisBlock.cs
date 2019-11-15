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
        private readonly BlockType _blockType;
        private readonly Color _blockColor;
        private readonly Rectangle[] _blockArray;

        public TetrisBlock(BlockType blockType, ref Canvas playSpaceCanvas)
        {
            _blockType = blockType;
            _blockColor = new Color();
            _blockArray = new Rectangle[4];

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

            DrawBlock(ref playSpaceCanvas);
        }

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

                    Canvas.SetLeft(_blockArray[1], 125);
                    Canvas.SetTop(_blockArray[1], 50);

                    Canvas.SetLeft(_blockArray[2], 100);
                    Canvas.SetTop(_blockArray[2], 50);

                    Canvas.SetLeft(_blockArray[3], 150);
                    Canvas.SetTop(_blockArray[3], 125);
                    break;
                case BlockType.Z:
                    break;
                case BlockType.ReverseZ:
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
            if (clockwise)
            {
                // When rotating clockwise
            }
            else
            {
                // When rotating counter clockwise
            }
        }

        /// <summary>
        /// Check to see if this block is colliding with walls or other blocks
        /// </summary>
        /// <param name="playSpaceCanvas">Reference to the canvas containing the blocks</param>
        public bool IsColliding(ref Canvas playSpaceCanvas)
        {
            return false;
        }
    }
}
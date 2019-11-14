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
            var block0 = new Rectangle { Width = 50, Height = 50, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua};
            var block1 = new Rectangle { Width = 50, Height = 50, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua };
            var block2 = new Rectangle { Width = 50, Height = 50, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua };
            var block3 = new Rectangle { Width = 50, Height = 50, Fill = new SolidColorBrush(_blockColor), StrokeThickness = 2, Stroke = Brushes.Aqua };

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
                    Canvas.SetLeft(_blockArray[0], 250);

                    Canvas.SetLeft(_blockArray[1], 250);
                    Canvas.SetTop(_blockArray[1], 50);

                    Canvas.SetLeft(_blockArray[2], 250);
                    Canvas.SetTop(_blockArray[2], 100);

                    Canvas.SetLeft(_blockArray[3], 250);
                    Canvas.SetTop(_blockArray[3], 150);
                    break;
                case BlockType.L:
                    Canvas.SetLeft(_blockArray[0], 250);

                    Canvas.SetLeft(_blockArray[1], 250);
                    Canvas.SetTop(_blockArray[1], 50);

                    Canvas.SetLeft(_blockArray[2], 250);
                    Canvas.SetTop(_blockArray[2], 100);

                    Canvas.SetLeft(_blockArray[3], 300);
                    Canvas.SetTop(_blockArray[3], 100);
                    break;
                case BlockType.T:
                    Canvas.SetLeft(_blockArray[0], 250);

                    Canvas.SetLeft(_blockArray[1], 250);
                    Canvas.SetTop(_blockArray[1], 50);

                    Canvas.SetLeft(_blockArray[2], 200);
                    Canvas.SetTop(_blockArray[2], 50);

                    Canvas.SetLeft(_blockArray[3], 300);
                    Canvas.SetTop(_blockArray[3], 50);
                    break;
                case BlockType.Cube:
                    Canvas.SetLeft(_blockArray[0], 250);

                    Canvas.SetLeft(_blockArray[1], 250);
                    Canvas.SetTop(_blockArray[1], 100);

                    Canvas.SetLeft(_blockArray[2], 200);
                    Canvas.SetTop(_blockArray[2], 100);

                    Canvas.SetLeft(_blockArray[3], 300);
                    Canvas.SetTop(_blockArray[3], 250);
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
            var xOffset = (x * 50);
            var yOffset = (y * 50);

            Canvas.SetLeft(_blockArray[0], Canvas.GetLeft(_blockArray[0]) + xOffset);

            Canvas.SetLeft(_blockArray[1], Canvas.GetLeft(_blockArray[0]) + xOffset);
            Canvas.SetTop(_blockArray[1], Canvas.GetLeft(_blockArray[0]) + yOffset);

            Canvas.SetLeft(_blockArray[2], Canvas.GetLeft(_blockArray[0]) + xOffset);
            Canvas.SetTop(_blockArray[2], Canvas.GetLeft(_blockArray[0]) + yOffset);

            Canvas.SetLeft(_blockArray[3], Canvas.GetLeft(_blockArray[0]) + xOffset);
            Canvas.SetTop(_blockArray[3], Canvas.GetLeft(_blockArray[0]) + yOffset);
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
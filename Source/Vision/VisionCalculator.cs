using System;
using System.Collections.Generic;
using System.Linq;
using Turnable.Places;
using Turnable.Utilities;

namespace Turnable.Vision
{
    public class VisionCalculator
    {
        // REF: http://www.roguebasin.com/index.php?title=Improved_Shadowcasting_in_Java
        public Rectangle Bounds { get; set; }

        private int[,] multipliers =
        {
            {1,  0,  0, -1, -1,  0,  0,  1},
            {0,  1, -1,  0,  0, -1,  1,  0},
            {0,  1,  1,  0,  0, -1, -1,  0},
            {1,  0,  0,  1, -1,  0,  0, -1},
        };

        public VisionCalculator(Rectangle bounds)
        {
            Bounds = bounds;
        }

        public double Slope(double x1, double y1, double x2, double y2, bool inverse = false)
        {
            if (!inverse)
            {
                return ((x1 - x2) / (y1 - y2));
            }
            else
            {
                return 1.0 / Slope(x1, y1, x2, y2);
            }
        }

        public double VisibleDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        public List<Position> CalculateFieldOfView(Position start, int visualRange, List<Position> obstacles)
        {
            List<Position> returnValue = new List<Position>();

            returnValue.Add(start);

            List<int> octantIndices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };

            foreach (int octantIndex in octantIndices)
            {
                CastLight(returnValue, start, 1, 1.0, 0.0, visualRange, multipliers[0, octantIndex], multipliers[1, octantIndex], multipliers[2, octantIndex], multipliers[3, octantIndex], obstacles);
            }

            return returnValue.Distinct<Position>().ToList<Position>();
        }

        private void CastLight(List<Position> visiblePositions, Position start, int row, double startSlope, double endSlope, int visualRange, int xx, int xy, int yx, int yy, List<Position> obstacles)
        {
            double newStartSlope = 0.0;

            if (startSlope < endSlope)
            {
                return;
            }

            bool blocked = false;
            int visualRangeSquared = visualRange * visualRange;

            for (int distance = row; distance <= visualRange && !blocked; distance++)
            {
                int deltaY = -distance;

                for (int deltaX = -distance; deltaX <= 0; deltaX++)
                {
                    int currentX = start.X + deltaX * xx + deltaY * xy;
                    int currentY = start.Y + deltaX * yx + deltaY * yy;
                    Position currentPosition = new Position(currentX, currentY);
                    float leftSlope = (deltaX - 0.5f) / (deltaY + 0.5f);
                    float rightSlope = (deltaX + 0.5f) / (deltaY - 0.5f);

                    if (!(currentX >= 0 && currentY >= 0 && currentX < Bounds.Width && currentY < Bounds.Height) || startSlope < rightSlope)
                    {
                        continue;
                    }
                    else if (endSlope > leftSlope)
                    {
                        break;
                    }

                    // Check if it's within the lightable area and light if needed
                    if ((deltaX * deltaX + deltaY * deltaY) <= visualRangeSquared)
                    {
                        visiblePositions.Add(currentPosition);
                    }

                    // Previous cell was a blocking one
                    if (blocked)
                    {
                        if (obstacles.Contains(currentPosition))
                        // Hit an obstacle
                        {
                            newStartSlope = rightSlope;
                            continue;
                        }
                        else
                        {
                            blocked = false;
                            startSlope = newStartSlope;
                        }
                    }
                    else
                    {
                        if (obstacles.Contains(currentPosition) && distance < visualRange)
                        // Hit an obstacle within sight line
                        {
                            blocked = true;
                            CastLight(visiblePositions, start, distance + 1, startSlope, leftSlope, visualRange, xx, xy, yx, yy, obstacles);
                            newStartSlope = rightSlope;
                        }
                    }
                }
            }

            return;
        }

    }
}
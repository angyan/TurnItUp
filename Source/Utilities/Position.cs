using System;

namespace Turnable.Utilities
{
    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position()
        {
        }

        public Position(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public Position Clone()
        {
            return new Position(X, Y);
        }

        public bool Equals(Position other)
        {
            if (other == null)
            {
                return false;
            }

            if (this.X == other.X && this.Y == other.Y)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(Object other)
        {
            if (other == null)
            {
                return false;
            }

            Position otherPosition = other as Position;
            if (otherPosition == null)
            {
                return false;
            }
            else
            {
                return Equals(otherPosition);
            }
        }

        public static bool operator ==(Position position1, Position position2)
        {
            if (((object)position1) == null || ((object)position2) == null)
            {
                return Object.Equals(position1, position2);
            }

            return position1.Equals(position2);
        }

        public static bool operator !=(Position position1, Position position2)
        {
            if (((object)position1) == null || ((object)position2) == null)
            {
                return !(Object.Equals(position1, position2));
            }

            return !(position1.Equals(position2));
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                // Suitable nullity checks etc, of course :)
                hash = hash * 16777619 ^ X.GetHashCode();
                hash = hash * 16777619 ^ Y.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return String.Format("Position {{X: {0}, Y: {1}}}", X, Y);
        }

        public Position NeighboringPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return new Position(X, Y + 1);
                case Direction.NorthEast:
                    return new Position(X + 1, Y + 1);
                case Direction.East:
                    return new Position(X + 1, Y);
                case Direction.SouthEast:
                    return new Position(X + 1, Y - 1);
                case Direction.South:
                    return new Position(X, Y - 1);
                case Direction.SouthWest:
                    return new Position(X - 1, Y - 1);
                case Direction.West:
                    return new Position(X - 1, Y);
                case Direction.NorthWest:
                    return new Position(X - 1, Y + 1);
                default:
                    return this;
            }
        }
    }
}

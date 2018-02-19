using System;
using System.Collections.Generic;
using Turnable.Utilities;

namespace Turnable.Pathfinding
{
    public class Node
    {
        public Node Parent { get; set; }
        public Position Position { get; private set; }
        public Rectangle LevelBounds { get; private set; }
        public const int OrthogonalMovementCost = 10;
        public const int DiagonalMovementCost = 14;

        public Node(Rectangle levelBounds, Position position, Node parent = null)
        {
            Position = position;
            Parent = parent;
            LevelBounds = levelBounds;
        }

        public int ActualMovementCost
        {
            get
            {
                if (Parent != null)
                {
                    if (Parent.Position.IsOrthogonalTo(Position))
                    {
                        return Parent.ActualMovementCost + OrthogonalMovementCost;
                    }
                    if (Parent.Position.IsDiagonalTo(Position))
                    {
                        return Parent.ActualMovementCost + DiagonalMovementCost;
                    }
                }

                return 0;
            }
        }

        public bool IsWithinBounds()
        {
            return LevelBounds.Contains(Position);
        }

        public List<Node> AdjacentNodes(bool diagonalMovementAllowed = true)
        {
            List<Node> returnValue = new List<Node>();

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                // Don't add diagonal nodes if diagonal movement is not allowed.
                if (!diagonalMovementAllowed)
                {
                    if (direction != Direction.NorthEast && direction != Direction.NorthWest && direction != Direction.SouthEast && direction != Direction.SouthWest)
                    {
                        returnValue.Add(new Node(LevelBounds, Position.NeighboringPosition(direction)));
                    }
                }
                else
                {
                    returnValue.Add(new Node(LevelBounds, Position.NeighboringPosition(direction)));
                }
            }

            // Remove all nodes that are out of bounds
            returnValue.RemoveAll(n => !n.IsWithinBounds());

            return returnValue;
        }

        public int EstimatedMovementCost(Node node)
        {
            // Manhattan distance = (Sum of the horizontal and vertical distance) * OrthogonalMovementCost
            return (Math.Abs(Position.X - node.Position.X) + Math.Abs(Position.Y - node.Position.Y)) * Node.OrthogonalMovementCost;
        }

        public bool Equals(Node other)
        {
            if (other == null)
            {
                return false;
            }

            return (this.Position == other.Position);
        }

        public override bool Equals(Object other)
        {
            Node otherNode = other as Node;

            if (otherNode == null)
            {
                return false;
            }
            else
            {
                return Equals(otherNode);
            }
        }

        public static bool operator ==(Node node1, Node node2)
        {
            if ((object)node1 == null || ((object)node2) == null)
            {
                return Object.Equals(node1, node2);
            }

            return node1.Equals(node2);
        }

        public static bool operator !=(Node node1, Node node2)
        {
            if ((object)node1 == null || ((object)node2) == null)
            {
                return !Object.Equals(node1, node2);
            }

            return !(node1.Equals(node2));
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}
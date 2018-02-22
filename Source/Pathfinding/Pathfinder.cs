using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Turnable.Utilities;

namespace Turnable.Pathfinding
{
    public class Pathfinder
    {
        public bool AllowDiagonalMovement { get; set; }
        public Rectangle Bounds { get; set; }

        public Pathfinder(Rectangle bounds, bool allowDiagonalMovement = true)
        {
            Bounds = bounds;
            AllowDiagonalMovement = allowDiagonalMovement;
        }

        public List<Position> FindPath(Position start, Position end, IEnumerable<Position> obstacles)
        {
            // If the end is an obstacle, there is no path that can be found. 
            // This is an exceptional situation, since most games should show the user via the UI that they cannot walk to an obstacle.
            if (obstacles.Contains<Position>(end))
            {
                throw new InvalidOperationException();
            }

            var openNodes = new NodeList();
            var closedNodes = new NodeList();
            Node node;
            Node currentNode;
            var shortestPathFound = false;
            var actualMovementCost = 0;
            var returnValue = new List<Position>();
            var startingNode = new Node(Bounds, start);
            var endingNode = new Node(Bounds, end);

            openNodes.Add(startingNode);

            while (!shortestPathFound)
            {
                // If a path to the end has not yet been found AND there are no openNodes, there is no feasible path to the end
                if (openNodes.Count == 0)
                {
                    return null;
                }

                currentNode = openNodes[0];

                openNodes.Remove(currentNode);
                closedNodes.Add(currentNode);

                if (currentNode == endingNode)
                {
                    // Stop when end node has been added to the closed list, in which case the shortest path has been found
                    shortestPathFound = true;
                    break;
                }

                foreach (Node adjacentNode in currentNode.AdjacentNodes(AllowDiagonalMovement))
                {
                    // If it is not walkable or if it is on the closed list, ignore it.
                    if (closedNodes.Find(x => x == adjacentNode) != null || obstacles.Contains<Position>(adjacentNode.Position))
                    {
                        continue;
                    }

                    node = openNodes.Find(x => x == adjacentNode);
                    // If it isn’t on the open list, add it to the open list. Make the current node the parent of this node. Calculate the EstimatedMovementCost of the node.
                    if (node == null)
                    {
                        node = new Node(Bounds, adjacentNode.Position, currentNode);
                        node.CalculateEstimatedMovementCost(endingNode);
                        openNodes.Add(node);
                    }
                    else
                    {
                        //If it is on the open list already, check to see if this path to that node is better, using ActualMovementCost as the measure. 
                        actualMovementCost = node.ActualMovementCost - node.Parent.ActualMovementCost + currentNode.ActualMovementCost;
                        if (adjacentNode.Position.IsOrthogonalTo(currentNode.Position))
                        {
                            actualMovementCost += Node.OrthogonalMovementCost;
                        }
                        else  // Nodes diagonal to each other
                        {
                            actualMovementCost += Node.DiagonalMovementCost;
                        }
                        // A lower ActualMovementCost means that this is a better path. If so, change the parent of the node to the current node. The ActualMovementCost and PathScore will be automatically recalculated when the parent is changed. Our NodeList automatically sorts nodes by PathScore, so we don't have to do any manual resorting.
                        if (actualMovementCost < node.ActualMovementCost)
                        {
                            node.Parent = currentNode;
                        }
                    }
                    //Stop when you fail to find the target node, and the open list is empty. In this case, there is no path.
                }
            }

            if (shortestPathFound)
            {
                // Save the path: Working backwards from the target node, go from each node to its parent node. This is the shortest path.
                node = closedNodes.Find(x => x == endingNode);

                returnValue.Add(node.Position);

                while (node.Parent != null)
                {
                    node = node.Parent;
                    returnValue.Add(node.Position);
                }

                returnValue.Reverse();
            }
            else
            {
                returnValue = null;
            }
            
            return returnValue;
        }
    }
}
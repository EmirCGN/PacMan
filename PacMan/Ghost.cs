using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PacMan
{
    class Ghost : Entity
    {
        private List<Node> path;
        private int currentPathIndex;

        public Ghost(int x, int y) : base(x, y, 'G')
        {
            path = new List<Node>();
            currentPathIndex = 0;
        }

        public void MoveTowardsPacMan(PacMan pacMan, char[,] gameBoard)
        {
            int targetX = pacMan.X;
            int targetY = pacMan.Y;

            if (X == targetX && Y == targetY)
            {
                // If the ghost is already at the target position, no need to calculate a new path
                return;
            }

            // Use A* algorithm to find the optimal path to the target
            Node startNode = new Node(X, Y);
            Node targetNode = new Node(targetX, targetY);
            List<Node> openList = new List<Node>();
            List<Node> closedList = new List<Node>();

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                Node currentNode = openList[0];

                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].TotalCost < currentNode.TotalCost || (openList[i].TotalCost == currentNode.TotalCost && openList[i].HeuristicCost < currentNode.HeuristicCost))
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode.X == targetNode.X && currentNode.Y == targetNode.Y)
                {
                    // Found the target, reconstruct the path
                    path.Clear();
                    Node node = currentNode;

                    while (node != null)
                    {
                        path.Insert(0, node);
                        node = node.Parent;
                    }

                    break;
                }

                List<Node> neighbors = GetNeighborNodes(currentNode, gameBoard);

                foreach (Node neighbor in neighbors)
                {
                    if (closedList.Contains(neighbor))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbor = currentNode.MovementCost + 1;

                    if (newMovementCostToNeighbor < neighbor.MovementCost || !openList.Contains(neighbor))
                    {
                        neighbor.MovementCost = newMovementCostToNeighbor;
                        neighbor.HeuristicCost = CalculateHeuristicCost(neighbor, targetNode);
                        neighbor.Parent = currentNode;

                        if (!openList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                        }
                    }
                }
            }

            if (path.Count > 0)
            {
                Node nextNode = path[currentPathIndex];

                if (nextNode.X == X && nextNode.Y == Y)
                {
                    // Move to the next node in the path
                    currentPathIndex++;
                    currentPathIndex %= path.Count;
                    nextNode = path[currentPathIndex];
                }

                MoveEntity(this, nextNode.X, nextNode.Y);
            }
        }

        private List<Node> GetNeighborNodes(Node node, char[,] gameBoard)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            List<Node> neighbors = new List<Node>();

            for (int i = 0; i < 4; i++)
            {
                int newX = node.X + dx[i];
                int newY = node.Y + dy[i];

                if (CanMoveTo(newX, newY, gameBoard))
                {
                    neighbors.Add(new Node(newX, newY));
                }
            }

            return neighbors;
        }

        private bool CanMoveTo(int x, int y, char[,] gameBoard)
        {
            int height = gameBoard.GetLength(0);
            int width = gameBoard.GetLength(1);

            return x >= 0 && x < width && y >= 0 && y < height && gameBoard[y, x] != '#';
        }

        private int CalculateHeuristicCost(Node node, Node targetNode)
        {
            return Math.Abs(node.X - targetNode.X) + Math.Abs(node.Y - targetNode.Y);
        }
    }
}

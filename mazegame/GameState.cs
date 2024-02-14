using System.Collections.Generic;
using mazegame;
using MazeGenerator;

namespace mazegame
{
    public class GameState
    {
        public GameState(int size)
        {
            maze = new Maze(size, size);
            shortestPath = new Stack<Cell>(maze.FindPath(0, 0, size - 1, size - 1));
        }

        private void MovePlayer(int x, int y)
        {
            Cell currentCell = maze.GetCell(playerX, playerY);
            if (currentCell.RightNeighbor != null && x > 0)
            {
                playerX++;
            }
            else if (currentCell.LeftNeighbor != null && x < 0)
            {
                playerX--;
            }
            else if (currentCell.TopNeighbor != null && y < 0)
            {
                playerY--;
            }
            else if (currentCell.BottomNeighbor != null && y > 0)
            {
                playerY++;
            }

            if (shortestPath.Count > 0 && currentCell == shortestPath.Peek())
            {
                shortestPath.Pop();
            }
            else
            {
                shortestPath.Push(currentCell);
            }
        }

        public List<Cell> breadcrumbs
        {
            get
            {
                return breadcrumbs;
            }
        }
        public bool showBreadcrumbs = false;
        public readonly Stack<Cell> shortestPath = new Stack<Cell>();
        public bool showShortestPath = true;

        public bool showSingleHint = false;

        private Maze maze { get; set; }

        public Maze getMaze()
        {
            return maze;
        }
        private int playerX = 0;

        public int getPlayerX()
        {
            return playerX;
        }
        private int playerY = 0;

        public int getPlayerY()
        {
            return playerY;
        }
    }
}
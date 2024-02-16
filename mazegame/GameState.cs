using System.Collections.Generic;
using mazegame;
using MazeGenerator;
using Microsoft.Xna.Framework.Graphics;

namespace mazegame
{
    public class GameState
    {
        public GameState(int size)
        {
            maze = new Maze(size, size);
            shortestPath = new Stack<Cell>(maze.FindPath(size - 1, size - 1, 0, 0));
            // pop the starting cell, we're already there
            shortestPath.Pop();
        }

        public void MovePlayer(int x, int y)
        {
            Cell currentCell = maze.GetCell(playerX, playerY);
            bool moved = false;
            if (currentCell.RightNeighbor != null && x > 0)
            {
                playerX++;
                moved = true;
            }
            else if (currentCell.LeftNeighbor != null && x < 0)
            {
                playerX--;
                moved = true;
            }
            else if (currentCell.TopNeighbor != null && y < 0)
            {
                playerY--;
                moved = true;
            }
            else if (currentCell.BottomNeighbor != null && y > 0)
            {
                playerY++;
                moved = true;
            }
            Cell newCell = maze.GetCell(playerX, playerY);

            // case: if we moved on the shortest path, pop the stack
            if (shortestPath.Count > 0 && newCell == shortestPath.Peek())
            {
                shortestPath.Pop();
            }
            // case: if we moved away from the shortest path, add to the stack
            else if (moved)
            {
                shortestPath.Push(currentCell);
            }

            if (moved && !breadcrumbs.Contains(newCell))
            {
                breadcrumbs.Add(currentCell);
            }
        }

        public readonly List<Cell> breadcrumbs = new List<Cell>();
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
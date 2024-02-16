using System.Collections.Generic;
using MazeGenerator;

namespace mazegame
{
    public struct LeaderboardEntry
    {
        public int size;
        public int score;
    }
    public class GameState
    {
        public GameState(int size)
        {
            maze = new Maze(size, size);
            shortestPath = new Stack<Cell>(maze.FindPath(size - 1, size - 1, 0, 0));
            // pop the starting cell, we're already there
            shortestPath.Pop();
        }

        public void reset(int size)
        {
            maze = new Maze(size, size);
            shortestPath = new Stack<Cell>(maze.FindPath(size - 1, size - 1, 0, 0));
            // pop the starting cell, we're already there
            shortestPath.Pop();
            breadcrumbs.Clear();
            playerX = 0;
            playerY = 0;
            score = 0;
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
                // if we haven't been here before, award points
                if (!breadcrumbs.Contains(newCell))
                {
                    score += 5;
                }
                shortestPath.Pop();
            }
            // case: if we moved away from the shortest path, add to the stack
            else if (moved)
            {
                // penalize the player for moving away from the shortest path
                if (!breadcrumbs.Contains(currentCell))
                {
                    score -= 1;
                }
                shortestPath.Push(currentCell);
            }
            // case: if we moved and we haven't been here before, add to breadcrumbs
            if (moved && !breadcrumbs.Contains(newCell))
            {
                breadcrumbs.Add(currentCell);
            }

            // win condition
            if (playerX == maze.width - 1 && playerY == maze.height - 1)
            {
                leaderboard.Add(new LeaderboardEntry { size = maze.width, score = score });
                reset(maze.width);
            }
        }

        public readonly List<Cell> breadcrumbs = new List<Cell>();
        public bool showBreadcrumbs = false;
        public int score = 0;
        public Stack<Cell> shortestPath = new Stack<Cell>();
        public bool showShortestPath = false;
        public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>();
        public bool showSingleHint = false;
        public bool showLeaderboards = false;
        public bool showCredits = false;
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
using System;
using System.IO;

namespace MazeGenerator
{
    public class Maze
    {
        private Cell[,] grid;
        private List<Wall> walls = new List<Wall>();
        public Maze(int width, int height)
        {
            grid = new Cell[width, height];
            InitializeCells();
            InitializeWalls();

            // Shuffle the walls
            Random random = new Random();
            for (int i = 0; i < walls.Count; i++)
            {
                int j = random.Next(i, walls.Count);
                Wall temp = walls[i];
                walls[i] = walls[j];
                walls[j] = temp;
            }

            foreach (Wall wall in walls)
            {
                if (wall.Cell1 != null && wall.Cell2 != null)
                {
                    if (wall.Cell1.Id != wall.Cell2.Id)
                    {
                        int newId = wall.Cell1.Id;
                        SetNeighborsId(wall.Cell2.X, wall.Cell2.Y, newId);
                        BreakWall(wall.Cell1, wall.Cell2);
                    }
                }
            }
        }
        private void SetNeighborsId(int x, int y, int newId)
        {
            Cell? cell = GetCell(x, y);
            if (cell != null && cell.Id != newId)
            {
                cell.Id = newId;
                if (cell.LeftNeighbor != null) SetNeighborsId(x - 1, y, newId);
                if (cell.RightNeighbor != null) SetNeighborsId(x + 1, y, newId);
                if (cell.TopNeighbor != null) SetNeighborsId(x, y - 1, newId);
                if (cell.BottomNeighbor != null) SetNeighborsId(x, y + 1, newId);
            }
        }

        private void BreakWall(Cell cell1, Cell cell2)
        {
            if (cell1.X == cell2.X)
            {
                if (cell1.Y < cell2.Y)
                {
                    cell1.BottomNeighbor = cell2;
                    cell2.TopNeighbor = cell1;
                }
                else
                {
                    cell1.TopNeighbor = cell2;
                    cell2.BottomNeighbor = cell1;
                }
            }
            else
            {
                if (cell1.X < cell2.X)
                {
                    cell1.RightNeighbor = cell2;
                    cell2.LeftNeighbor = cell1;
                }
                else
                {
                    cell1.LeftNeighbor = cell2;
                    cell2.RightNeighbor = cell1;
                }
            }
        }

        private void InitializeWalls()
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    // Check the right wall
                    if (x < grid.GetLength(0) - 1)
                    {
                        walls.Add(new Wall(grid[x, y], grid[x + 1, y]));
                    }

                    // Check the bottom wall
                    if (y < grid.GetLength(1) - 1)
                    {
                        walls.Add(new Wall(grid[x, y], grid[x, y + 1]));
                    }
                }
            }
        }
        private void InitializeCells()
        {
            int id = 0;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = new Cell(id++, x, y);
                }
            }
        }

        public Cell? GetCell(int x, int y)
        {
            if (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
            {
                return grid[x, y];
            }
            else
            {
                return null;
            }
        }
        public void PrintMaze()
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                // Print the north edge
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Console.Write("+");
                    Console.Write(grid[x, y].TopNeighbor == null ? "---" : "   ");
                }
                Console.WriteLine("+");

                // Print the west edge and the cell
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Console.Write(grid[x, y].LeftNeighbor == null ? "|" : " ");
                    Console.Write("   ");
                }
                Console.WriteLine("|");
            }

            // Print the south edge of the last row
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                Console.Write("+---");
            }
            Console.WriteLine("+");
        }

    }

    public class Cell(int id, int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public Cell? TopNeighbor { get; set; } = null;
        public Cell? RightNeighbor { get; set; } = null;
        public Cell? BottomNeighbor { get; set; } = null;
        public Cell? LeftNeighbor { get; set; } = null;
        public int Id { get; set; } = id;
    }

    public class Wall(Cell cell1, Cell cell2)
    {
        public Cell? Cell1 { get; set; } = cell1;
        public Cell? Cell2 { get; set; } = cell2;
    }
}

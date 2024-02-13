using System;
using System.IO;

namespace MazeGenerator
{
    public class Maze
    {
        public readonly int width;
        public readonly int height;
        private Cell[,] grid;
        private List<Wall> walls = new List<Wall>();
        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
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
        public List<Cell> FindPath(int startX, int startY, int endX, int endY)
        {
            List<Cell> path = new List<Cell>();
            Cell? startCell = GetCell(startX, startY);
            Cell? endCell = GetCell(endX, endY);

            if (startCell == null || endCell == null)
            {
                return path;
            }

            Queue<Cell> queue = new Queue<Cell>();
            Dictionary<Cell, Cell?> parentMap = new Dictionary<Cell, Cell?>();

            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                Cell currentCell = queue.Dequeue();

                if (currentCell == endCell)
                {
                    // Found the end cell, reconstruct the path
                    Cell? pathCell = currentCell;
                    if (parentMap.ContainsKey(startCell))
                    {
                        parentMap[startCell] = null;
                    }
                    while (pathCell != null)
                    {
                        path.Insert(0, pathCell);
                        pathCell = parentMap.ContainsKey(pathCell) ? parentMap[pathCell] : null;
                    }
                    return path;
                }

                // Explore the neighbors
                if (currentCell.LeftNeighbor != null && !parentMap.ContainsKey(currentCell.LeftNeighbor))
                {
                    queue.Enqueue(currentCell.LeftNeighbor);
                    parentMap[currentCell.LeftNeighbor] = currentCell;
                }
                if (currentCell.RightNeighbor != null && !parentMap.ContainsKey(currentCell.RightNeighbor))
                {
                    queue.Enqueue(currentCell.RightNeighbor);
                    parentMap[currentCell.RightNeighbor] = currentCell;
                }
                if (currentCell.TopNeighbor != null && !parentMap.ContainsKey(currentCell.TopNeighbor))
                {
                    queue.Enqueue(currentCell.TopNeighbor);
                    parentMap[currentCell.TopNeighbor] = currentCell;
                }
                if (currentCell.BottomNeighbor != null && !parentMap.ContainsKey(currentCell.BottomNeighbor))
                {
                    queue.Enqueue(currentCell.BottomNeighbor);
                    parentMap[currentCell.BottomNeighbor] = currentCell;
                }
            }
            System.Console.WriteLine("No path found!");
            return path;
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

    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Cell? TopNeighbor { get; set; }
        public Cell? RightNeighbor { get; set; }
        public Cell? BottomNeighbor { get; set; }
        public Cell? LeftNeighbor { get; set; }
        public int Id { get; set; }

        public Cell(int id, int x, int y)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public override string ToString()
        {
            return $"Cell ({X}, {Y})";
        }
    }

    public class Wall
    {
        public Wall(Cell cell1, Cell cell2)
        {
            Cell1 = cell1;
            Cell2 = cell2;
        }
        public Cell? Cell1 { get; init; }
        public Cell? Cell2 { get; init; }
    }
}

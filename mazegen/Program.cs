using MazeGenerator;

Maze maze = new Maze(20, 6);

maze.PrintMaze();

List<Cell> path = maze.FindPath(0, 0, 19, 5);

foreach (Cell cell in path)
{
    Console.WriteLine("path:" + cell);
}
Console.WriteLine("Done!");
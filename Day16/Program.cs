using System;
using System.Collections.Generic;

class Program
{
    static int[] dx = { 0, 1, 0, -1 }; // Directions for moving (right, down, left, up)
    static int[] dy = { 1, 0, -1, 0 };

    static void Main()
    {
        char[,] maze = new char[15, 15]
        {
 {'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'},
{'#', '.', '.', '.', '.', '.', '.', '.', '#', '.', '.', '.', '.', 'E', '#'},
{'#', '.', '#', '.', '#', '#', '#', '.', '#', '.', '#', '#', '#', '.', '#'},
{'#', '.', '.', '.', '.', '.', '#', '.', '#', '.', '.', '.', '#', '.', '#'},
{'#', '.', '#', '#', '#', '.', '#', '#', '#', '#', '#', '.', '#', '.', '#'},
{'#', '.', '#', '.', '#', '.', '.', '.', '.', '.', '.', '.', '#', '.', '#'},
{'#', '.', '#', '.', '#', '#', '#', '#', '#', '.', '#', '#', '#', '.', '#'},
{'#', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '#', '.', '#'},
{'#', '#', '#', '.', '#', '.', '#', '#', '#', '#', '#', '.', '#', '.', '#'},
{'#', '.', '.', '.', '#', '.', '.', '.', '.', '.', '#', '.', '#', '.', '#'},
{'#', '.', '#', '.', '#', '.', '#', '#', '#', '.', '#', '.', '#', '.', '#'},
{'#', '.', '.', '.', '.', '.', '#', '.', '.', '.', '#', '.', '#', '.', '#'},
{'#', '.', '#', '#', '#', '.', '#', '.', '#', '.', '#', '.', '#', '.', '#'},
{'#', 'S', '.', '.', '#', '.', '.', '.', '.', '.', '#', '.', '.', '.', '#'},
{'#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#'}
        };

        // Find the start and end points
        int startX = -1, startY = -1, endX = -1, endY = -1;

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j] == 'S')
                {
                    startX = i;
                    startY = j;
                }
                if (maze[i, j] == 'E')
                {
                    endX = i;
                    endY = j;
                }
            }
        }

        // Start DFS to find the path
        List<Tuple<int, int>> path = new List<Tuple<int, int>>();
        if (DFS(maze, startX, startY, endX, endY, path))
        {
            // Mark the path with 'X'
            foreach (var point in path)
            {
                if (maze[point.Item1, point.Item2] != 'S' && maze[point.Item1, point.Item2] != 'E')
                {
                    maze[point.Item1, point.Item2] = 'X';
                }
            }

            // Print the maze with the path marked
            Console.WriteLine("Maze with the shortest path:");
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No path found from S to E.");
        }
    }

    static bool DFS(char[,] maze, int x, int y, int endX, int endY, List<Tuple<int, int>> path)
    {
        // Check if the current position is out of bounds or a wall
        if (x < 0 || x >= maze.GetLength(0) || y < 0 || y >= maze.GetLength(1) || maze[x, y] == '#' || maze[x, y] == 'X')
        {
            return false;
        }

        // Add current position to the path
        path.Add(Tuple.Create(x, y));

        // If we reach the end point, return true
        if (x == endX && y == endY)
        {
            return true;
        }

        // Mark the current cell as visited
        maze[x, y] = 'X';

        // Explore all 4 possible directions (up, right, down, left)
        foreach (var direction in new[] { (0, 1), (1, 0), (0, -1), (-1, 0) })
        {
            int newX = x + direction.Item1;
            int newY = y + direction.Item2;

            // Recur in the next direction
            if (DFS(maze, newX, newY, endX, endY, path))
            {
                return true;
            }
        }

        // If no path is found, backtrack: remove the current position
        path.RemoveAt(path.Count - 1);

        return false; // No path found
    }
}

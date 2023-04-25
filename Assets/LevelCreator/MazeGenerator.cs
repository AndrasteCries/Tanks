using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int x;
    public int y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool Visited = false;

    private Buff gangawe;
    public bool haveBuff() 
    {
        if (gangawe == null)
        {
            return false;
        }
        else return true;
    }

    public void spawnBuff(Buff buff)
    {
        if (buff == null) { Debug.LogError("Buff prebuf is null"); }
        else
        {
            Vector2 spawnBonus = new Vector2(x * 2 - 1, y * 2 - 1);
            float randomZAngle = Random.Range(0, 360);
            Quaternion spawnRotation = Quaternion.Euler(0f, 0f, randomZAngle);
            gangawe = MonoBehaviour.Instantiate(buff, spawnBonus, spawnRotation).GetComponent<Buff>();
            Debug.Log("Bonus spawned coordinate = " + gangawe.transform.position);
        }
    }
}

public class MazeGenerator
{
    public int _width = 3;
    public int _height = 3;

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[_width, _height];

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = new MazeGeneratorCell { x = i, y = j };
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, _height - 1].WallLeft = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[_width - 1, y].WallBottom = false;
        }
        RemoveWallsWithBacktracker(maze);
        

        return maze;
    }
    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.x;
            int y = current.y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < _width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < _height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.x == b.x)
        {
            if (a.y > b.y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.x > b.x) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }
}

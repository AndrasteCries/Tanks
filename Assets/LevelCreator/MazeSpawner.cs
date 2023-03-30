using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(i * 2, j * 2), Quaternion.identity).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[i, j].WallLeft);
                c.WallBottom.SetActive(maze[i, j].WallBottom);
            }
        }
    }
}

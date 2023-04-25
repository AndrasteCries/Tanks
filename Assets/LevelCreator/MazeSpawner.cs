using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public float BonusSpawnTime = 2f;
    public ShotGun ShotGunBonus;

    MazeGeneratorCell[,] maze;
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(i * 2, j * 2), Quaternion.identity).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[i, j].WallLeft);
                c.WallBottom.SetActive(maze[i, j].WallBottom);
            }
        }

        InvokeRepeating("SpawnBonus", BonusSpawnTime, BonusSpawnTime);

    }
    void SpawnBonus()
    {
        int randomCellX = Random.Range(1, maze.GetLength(0));
        int randomCellY = Random.Range(1, maze.GetLength(1));
        if (!maze[randomCellX, randomCellY].haveBuff())
        {
            if (ShotGunBonus == null)
            {
                Debug.Log("PIDOR");
            }
            maze[randomCellX, randomCellY].spawnBuff(ShotGunBonus);
        }
    }
}

 
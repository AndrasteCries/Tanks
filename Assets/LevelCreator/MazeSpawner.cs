using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public float BonusSpawnTime = 2f;
    public ShotGunBuff ShotGunPrefab;
    public LaserBuff LaserPrefab;
    public SpikeBallBuff SpikeBallPrefab;
    public RocketBuff RocketPrefab;

    MazeGeneratorCell[,] maze;
    void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        maze = generator.GenerateMaze();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                if(CellPrefab == null) Debug.Log("CELLLL PREFAB IS NUULLL"); 
                Cell c = Instantiate(CellPrefab, new Vector2(i * 2, j * 2), Quaternion.identity).GetComponent<Cell>();
                c.WallLeft.SetActive(maze[i, j].WallLeft);
                c.WallBottom.SetActive(maze[i, j].WallBottom);
            }
        }

        //InvokeRepeating("SpawnBonus", BonusSpawnTime, BonusSpawnTime);

    }
    void SpawnBonus()
    {
        int randomCellX = Random.Range(1, maze.GetLength(0));
        int randomCellY = Random.Range(1, maze.GetLength(1));
        if (!maze[randomCellX, randomCellY].haveBuff())
        {
            if (ShotGunPrefab == null)
            {
                Debug.LogError("Prefab is null");
            }
            switch(Random.Range(1, 5))
            {
                case 1:
                    maze[randomCellX, randomCellY].spawnBuff(ShotGunPrefab);
                    break;
                case 2:
                    maze[randomCellX, randomCellY].spawnBuff(LaserPrefab);
                    break;
                case 3:
                    maze[randomCellX, randomCellY].spawnBuff(SpikeBallPrefab);
                    break;
                case 4:
                    maze[randomCellX, randomCellY].spawnBuff(RocketPrefab);
                    break;
                default:
                    Debug.LogError("PrefabErrorSpawn");
                    break;
            }
        }
    }
}

 
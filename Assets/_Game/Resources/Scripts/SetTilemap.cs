using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap tilemap;
    public Transform player;

    public TileBase[] tiles;

    public int ChunkSize = 10;

    HashSet<Vector2Int> generatedChunks = new HashSet<Vector2Int>();
    public int NumOfChunk = 1;

    public GameObject[] prefabs;
    public int maxObstacle = 8;
    public int minObstacle = 5;
    HashSet<GameObject> generatedObstacle = new HashSet<GameObject>();
    public float checkRadius = 1.0f;

    void Start()
    {
        tiles = Resources.LoadAll<TileBase>("TileMap");
        prefabs = Resources.LoadAll<GameObject>("Prefab/Obstacle");

    }
    void Update()
    {
        GenerateChunk(player.position);
        RemoveFarChunk(player.position);
    }

    Vector2Int CheckCurrentChunk(Vector3 Pos) 
    {
        int currentChunkX = Mathf.FloorToInt(Pos.x / ChunkSize);
        int currentChunkY = Mathf.FloorToInt(Pos.y / ChunkSize);

        Vector2Int chunkPos = new Vector2Int(currentChunkX, currentChunkY);

        return chunkPos;
    }
    Vector3Int CheckCurrentPos(Vector2Int pos)
    {
        int CurrentPosX = pos.x * ChunkSize;
        int CurrentPosY = pos.y * ChunkSize;

        Vector3Int CurrentPos = new Vector3Int(CurrentPosX, CurrentPosY);

        return CurrentPos;
    }

    void GenerateChunk(Vector3 pos)
    {
        Vector2Int chunkPos = CheckCurrentChunk(pos);

        for (int x = -NumOfChunk; x <= NumOfChunk; x++)
        {
            for (int y = -NumOfChunk; y <= NumOfChunk; y++)
            {
                Vector2Int aroundChunk = new Vector2Int(chunkPos.x + x, chunkPos.y + y);
                if (!generatedChunks.Contains(aroundChunk))
                {
                    GenerateChunkTile(aroundChunk);
                    GenerateObstacle(aroundChunk);
                    generatedChunks.Add(aroundChunk);
                }
            }
        }
    }
    void GenerateChunkTile(Vector2Int chunkPos)
    {
        Vector3Int currentPos = CheckCurrentPos(chunkPos);

        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                TileBase tile = tiles[Random.Range(0, tiles.Length)];

                tilemap.SetTile(
                    new Vector3Int(currentPos.x + x, currentPos.y + y, 0),
                    tile
                );
            }
        }
    }

    void RemoveFarChunk(Vector3 currentPos)
    {
        Vector2Int chunkPos = CheckCurrentChunk(currentPos);
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (Vector2Int chunk in generatedChunks) 
        {
            int dx = Mathf.Abs(chunk.x - chunkPos.x);
            int dy = Mathf.Abs(chunk.y - chunkPos.y);

            if (dx > NumOfChunk || dy > NumOfChunk)
            {
                chunksToRemove.Add(chunk);
            }
        }

        foreach (Vector2Int chunk in chunksToRemove)
        {
            RemoveObstacle(chunk);
            RemoveChunk(chunk);
        }



    }
    void RemoveChunk(Vector2Int chunkPos)
    {
        Vector3Int currentPos = CheckCurrentPos(chunkPos);

        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                tilemap.SetTile(
                    new Vector3Int(currentPos.x + x, currentPos.y + y, 0),
                    null
                );
            }
        }
        generatedChunks.Remove(chunkPos);
    }

    void GenerateObstacle(Vector2Int chunkPos)
    {
        int startX = chunkPos.x * ChunkSize;
        int startY = chunkPos.y * ChunkSize;
        int num = Random.Range(minObstacle, maxObstacle+1);

        // ·£´ý ÇÁ¸®ÆÕ ¼±ÅÃ
        for (int i = 0; i < num; i++)
        {
            GameObject randomPrefab =
                prefabs[Random.Range(0, prefabs.Length)];

            // ·£´ý À§Ä¡ »ý¼º
            Vector3 randomPos = new Vector3(
                Random.Range(startX, startX + ChunkSize),
                Random.Range(startY, startY + ChunkSize),
                0
            );

            Collider2D hit = Physics2D.OverlapCircle(
           randomPos,
           checkRadius
       );

            // ÇÁ¸®ÆÕ »ý¼º
            if (hit == null)
            {
                GameObject obstacle = Instantiate(
                    randomPrefab,
                    randomPos,
                    Quaternion.identity
                );

                generatedObstacle.Add(obstacle);
            }
        }
    }
    void RemoveObstacle(Vector2Int chunkPos)
    {
        List<GameObject> removeObstacle = new List<GameObject>();

        foreach(GameObject obstacle in generatedObstacle)
        {
            int removeObstacleX = Mathf.FloorToInt(obstacle.transform.position.x / ChunkSize);
            int removeObstacleY = Mathf.FloorToInt(obstacle.transform.position.y / ChunkSize);

            if(removeObstacleX == chunkPos.x && removeObstacleY == chunkPos.y)
            {
                removeObstacle.Add( obstacle );
            }
        }

        foreach(GameObject obstacle in removeObstacle)
        {
            generatedObstacle.Remove(obstacle);
            Destroy(obstacle);
        }
    }
}

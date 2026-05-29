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
        int currentChunkX = Mathf.FloorToInt(player.position.x / ChunkSize);
        int currentChunkY = Mathf.FloorToInt(player.position.y / ChunkSize);

        for (int x = -NumOfChunk; x <= NumOfChunk; x++)
        {
            for(int y = -NumOfChunk; y <= NumOfChunk; y++)
            {
                Vector2Int chunkPos = new Vector2Int(
                currentChunkX + x,  
                currentChunkY + y
                );


                if (!generatedChunks.Contains(chunkPos))
                {
                    GenerateChunk(chunkPos);
                    GenerateObstacle(chunkPos);
                    generatedChunks.Add(chunkPos);
                }
            }
        }

        RemoveFarChunk(currentChunkX, currentChunkY);
        OnDrawGizmos();
    }
    void GenerateChunk(Vector2Int chunkPos)
    {
        int startX = chunkPos.x * ChunkSize;
        int startY = chunkPos.y * ChunkSize;

        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                TileBase tile = tiles[Random.Range(0, tiles.Length)];

                tilemap.SetTile(
                    new Vector3Int(startX + x, startY + y, 0),
                    tile
                );
            }
        }
    }

    void RemoveFarChunk(int currentChunkX, int currentChunkY)
    {
        List<Vector2Int> chunksToRemove = new List<Vector2Int>();

        foreach (Vector2Int chunk in generatedChunks) 
        {
            int dx = Mathf.Abs(chunk.x - currentChunkX);
            int dy = Mathf.Abs(chunk.y - currentChunkY);

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
        int startX = chunkPos.x * ChunkSize;
        int startY = chunkPos.y * ChunkSize;

        for (int x = 0; x < ChunkSize; x++)
        {
            for (int y = 0; y < ChunkSize; y++)
            {
                tilemap.SetTile(
                    new Vector3Int(startX + x, startY + y, 0),
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

        // 랜덤 프리팹 선택
        for (int i = 0; i < num; i++)
        {
            GameObject randomPrefab =
                prefabs[Random.Range(0, prefabs.Length)];

            // 랜덤 위치 생성
            Vector3 randomPos = new Vector3(
                Random.Range(startX, startX + ChunkSize),
                Random.Range(startY, startY + ChunkSize),
                0
            );

            Collider2D hit = Physics2D.OverlapCircle(
           randomPos,
           checkRadius
       );

            // 프리팹 생성
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
    void OnDrawGizmos()
    {
        if (player == null)
            return;

        Vector2Int currentChunk = new Vector2Int(
        (int)(player.position.x / ChunkSize),
            (int)(player.position.y / ChunkSize)
        );

        foreach (Vector2Int chunk in generatedChunks)
        {
            // 현재 청크면 초록색
            if (chunk == currentChunk)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }

            Vector3 center = new Vector3(
                chunk.x * ChunkSize + ChunkSize / 2f,
                chunk.y * ChunkSize + ChunkSize / 2f,
                0
            );

            Vector3 size = new Vector3(
                ChunkSize,
                ChunkSize,
                0
            );

            Gizmos.DrawWireCube(center, size);
        }
    }
}

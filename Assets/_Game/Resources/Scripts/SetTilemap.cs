using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap tilemap;
    public Transform player;

    public TileBase[] tiles;

    public int ChunkSize = 10;

    HashSet<Vector2Int> generatedChunks = new HashSet<Vector2Int>();
    public int NumOfChunk = 2;

    void Start()
    {
        tiles = Resources.LoadAll<TileBase>("TileMap");
    }
    void Update()
    {
        int currentChunkX = Mathf.FloorToInt(player.position.x / ChunkSize);
        int currentChunkY = Mathf.FloorToInt(player.position.y / ChunkSize);

        for(int x = -NumOfChunk; x < NumOfChunk; x++)
        {
            for(int y = -NumOfChunk; y < NumOfChunk; y++)
            {
                Vector2Int chunkPos = new Vector2Int(
                currentChunkX + x,  
                currentChunkY + y
                );

                if (!generatedChunks.Contains(chunkPos))
                {
                    GenerateChunk(chunkPos);
                    generatedChunks.Add(chunkPos);
                }
            }
        }

        RemoveFarChunk(currentChunkX, currentChunkY);
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
                RemoveChunk(chunk);
                chunksToRemove.Add(chunk);
            }
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
        generatedChunks.Remove( chunkPos );
    }


}

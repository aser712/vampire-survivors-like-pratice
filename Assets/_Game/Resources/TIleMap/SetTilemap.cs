using UnityEngine;
using UnityEngine.Tilemaps;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Tilemap tilemap;

    public TileBase[] tiles;

    public int width = 50;
    public int height = 50;

    void Start()
    {
        tiles = Resources.LoadAll<TileBase>("TileMap");
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = -width/2; x < width/2; x++)
        {
            for (int y = -height/2; y < height/2; y++)
            {
                int rand = Random.Range(0, tiles.Length);

                tilemap.SetTile(
                    new Vector3Int(x, y, 0),
                    tiles[rand]
                );
            }
        }
    }
}

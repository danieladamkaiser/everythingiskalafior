using UnityEngine;

public class Garden : MonoBehaviour {
    public GameObject[] tilesObjects;
    private GardenTile[] tiles;
    private int len = 0;
    
    void Awake()
    {
        Debug.Log("Awake");
        tiles = new GardenTile[tilesObjects.Length];
        len = Mathf.CeilToInt(Mathf.Sqrt(tilesObjects.Length));
        for (int i = 0; i < tilesObjects.Length; ++i) {
            tiles[i] = tilesObjects[i].GetComponent<GardenTile>();
        }
    }

    public GardenTile OnMove(Vector2Int pos) {
        int it = pos.x * len + pos.y;
        if (pos.x < 0 || pos.x >= len || pos.y < 0 || pos.y >= len) {
            return null;
        }

        return tiles[it];
    }

    void Update()
    {
        
    }
}

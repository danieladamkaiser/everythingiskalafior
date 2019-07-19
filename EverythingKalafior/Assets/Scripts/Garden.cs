using UnityEngine;

public class Garden : MonoBehaviour {
    public GameObject[] tilesObjects;
    private GardenTile[] tiles;
    
    void Awake()
    {
        Debug.Log("Awake");
        tiles = new GardenTile[tilesObjects.Length];
        for (int i = 0; i < tilesObjects.Length; ++i) {
            tiles[i] = tilesObjects[i].GetComponent<GardenTile>();
        }
    }

    public GardenTile OnMove(Vector2Int pos) {
        int it = pos.x * 4 + pos.y;
        if (pos.x < 0 || pos.x >= 4 || pos.y < 0 || pos.y >= 4) {
            return null;
        }

        return tiles[it];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

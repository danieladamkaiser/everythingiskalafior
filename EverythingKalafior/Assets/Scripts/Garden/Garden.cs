using UnityEngine;

public class Garden : MonoBehaviour {
    public GameObject[] tilesObjects;
    private GardenTile[] tiles;
    public int unrobaczacz = int.MaxValue;
    private int len = 0;

    public bool IsUnrobaczaczAvailable() {
        return unrobaczacz > 0;
    }

    public void DecrementUnrobaczacz() {
        unrobaczacz--;
    }
    
    public void IncrementUnrobaczacz() {
        unrobaczacz++;
    }
    void Awake()
    {
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

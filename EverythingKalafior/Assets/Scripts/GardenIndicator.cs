using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class    GardenIndicator : MonoBehaviour
{
    public GameObject gardenObject;
    public Garden garden;
    private GardenTile tile;
    private Vector2Int position = Vector2Int.zero;
    
    void Start() {
        Debug.Log("Start");

        garden = gardenObject.GetComponent<Garden>();
        tile = garden.OnMove(position);
        if (!tile) {
            throw new Exception("null tile");
        }
        
        SetTile(tile, position);
    }

    void TryToMove() {
        var offset = Controlls.TileMoves();
        if (offset == Vector2.zero) {
            return;
        }

        var newTile = garden.OnMove(position + offset);
        if (newTile) {
            SetTile(newTile, offset);
        }
    }
    
    void SetTile(GardenTile t, Vector2Int offset) {
        transform.position = t.transform.position;
        tile = t;
        position += offset;
    }

    // Update is called once per frame
    void Update() {
        TryToMove();
    }
}

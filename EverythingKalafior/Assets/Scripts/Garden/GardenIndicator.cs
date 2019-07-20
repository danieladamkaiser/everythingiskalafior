using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GardenIndicator : MonoBehaviour
{
    public GameObject gardenObject;
    private Garden garden;
    private GardenTile tile;
    private Vector2Int position = Vector2Int.zero;
    public GameObject cauliflowerPrefab;
    private SpriteRenderer sRenderer;
    
    public void TurnOff() {
        sRenderer.enabled = false;
    }

    public void TurnOn() {
        sRenderer.enabled = true;
    }

    public void Notify(Vector2 pos) {
        // --
    }

    private void CreateFloatingIndicator(Cauliflower cf) {
        GameController.GetInstance().MinimizeCamera();
        GameController.GetInstance().OnNewCauliflower(cf);
    }
    
    void Start() {
        sRenderer = GetComponent<SpriteRenderer>();

        garden = gardenObject.GetComponent<Garden>();
        tile = garden.OnMove(position);
        if (!tile) {
            throw new Exception("null tile");
        }
        
        SetTile(tile, position);
    }

    void TryToMove() {
        var offset = GardenControlls.TileMoves();
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

    bool CanPlant() {
        return !tile.GetCauliflower();
    }

    bool CanUproot() {
        return true;
    }

    void TryUproot() {
        if (GardenControlls.UprootAction() && CanUproot()) {
            var cf = tile.Uproot();
            if (cf != null) {
                TurnOff();
                CreateFloatingIndicator(cf);
            }
        }
    }

    void TryPlant() {
        if (GardenControlls.PlantAction() && CanPlant()) {
            var c = Instantiate(cauliflowerPrefab).GetComponent<Cauliflower>();
            c.SetListener(this);
            tile.PlantCauliflower(c);
        }
    }

    void TryUnrobak() {
        if (GardenControlls.UnRobak() && garden.IsUnrobaczaczAvailable() && tile.UnRobak()) {
            garden.DecrementUnrobaczacz();
        }
    }
    
    void Update() {
        if (!sRenderer.enabled) {
            return;
        }
        
        TryToMove();
        TryUnrobak();
        TryPlant();
        TryUproot();
    }
}

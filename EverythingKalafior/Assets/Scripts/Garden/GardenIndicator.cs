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
    public GameObject floatingPrefab;
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
        var floating = Instantiate(floatingPrefab);
        var f = floating.GetComponent<FloatingIndicator>();

        floating.transform.position = GameController.GetInstance().GetCamPos() + Vector3.forward;
        GameController.GetInstance().MinimizeCamera();
        
        f.SetCauliflower(cf);
        f.SetListener(this);
    }
    
    void Start() {
        sRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Start");

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
            tile.PlantCauliflower(Instantiate(cauliflowerPrefab).GetComponent<Cauliflower>());
        }
    }
    
    void Update() {
        if (!sRenderer.enabled) {
            return;
        }
        
        TryToMove();
        TryPlant();
        TryUproot();
    }
}

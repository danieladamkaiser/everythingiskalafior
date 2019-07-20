using System;
using UnityEngine;

public class GardenTile : MonoBehaviour {
    private Cauliflower cauliflower;

    public void PlantCauliflower(Cauliflower c) {
        cauliflower = c;
        cauliflower.gameObject.transform.position = transform.position;
    }
    
    public Cauliflower GetCauliflower() {
        return cauliflower;
    }
    
    private void Update() {
        if (cauliflower) {
            cauliflower.Grow();
        }
    }
}


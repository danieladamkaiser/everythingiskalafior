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

    public Cauliflower Uproot() {
        if (cauliflower && cauliflower.IsGrownUp()) {
            cauliflower.Uproot();
            var tmp = cauliflower;
            cauliflower = null;
            return tmp;
        }

        return null;
    }
    
    private void Update() {
        if (cauliflower) {
            cauliflower.Grow();
        }
    }
}


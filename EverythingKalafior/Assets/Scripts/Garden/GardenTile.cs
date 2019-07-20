using System;
using UnityEngine;
using Random = System.Random;

public class GardenTile : MonoBehaviour {
    public GameObject robakPrefab;
    private GameObject robak;
    public float robakProbability;
    public float robakTimeout;
    private Cauliflower cauliflower;
    private float robakTimer;
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
            robakTimer = 0;
            cauliflower = null;
            return tmp;
        }

        return null;
    }

    public bool UnRobak() {
        if (robak) {
            Destroy(robak);
            robak = null;
            return true;
        }

        return false;
    }

    private void zarobakuj() {
        if (robakTimer > robakTimeout && !cauliflower.IsGrownUp()) {
            if (UnityEngine.Random.Range(0f, 1f) <= robakProbability) {
                robak = Instantiate(robakPrefab);
                var newpos = cauliflower.gameObject.transform.position;
                newpos.z = 1;
                robak.transform.position = newpos;
            }
            
            robakTimer = 0;
        }
    }
    
    private void Update() {
        if (cauliflower && !robak) {
            zarobakuj();
        }
        if (cauliflower)
            cauliflower.Grow(robak);

        if (!robak && cauliflower) {
            robakTimer += Time.deltaTime;
        }
    }
}


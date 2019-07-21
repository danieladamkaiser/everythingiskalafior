using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour {
    public GardenTile.Power pwr;
    // Start is called before the first frame update
    void Awake() {
        GardenTile.EnablePower(pwr);
        Debug.Log("PWR enavled");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

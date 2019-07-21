using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneTrigger : MonoBehaviour {
    public GameObject stonePos;
    public GameObject seedsPref;
    public GameObject stonePref;
    public GameObject seedsPos;
    public GameObject seedsPos1;
    public GameObject seedsPos2;
    
    void Start() {
        Instantiate(seedsPref, seedsPos.transform.position, Quaternion.identity);
        Instantiate(seedsPref, seedsPos1.transform.position, Quaternion.identity);
        Instantiate(seedsPref, seedsPos2.transform.position, Quaternion.identity);
        Instantiate(stonePref, stonePos.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Instantiate(seedsPref, seedsPos.transform.position, Quaternion.identity);
            Instantiate(seedsPref, seedsPos1.transform.position, Quaternion.identity);
            Instantiate(seedsPref, seedsPos2.transform.position, Quaternion.identity);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FloatingIndicator : MonoBehaviour {
    private Cauliflower cauliflower;
    private GardenIndicator indicator;

    public void SetCauliflower(Cauliflower cf) {
        cauliflower = cf;
    }

    public void SetListener(GardenIndicator ind) {
        indicator = ind;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (GardenControlls.ThrowCauliflower()) {
            indicator.TurnOn();
            indicator.Notify(cauliflower.transform.position);
            Destroy(cauliflower.gameObject);
            Destroy(gameObject);
        }
    }
}

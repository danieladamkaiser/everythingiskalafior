using System;
using UnityEngine;

public class Cauliflower : MonoBehaviour {
    public float growTime = 10f;
    private Animator anim;
    private float currentGrowTime = 0;
    private bool floating = false;
    public GameObject indicator;
    private GameObject realIndicator;
    private GardenIndicator gardenIndicator;
    public void SetListener(GardenIndicator gi) {
        gardenIndicator = gi;
    }

    public void OnThrow() {
        gardenIndicator.TurnOn();
        Destroy(gameObject);
        Destroy(realIndicator);
    }
    
    void Awake() {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if(realIndicator)
            realIndicator.transform.position = transform.position;
    }

    public void Uproot() {
        floating = true;
        anim.Play("floating");  
        anim.enabled = true;
        realIndicator = Instantiate(indicator);
        realIndicator.GetComponent<SpriteRenderer>().enabled = true;
    }

    void UpdateAnim() {
        if (growTime >= currentGrowTime) {
            var state = Utils.Remap(Mathf.Min(currentGrowTime, growTime), 0, growTime, 0, 0.9999f);
            anim.Play("GROW_NORMIK", 0, state);
        } else if (floating) {
            anim.Play("floating");  
            anim.enabled = true;
        } else {
            anim.Play("grown-up");
            anim.enabled = true;
        }
    }

    public void Grow() {
        currentGrowTime += Time.deltaTime;
        UpdateAnim();
    }

    public bool IsGrownUp() {
        return currentGrowTime > growTime;
    }
}

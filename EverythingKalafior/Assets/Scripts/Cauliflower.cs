using System;
using UnityEngine;

public class Cauliflower : MonoBehaviour {
    public float growTime = 10f;
    private Animator anim;
    private float currentGrowTime = 0;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void UpdateAnim() {
        if (growTime >= currentGrowTime) {
            var state = Utils.Remap(Mathf.Min(currentGrowTime, growTime), 0, growTime, 0, 0.9999f);
            anim.Play("GROW_NORMIK", 0, state);
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

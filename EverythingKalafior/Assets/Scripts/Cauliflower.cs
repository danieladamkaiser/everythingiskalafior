using UnityEngine;

public class Cauliflower : MonoBehaviour {
    public float growTime = 10f;
    private Animator anim;
    private float currentGrowTime = 0;

    string AnimName() {
        var info = anim.GetCurrentAnimatorClipInfo(0);
        return info[0].clip.name;    
    }

    void Start() {
        anim = GetComponent<Animator>();
        anim.speed = 0f;
        anim.Play(AnimName(), 0, 0);
    }

    void UpdateAnim() {
        var state = Utils.Remap(Mathf.Min(currentGrowTime, growTime), 0, growTime, 0, 0.9999f);
        anim.Play(AnimName(), 0, state);
    }

    public void Grow() {
        currentGrowTime += Time.deltaTime;
        UpdateAnim();
    }
}

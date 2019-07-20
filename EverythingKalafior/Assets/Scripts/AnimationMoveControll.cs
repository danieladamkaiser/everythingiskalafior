using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMoveControll : MonoBehaviour {
    private Animator anim;
    private bool ded = false;
    private Vector3 pos;

    public void Die() {
        ded = true;
    }
    void Start() {
        anim = GetComponent<Animator>();
        pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.x > pos.x) {
            anim.Play("RIGHT");
        } else if (transform.position.x < pos.x) {
            anim.Play("LEFT");
        } else if (ded) {
            anim.Play("DEAD");
        } else if (transform.position == pos) {
            anim.Play("IDLE");
        }

        pos = transform.position;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Seeds : MonoBehaviour
{
    public static void SetCnt(int x) {
        cnt = x;
    }
    
    public static int Count() {
        return cnt;
    }

    public static void Decrement() {
        cnt--;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
        cnt++;
    }

    private static int cnt;
}

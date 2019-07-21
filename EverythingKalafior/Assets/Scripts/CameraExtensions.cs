using System;
using UnityEngine;

public class CameraExtensions : MonoBehaviour {
    private static Camera mcamera;

    private void Awake() {
        mcamera = GetComponent<Camera>();
    }

    public static Bounds OrthographicBounds(Vector3 mod)
    {
        var t = mcamera.transform;
        var x = t.position.x;
        var y = t.position.y;
        var size = mcamera.orthographicSize * 2;
        var width = size * Screen.width / Screen.height;
        var height = size;
 
        return new Bounds(new Vector3(x, y, 0), new Vector3(width, height, 0) + mod);
    }
}
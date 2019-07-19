using UnityEngine;

public class Controlls {
    public static Vector2Int TileMoves() {
        Vector2Int vec = Vector2Int.zero;
        if (Input.GetKeyDown("w")) {
            vec.x--;
        }
        if (Input.GetKeyDown("s")) {
            vec.x++;
        }
        if (Input.GetKeyDown("a")) {
            vec.y--;
        }
        if (Input.GetKeyDown("d")) {
            vec.y++;
        }

        return vec;
    }
}

using Assets.Scripts;
using UnityEngine;

public class GardenControlls {
    public static Vector2Int TileMoves() {
        if (GameController.GetInstance().isCameraMinimized)
        {
            return Vector2Int.zero;
        }
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

    public static bool PlantAction()
    {
        return !GameController.GetInstance().isCameraMinimized && Input.GetKeyDown("space");
    }

    public static bool UprootAction() {
        return !GameController.GetInstance().isCameraMinimized && Input.GetKeyDown("space");
    }
    
    public static bool UnRobak() {
        return !GameController.GetInstance().isCameraMinimized && Input.GetKeyDown("space");
    }
}

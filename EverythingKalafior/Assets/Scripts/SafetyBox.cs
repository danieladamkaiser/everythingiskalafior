using UnityEngine;

namespace Assets.Scripts
{
    public class SafetyBox : MonoBehaviour
    {
        void OnTriggerExit2D(Collider2D collision)
        {
            GameController.GetInstance().RemovePlayerFromScene();
        }
    }
}

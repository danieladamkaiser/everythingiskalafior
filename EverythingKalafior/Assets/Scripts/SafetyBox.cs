using UnityEngine;

namespace Assets.Scripts
{
    public class SafetyBox : MonoBehaviour
    {
        private GameController gc;

        private void Awake()
        {
            gc = GameController.GetInstance();
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (gc.Player == collision.gameObject)
            {
                Debug.LogWarning("Destroyed kalafior");
                GameController.GetInstance().RemovePlayerFromScene();
            }
        }
    }
}

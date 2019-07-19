using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface IPlayerController
    {
        void Walk(float direction);
        void Jump();
        void Die();
    }
}

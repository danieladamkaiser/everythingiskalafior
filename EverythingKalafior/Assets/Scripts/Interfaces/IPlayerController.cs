using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface IPlayerController
    {
        void Walk(Vector2 direction);
        void Jump();
        void Freeze();
    }
}

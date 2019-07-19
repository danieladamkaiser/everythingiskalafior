using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class KeyboardInput : IInput
    {
        public float GetHorizontalAxisValue()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        public float GetVerticalAxisValue()
        {
            return Input.GetAxisRaw("Vertical");
        }

        public KeyStatus GetKeyStatus(KeyCode key)
        {
            if (Input.GetKeyDown(key)) return KeyStatus.JustDown;
            if (Input.GetKeyUp(key)) return KeyStatus.JustUp;
            if (Input.GetKey(key)) return KeyStatus.Down;
            return KeyStatus.Up;
        }
    }
}

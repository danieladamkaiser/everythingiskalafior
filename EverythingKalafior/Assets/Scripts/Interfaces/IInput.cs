using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IInput
    {
        float GetHorizontalAxisValue();
        float GetVerticalAxisValue();
        KeyStatus GetKeyStatus(KeyCode key);
    }
}

using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Serializable
{
    [Serializable]
    public class PlayerConfig
    {
        public float MoveSpeed;
        public float JumpForce;
        public InputType inputType;
    }
}

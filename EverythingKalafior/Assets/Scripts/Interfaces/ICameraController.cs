﻿using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ICameraController
    {
        void FollowTarget(GameObject target);
        void ResizeCamera(Vector2 fromPosition, Vector2 size, float? depth = null);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class MouseFollower : MonoBehaviour
    {
        public Camera relativeCamera;

        public void FollowMouse(Rigidbody2D rb)
        {
            if (rb != null && relativeCamera != null)
            {
                rb.MovePosition(Vector3.Lerp(rb.position, relativeCamera.ScreenToWorldPoint(Input.mousePosition), 0.25f));
            }
        }
    }
}

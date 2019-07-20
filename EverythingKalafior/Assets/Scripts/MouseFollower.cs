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
        public Rigidbody2D rb;
        public Camera relativeCamera;


        void Update()
        {
            if (rb != null && relativeCamera != null)
            {
                FollowMouse();
            }
        }
        public void FollowMouse()
        {
            rb.MovePosition(relativeCamera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}

using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour, ICameraController
    {
        private Camera cam;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }


        public void FollowTarget(GameObject target)
        {
            transform.position = new Vector3(target.transform.position.x, transform.position.y);
        }

        public void ResizeCamera(Vector2 fromPosition, Vector2 size, float? depth = null)
        {
            cam.rect = new Rect(fromPosition, size);
            if (depth!=null)
            {
                cam.depth = depth.Value;
            }
        }
    }
}

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

        private Vector3 currentOffset;
        
        public GameObject[] parallax;
        public float[] parallaxModifiers;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }


        public void FollowTarget(GameObject target) {
            var n = new Vector3(target.transform.position.x, transform.transform.position.y, transform.position.z);
            currentOffset = n - transform.position;
            transform.position = n;
        }

        public void ResizeCamera(Vector2 fromPosition, Vector2 size, float? depth = null)
        {
            cam.rect = new Rect(fromPosition, size);
            if (depth!=null)
            {
                cam.depth = depth.Value;
            }
        }

        private void Update() {
            for (int i = 0; i < parallax.Length; i++) {
                var newPos = parallax[i].transform.position;
                newPos.x += currentOffset.x * parallaxModifiers[i];
                newPos.y += currentOffset.y * parallaxModifiers[i];
                parallax[i].transform.position = newPos;
            }
        }
    }
}

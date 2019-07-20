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

        public float yOff = 0;
        public GameObject[] parallax;
        public float[] parallaxModifiers;

        private GameObject target = null;

        public void SetTarget(GameObject t) {
            target = t;
        }
        
        void Awake()
        {
            cam = GetComponent<Camera>();
        }

        public void SetPosition(Vector2 position) {
            cam.transform.position = new Vector3(position.x, position.y, cam.transform.position.z);
        }

        Vector3 FollowTarget(GameObject target) {
            var n = new Vector3(target.transform.position.x, target.transform.position.y + yOff, transform.position.z);
            var off =  n - transform.position;
            n = Vector3.Lerp(transform.position, n, 0.25f);
            transform.position = n;
            return off;
        }

        public void ResizeCamera(Vector2 fromPosition, Vector2 size, float? depth = null)
        {
            cam.rect = new Rect(fromPosition, size);
            if (depth!=null)
            {
                cam.depth = depth.Value;
            }
        }

        private void FixedUpdate() {
            if (!target) {
                return;
            }
            
            var off = FollowTarget(target);

            for (int i = 0; i < parallax.Length; i++) {
                var newPos = parallax[i].transform.position;
                newPos.x += off.x * parallaxModifiers[i];
                parallax[i].transform.position = newPos;
            }
        }
    }
}

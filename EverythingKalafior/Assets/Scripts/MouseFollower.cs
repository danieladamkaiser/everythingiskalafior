using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class MouseFollower : MonoBehaviour {
        private Rigidbody2D rb;
        private GameObject gj;
        
        private void Start() {
            gj = Instantiate(new GameObject(), transform.position, Quaternion.identity);
            activate();
        }

        public void activate() {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Update() {
            var bounds = CameraExtensions.OrthographicBounds(new Vector3(-1.2f, -1.2f, 999));
            var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

            var npos = gj.transform.position + move * Time.deltaTime * Mathf.PI;

            if (!GameController.GetInstance().isCameraMinimized || !bounds.Contains(npos)) {
                return;
            }

            gj.transform.position = npos;
            rb.MovePosition(gj.transform.position);
        }
    }
}

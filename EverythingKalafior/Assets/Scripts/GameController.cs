using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public GameObject PlayerPrefab;
        public Transform SpawnPosition;
        public CameraController GardenCamera;
        private bool cameraMinimized;
        private float aspectRatio;

        void Awake()
        {
            aspectRatio = Screen.width * 1f / Screen.height;
            MinimizeCamera();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SpawnPlayer();
            }
        }

        private void SpawnPlayer()
        {
            Instantiate(PlayerPrefab, SpawnPosition);
        }

        private void MaximizeCamera()
        {
            GardenCamera.ResizeCamera(new Vector2(0.05f, 0.35f), new Vector2(0.6f / aspectRatio, 0.6f));
            cameraMinimized = false;
        }

        private void MinimizeCamera()
        {
            GardenCamera.ResizeCamera(new Vector2(0.02f, 0.75f), new Vector2(0.2f / aspectRatio, 0.2f));
            cameraMinimized = true;
        }

        private void LoadLevel(int level)
        {

        }
        
        private void ShowLoadingScreen(float duration)
        {

        }

    }
}

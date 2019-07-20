﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public GameObject PlayerPrefab;
        public GameObject GardenPrefab;
        public GameObject PlaygroundPrefab;
        public Transform SpawnPosition;
        public MouseFollower mouseFollower;
        public Scene[] scenes;

        private int currentLevel;
        private CameraController gardenCameraController;
        private CameraController playgroundCameraController;
        private bool isCameraMinimized;
        private float aspectRatio;
        private Transform levelStart;
        private Transform levelEnd;

        private GameObject player;

        void Awake()
        {
            aspectRatio = Screen.width * 1f / Screen.height;
            var garden = Instantiate(GardenPrefab, new Vector3(-100, 0, -1), Quaternion.identity);
            gardenCameraController = garden.GetComponent<CameraController>();
            mouseFollower = GetComponent<MouseFollower>();
            var playground = Instantiate(PlaygroundPrefab, new Vector3(0, 0, -1), Quaternion.identity);
            mouseFollower.relativeCamera = playground.GetComponent<Camera>();
            playgroundCameraController = playground.GetComponent<CameraController>();
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(garden);
            DontDestroyOnLoad(playground);
        }

        void Start()
        {
            MinimizeCamera();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (isCameraMinimized)
                {
                    MaximizeCamera();
                }
                else
                {
                    MinimizeCamera();
                }
            }

            if (player!=null)
            {
                playgroundCameraController.FollowTarget(player);
            }
        }

        private void SpawnPlayer()
        {
            player = Instantiate(PlayerPrefab, SpawnPosition);
        }

        private void MaximizeCamera()
        {
            gardenCameraController.ResizeCamera(new Vector2(0.05f, 0.35f), new Vector2(0.6f / aspectRatio, 0.6f));
            isCameraMinimized = false;
        }

        private void MinimizeCamera()
        {
            gardenCameraController.ResizeCamera(new Vector2(0.02f, 0.75f), new Vector2(0.2f / aspectRatio, 0.2f));
            isCameraMinimized = true;
        }

        private void LoadLevel(int level)
        {
            SceneManager.LoadScene(level);
        }
        
        private void ShowLoadingScreen(float duration)
        {

        }

        private void FindStartAndEnd()
        {
            levelStart = GameObject.Find("start").transform;
            levelEnd = GameObject.Find("end").transform;
        }
    }
}

using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour, GardenListener
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
        private Rigidbody2D dummieCaliflowerRB;
        private Cauliflower cauliflower;
        private bool isCarried;

        private static GameController instance;

        void Awake()
        {
            instance = this;

            aspectRatio = Screen.width * 1f / Screen.height;
            var garden = Instantiate(GardenPrefab, new Vector3(-100, 0, -1), Quaternion.identity);
            gardenCameraController = garden.GetComponent<CameraController>();
            mouseFollower = GetComponent<MouseFollower>();
            var playground = Instantiate(PlaygroundPrefab, new Vector3(0, 0, -1), Quaternion.identity);
            var playGroundCamera = GameObject.Find("PlayGroundCamera").GetComponent<Camera>();
            playgroundCameraController = playGroundCamera.GetComponent<CameraController>();
            mouseFollower.relativeCamera = playGroundCamera;
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(garden);
            DontDestroyOnLoad(playground);
        }

        public static GameController GetInstance()
        {
            return instance;
        }

        public Vector3 GetCamPos()
        {
            return playgroundCameraController.transform.position;
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

            if (isCarried)
            {
                mouseFollower.FollowMouse(dummieCaliflowerRB);
                if (Input.GetMouseButtonDown(0))
                {
                    isCarried = false;
                    SpawnPlayer();
                    player.GetComponent<PlayerController>().WakeUp();
                    cauliflower?.OnThrow();
                    cauliflower = null;
                }
            }

            if (player != null)
            {
                playgroundCameraController.FollowTarget(player);
            }
        }

        private void SpawnPlayer()
        {
            player = Instantiate(PlayerPrefab, cauliflower.transform.position, cauliflower.transform.rotation);
        }

        private void MaximizeCamera()
        {
            gardenCameraController.ResizeCamera(new Vector2(0.05f, 0.35f), new Vector2(0.6f / aspectRatio, 0.6f));
            isCameraMinimized = false;
        }

        public void MinimizeCamera()
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

        public void OnNewCauliflower(Cauliflower cf)
        {
            cauliflower = cf;
            cf.transform.position = new Vector3(playgroundCameraController.transform.position.x, playgroundCameraController.transform.position.y, cf.transform.position.z);
            if (player != null)
            {
                player.GetComponent<PlayerController>().Die();
            }
            player = null;
            cf.gameObject.AddComponent<MouseFollower>();
            dummieCaliflowerRB = cf.gameObject.AddComponent<Rigidbody2D>();
            isCarried = true;
        }
    }
}

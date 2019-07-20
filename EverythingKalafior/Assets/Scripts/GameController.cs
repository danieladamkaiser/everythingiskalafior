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
        public string[] sceneNames;
        public bool isCameraMinimized;

        private int currentLevel = 0;
        private CameraController gardenCameraController;
        private CameraController playgroundCameraController;
        private float aspectRatio;
        private GameObject levelStart;
        private GameObject levelEnd;

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
            LoadLevel(sceneNames[currentLevel]);
        }

        void Update()
        {
            if (levelStart==null || levelEnd == null)
            {
                FindStartAndEnd();
            }
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

                if (levelEnd!=null && Vector2.Distance(player.transform.position, levelEnd.transform.position) < 1)
                {
                    LoadLevel(sceneNames[++currentLevel]);
                }
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

        private void LoadLevel(string level)
        {
            Destroy(player);
            player = null;
            SceneManager.LoadScene(level);
            FindStartAndEnd();
        }

        private void ShowLoadingScreen(float duration)
        {

        }

        private void FindStartAndEnd()
        {
            levelStart = GameObject.Find("start");
            levelEnd = GameObject.Find("end");
            if (levelStart!=null)
            {
                playgroundCameraController.SetPosition(levelStart.transform.position);
            }
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
            dummieCaliflowerRB.freezeRotation = true;
            isCarried = true;
        }
    }
}

using System.Runtime.CompilerServices;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
        public BoxCollider2D SafetyCollider;
        public GameObject Player;
        public TextMeshProUGUI tmp;
        public GameObject winTex;
        private int currentLevel = 0;
        private CameraController gardenCameraController;
        private CameraController playgroundCameraController;
        private float aspectRatio;
        private GameObject levelStart;
        private GameObject levelEnd;

        private Rigidbody2D dummieCaliflowerRB;
        private Cauliflower cauliflower;

        private static GameController instance;

        private bool win = false;

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
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(garden);
            DontDestroyOnLoad(playground);
            DontDestroyOnLoad(winTex);
            tmp = GameObject.Find("SeedCounter").GetComponent<TextMeshProUGUI>();
        }

        public static GameController GetInstance()
        {
            return instance;
        }

        public Vector3 GetCamPos() {
            var pos = playgroundCameraController.transform.position;
            pos.z = 0;
            return pos;
        }

        void Start()
        {
            MinimizeCamera();
            LoadLevel(sceneNames[currentLevel]);
        }

        void Update()
        {
            if ( Input.GetKeyDown(KeyCode.R) )
                LoadLevel(sceneNames[currentLevel]);

            if (win) {
                winTex.transform.position = GetCamPos();
                winTex.GetComponent<SpriteRenderer>().enabled = true;
                if (Input.GetKeyDown(KeyCode.Return)) {
                    win = false;
                    winTex.GetComponent<SpriteRenderer>().enabled = false;
                    LoadLevel(sceneNames[++currentLevel]);
                }
                return;
            }
            
            tmp.text = Seeds.Count().ToString();

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

            if (cauliflower && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0)))
            {
                SpawnPlayer();
                Player.GetComponent<PlayerController>().WakeUp();
                cauliflower.OnThrow();
                cauliflower = null;
            }

            if (Player != null)
            {
                if (levelEnd!=null && Vector2.Distance(Player.transform.position, levelEnd.transform.position) < 0.3f) {
                    win = true;
                    Player.GetComponent<PlayerController>().Die();
                }
            }
        }

        private void SpawnPlayer()
        {
            Player = Instantiate(PlayerPrefab, cauliflower.transform.position, cauliflower.transform.rotation);
            playgroundCameraController.SetTarget(Player);
            Player.GetComponent<PlayerController>().dir = cauliflower.GetDir();
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
            Destroy(Player);
            playgroundCameraController.SetTarget(null);

            Player = null;
            levelStart = null;
            levelEnd = null;
            SceneManager.LoadScene(level);
            FindStartAndEnd();
            Seeds.SetCnt(1);
        }

        public void RemovePlayerFromScene()
        {
            Destroy(Player);
            Player = null;
            playgroundCameraController.SetPosition(levelStart.transform.position);
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
            if (Player != null)
            {
                Player.GetComponent<PlayerController>().Die();
                playgroundCameraController.SetTarget(null);
            }
            Player = null;
            cf.gameObject.AddComponent<MouseFollower>();
            dummieCaliflowerRB = cf.gameObject.AddComponent<Rigidbody2D>();
            dummieCaliflowerRB.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            dummieCaliflowerRB.freezeRotation = true;
            dummieCaliflowerRB.gravityScale = 0;
        }
    }
}

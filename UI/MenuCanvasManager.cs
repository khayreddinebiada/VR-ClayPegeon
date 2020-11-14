using game.control;
using game.data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace game.ui
{
    public class MenuCanvasManager : MonoBehaviour
    {
        public static MenuCanvasManager instance;

        [Header("Controllers")]
        [SerializeField]
        private Hand hand;

        [SerializeField]
        private LayerMask layers;
        private RaycastHit _hit;

        [SerializeField]
        private bool showHitPoint = false;
        [SerializeField]
        private Transform hitPoint;
        [SerializeField]
        private ControllerInputs controllerInputs;

        [Header("Gun Info")]
        [SerializeField]
        private Image movingOnShoot;
        [SerializeField]
        private Image focusRadius;
        [SerializeField]
        private Image additionalBullets;

        [Header("UI")]
        [SerializeField]
        private GameObject loadingPanel;
        private GunInfo _gunInfo;
        [SerializeField]
        private Text _textPrice;




        private void Awake()
        {
            instance = this;
        }


        void Start()
        {
            if (!showHitPoint)
            {
                hitPoint.gameObject.SetActive(false);
            }

            UpdateGunInfo();
            UpdatePrice();
        }

        void LateUpdate()
        {
#if !UNITY_EDITOR
            hand.transform.rotation = GetControllerHandRotation();
            UpdateHitPoint();
            if (controllerInputs.isShootingDown && _hit.collider != null)
            {
                Button button = _hit.collider.gameObject.GetComponent<Button>();
                if(button != null && button.enabled)
                    button.onClick.Invoke();
            }
#endif
        }

        public void UpdateGunInfo()
        {
            _gunInfo = GlobalData.instance.GetCurrentGunInfo();
            _gunInfo.UpdateDataOnCanvas(movingOnShoot, focusRadius, additionalBullets);
        }

        private void UpdateHitPoint()
        {
            Physics.Raycast(hand.transform.position, hand.transform.forward, out _hit, layers);

            if (showHitPoint)
            {
                if (_hit.collider != null)
                {
                    hitPoint.gameObject.SetActive(true);
                    hitPoint.position = _hit.point;
                    hitPoint.rotation = Quaternion.LookRotation(_hit.normal);
                }
                else
                {
                    hitPoint.gameObject.SetActive(false);
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
            {
                if (_hit.collider != null)
                {
                    Button button = _hit.collider.gameObject.GetComponent<Button>();
                    if (button != null && button.enabled)
                    {
                        button.onClick.Invoke();
                    }
                }
            }
        }

        public void Play()
        {
            int envi = EnvironmentsManager.instance.indexCurrentEnvironment;
            SceneManager.LoadSceneAsync("Level" + (envi + 1));
            loadingPanel.SetActive(true);
        }

        public Quaternion GetControllerHandRotation()
        {
            Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
            return rotation;
        }

        public void UpdatePrice()
        {
            _textPrice.text = GlobalData.TotalCoins().ToString() + "$";
        }
    }
}

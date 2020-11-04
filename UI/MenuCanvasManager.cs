using game.control;
using game.data;
using UnityEngine;
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
        private Image movingOnShoot;



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
        }

        void Update()
        {

#if !UNITY_EDITOR
            hand.transform.rotation = GetControllerHandRotation();
#endif
            UpdateHitPoint();

            if (controllerInputs.isShootingDown && _hit.collider != null)
            {
                Button button = _hit.collider.gameObject.GetComponent<Button>();
                if(button != null)
                    button.onClick.Invoke();
            }
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
                    if (button != null)
                    {
                        button.onClick.Invoke();
                    }
                }
            }

        }

        public Quaternion GetControllerHandRotation()
        {
            Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
            return rotation;
        }

    }
}

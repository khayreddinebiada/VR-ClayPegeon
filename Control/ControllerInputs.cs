using UnityEngine;

namespace game.control
{
    public class ControllerInputs : MonoBehaviour
    {
        public Vector3 deltaDroping;
        public bool isShootingPress = false;
        public bool isShootingDown = false;
        public bool isCharging = false;
        // Start is called before the first frame update
        void Start()
        {
            Cursor.visible = false;
        }

        // Update is called once per frame
        void Update()
        {
            OnPCController();
            ControllerShootingInputs();
        }

        private void OnPCController()
        {
            deltaDroping = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }

        public Quaternion GetControllerHandRotation()
        {

            return OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        }

        private void ControllerShootingInputs()
        {
#if !UNITY_EDITOR
            isShootingPress = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);
            isShootingDown = OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger);
            isCharging = OVRInput.GetDown(OVRInput.Button.One);
#else
            isShootingPress = Input.GetMouseButton(0);
            isShootingDown = Input.GetMouseButtonDown(0);
            isCharging = Input.GetKeyDown(KeyCode.R);
#endif
        }
    }
}

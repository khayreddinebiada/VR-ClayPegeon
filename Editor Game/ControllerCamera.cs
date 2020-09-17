using UnityEngine;
using game.control;

    public class ControllerCamera : MonoBehaviour
    {

        [SerializeField]
        private ControllerInputs controllerInputs;

        [SerializeField]
        private float rotateSpeed;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

#if UNITY_EDITOR
            RotateCamera();
#endif
        }


        // RotateCamera for controller the rotation of camera in environment deltaDroping is the value of input from ControllerInputs class.
        private void RotateCamera()
        {
            Vector3 euler = transform.rotation.eulerAngles +
                            new Vector3(-controllerInputs.deltaDroping.y, controllerInputs.deltaDroping.x) *
                            rotateSpeed * Time.deltaTime;

            Quaternion rotation = Quaternion.Euler(euler);
            transform.rotation = rotation;
        }

        
    }

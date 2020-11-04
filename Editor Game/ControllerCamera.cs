using UnityEngine;
using game.control;
using UnityEngine.UI;

public class ControllerCamera : MonoBehaviour
{
    private ControllerInputs controllerInputs;

    [SerializeField]
    private float rotateSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        controllerInputs = GetComponentInChildren<ControllerInputs>();
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
                        new Vector3(-ControllerInputs.instance.deltaDroping.y, ControllerInputs.instance.deltaDroping.x) *
                        rotateSpeed * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(euler);
        transform.rotation = rotation;
    }


}

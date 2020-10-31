using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace game.ui
{
    public class VRControllerUI : MonoBehaviour
    {
        [SerializeField]
        private Hand hand;

        [SerializeField]
        private LayerMask layers;

        private RaycastHit hit;

        void Start()
        {

        }

        void Update()
        {

#if !UNITY_EDITOR
            hand.transform.rotation = GetControllerHandRotation();
#endif
            UpdateHitPoint();

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetMouseButtonDown(0))
            {
                if (hit.collider != null)
                {
                    VRButton button = hit.collider.gameObject.GetComponent<VRButton>();
                    if (button != null)
                    {
                        button.onClick.Invoke();
                    }
                }
            }

            if(hit.collider != null)
            {
                hand.ChangePositionHitPoint(hit.point);
                print(hit.collider.gameObject.name);
            }
            else
            {
                hand.ChangePositionHitPoint();
            }
        }

        private void UpdateHitPoint()
        {
            Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, layers);
        }

        public Quaternion GetControllerHandRotation()
        {
            Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
            return rotation;
        }
    }
}

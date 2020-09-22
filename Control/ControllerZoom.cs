using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace game.control
{
    public class ControllerZoom : MonoBehaviour
    {
        public bool isZoom = false;

        [SerializeField]
        private ControllerGun controllerGun;
        [SerializeField]
        private Transform cameraBody;
        public Transform cameraCenterView;
        [SerializeField]
        private GameObject[] zoomImage;
        public GameObject cameras;
        [SerializeField]
        private UnityEvent onActivateModeZoom;
        [SerializeField]
        private UnityEvent onDeactivateModeZoom;

        private void Update()
        {
            ControllerCameraOnZome();
        }

        private void ControllerCameraOnZome()
        {
            
            RaycastHit _hit;
            Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out _hit, Mathf.Infinity);
        }

        public void ActivateModeZoom()
        {
            if (!controllerGun.gun.zoomSupport)
                return;

            if (!isZoom)
            {
                controllerGun.gun.body.SetActive(false);
                cameraBody.SetParent(controllerGun.transform);

                foreach (GameObject ima in zoomImage)
                {
                    ima.SetActive(true);
                }

                foreach (Camera camera in cameras.GetComponentsInChildren<Camera>())
                {
                    camera.fieldOfView = controllerGun.gun.zoonFieldValue;
                }
                
                onActivateModeZoom.Invoke();
                isZoom = true;
            }
        }
        public void DeactivateModeZoom()
        {
            if (!controllerGun.gun.zoomSupport)
                return;

            if (isZoom)
            {
                controllerGun.gun.body.SetActive(true);
                cameraBody.SetParent(controllerGun.transform.parent);

                foreach (GameObject ima in zoomImage)
                {
                    ima.SetActive(false);
                }
                foreach (Camera camera in cameras.GetComponentsInChildren<Camera>())
                {
                    camera.fieldOfView = controllerGun.gun.unzoonFieldValue;
                }

                onDeactivateModeZoom.Invoke();
                isZoom = false;
            }
        }
    }
}

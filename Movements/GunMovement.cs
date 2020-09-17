using System.Collections;
using UnityEngine;
using game.control;
using game.objects;
using System;

namespace game.movement
{
    [RequireComponent(typeof(ControllerGun))]
    [RequireComponent(typeof(ControllerInputs))]
    public class GunMovement : MonoBehaviour
    {
        [SerializeField]
        private float speedResetRotation;
        [SerializeField]
        private float focusSpeed;
        [SerializeField]
        private float timeWaitingAndReset = 0.2f;
        [SerializeField]
        private Vector3 focusRotation;


        private bool _setReset = false;
        private bool _setShooting = false;
        private Gun _gun;
        private ControllerGun _controllerGun;
        private ControllerInputs _controllerInputs;

        private void Awake()
        {
            _controllerInputs = GetComponent<ControllerInputs>();
            _controllerGun = GetComponent<ControllerGun>();
            _gun = _controllerGun.gun;
        }

        private void Update()
        {
#if !UNITY_EDITOR
            ControllerGunRotation();
#endif
            if (_setReset)
            {
                ResetRotation();
            }
            if (_setShooting)
            {
                _gun.body.transform.Rotate(focusRotation * focusSpeed * Time.deltaTime);
            }
        }

        private void ControllerGunRotation()
        {
            transform.rotation = _controllerInputs.GetControllerHandRotation();
        }

        private void ResetRotation()
        {
            _gun.body.transform.localRotation = Quaternion.RotateTowards(_gun.body.transform.localRotation, Quaternion.identity, speedResetRotation * Time.deltaTime);
        }

        public void Shooting()
        {
            StartCoroutine(WaitAndReset());
            _setReset = false;
            _setShooting = true;
        }

        IEnumerator WaitAndReset()
        {
            yield return new WaitForSeconds(timeWaitingAndReset);
            _setReset = true;
            _setShooting = false;
        }
        
    }
}

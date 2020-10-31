using System.Collections;
using UnityEngine;
using game.control;
using game.objects;
using game.manager;
using game.data;

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


        private bool _setReset = false;
        private bool _setShooting = false;
        private GunInfo _gun;
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
            if (_controllerGun.isCharging || GameManager.instance.GameIsPaused())
                return;

#if !UNITY_EDITOR
            ControllerGunRotation();
#endif
            if (_setReset)
            {
                ResetRotation();
            }
            if (_setShooting)
            {
                _gun.body.transform.Rotate(_gun.movingOnShoot * focusSpeed * Time.deltaTime);
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

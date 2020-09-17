using UnityEngine;
using game.objects;
using game.movement;

namespace game.control
{
    [RequireComponent(typeof(ControllerInputs))]
    [RequireComponent(typeof(GunMovement))]
    public class ControllerGun : MonoBehaviour
    {

        [Header("Controller Gun")]
        public Gun gun;
        [SerializeField]
        private GameObject bullEffectPrefab;
        private ControllerInputs _controllerInputs;
        private GunMovement _gunMovement;
        private RaycastHit _hitPoint;
        private float waitingTime = 0;

        [Header("Controller TargetPoint")]
        [SerializeField]
        private Transform targetPoint;
        [SerializeField]
        private float scaleOfTargetPoint = 0.65f;
        [SerializeField]
        private LayerMask layerMaskEnvironment;
        [SerializeField]
        private float maxDistance = 300;
        [SerializeField]
        private float minDistance = 5;

        // Start is called before the first frame update
        void Awake()
        {
            _gunMovement = GetComponent<GunMovement>();
            _controllerInputs = GetComponent<ControllerInputs>();
        }

        // Update is called once per frame
        void Update()
        {
            ControllerTargetPoint();
            ControllerShooting();
        }

        #region Gun
        private void ControllerShooting()
        {
            if (!gun.machineGun)
            {
                waitingTime -= Time.deltaTime;
                if (_controllerInputs.isShootingDown && waitingTime <= 0)
                {
                    ShootingBullet();
                    waitingTime = gun.detlaTimeBetweenBullets;
                    _controllerInputs.isShootingDown = false;
                }
            }
            else
            {
                waitingTime -= Time.deltaTime;
                if (_controllerInputs.isShootingPress && waitingTime <= 0)
                {
                    ShootingBullet();
                    waitingTime = gun.detlaTimeBetweenBullets;
                }
            }
        }

        private void ShootingBullet()
        {
            float randX = Random.Range(-gun.focusRadius, gun.focusRadius);
            float randY = Random.Range(-gun.focusRadius, gun.focusRadius);
            Physics.Raycast(transform.position, gun.body.transform.TransformDirection(Vector3.forward + new Vector3(randX, randY)), out _hitPoint, maxDistance, layerMaskEnvironment);

            if (_hitPoint.collider != null)
            {
                GameObject obj = Instantiate(bullEffectPrefab, _hitPoint.point, Quaternion.LookRotation(_hitPoint.normal));
            }
            _gunMovement.Shooting();
        }
        #endregion

        #region Target Point
        private void ControllerTargetPoint()
        {
            RaycastHit _hit;
            if (Physics.Raycast(transform.position, gun.body.transform.TransformDirection(Vector3.forward), out _hit, maxDistance, layerMaskEnvironment))
            {
                float scale = Mathf.Clamp((_hit.distance - 5) * scaleOfTargetPoint, 1, Mathf.Infinity);
                targetPoint.localScale = new Vector3(1, 1, 1) * scale;
                targetPoint.position = _hit.point;

                if (_hit.distance <= minDistance)
                {
                    targetPoint.localScale = new Vector3(1, 1, 1);
                    targetPoint.localPosition = Vector3.forward;
                }
            }
            else
            {
                targetPoint.localScale = new Vector3(1, 1, 1) * maxDistance * scaleOfTargetPoint;
                targetPoint.localPosition = Vector3.forward * maxDistance;
            }

        }
        #endregion
    }
}
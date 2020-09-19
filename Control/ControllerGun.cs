﻿using System.Collections;
using UnityEngine;
using game.objects;
using game.movement;
using game.animation;
using game.manager;
using UnityEngine.Events;

namespace game.control
{
    [RequireComponent(typeof(ControllerInputs))]
    [RequireComponent(typeof(GunMovement))]
    public class ControllerGun : MonoBehaviour
    {
        [Header("Controller Gun")]
        public Gun gun;
        public bool isCharging = false;
        public bool isStop = false;
        [SerializeField]
        private UnityEvent shootingEffect;
        [SerializeField]
        private GameObject bullEffectPrefab;
        private ControllerInputs _controllerInputs;
        private GunMovement _gunMovement;
        private RaycastHit _hitPoint;
        private float waitingTime = 0;

        [Header("Animation")]
        [SerializeField]
        private GunAnimation gunAnimation;

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

        private void Start()
        {
            if(gun.gunContain == 0)
            {
                ChargeGun();
            }
        }


        // Update is called once per frame
        void Update()
        {

            ControllerTargetPoint();

            if (isCharging || isStop)
                return;

            CheckChargeGun();
            ControllerShooting();
        }

        #region Gun
        private void CheckChargeGun()
        {
            if (_controllerInputs.isCharging || (gun.gunContain == 0 && 0 < gun.maxSavingBullets))
            {
                ChargeGun();
            }
        }

        public void ChargeGun()
        {
            if (0 < gun.maxSavingBullets)
            {
                int delta = gun.maxBulletInGun - gun.gunContain;
                if (delta < gun.maxSavingBullets)
                {
                    gun.gunContain = gun.maxBulletInGun;
                    gun.maxSavingBullets -= delta;
                }
                else
                {
                    gun.gunContain += gun.maxSavingBullets;
                    gun.maxSavingBullets = 0;
                }

                // Statements below excute  when player charge the gun.
                isCharging = true;
                gunAnimation.ChargeGun();
                StartCoroutine(WaitAndDeactivate());
            }
            else
            {
                print("Your saving is empty");
            }
        }

        IEnumerator WaitAndDeactivate()
        {
            yield return new WaitForSeconds(1);
            isCharging = false;
            gunAnimation.DeactivateChargeGun();
        }

        private void ControllerShooting()
        {
            if (gun.gunContain <= 0)
                return;

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

        // When the player shoot will excute this function.
        private void ShootingBullet()
        {

            float randX = Random.Range(-gun.focusRadius, gun.focusRadius);
            float randY = Random.Range(-gun.focusRadius, gun.focusRadius);
            Physics.Raycast(transform.position, gun.body.transform.TransformDirection(Vector3.forward + new Vector3(randX, randY)), out _hitPoint, maxDistance, layerMaskEnvironment);

            if (_hitPoint.collider != null)
            {
                GameObject obj = Instantiate(bullEffectPrefab, _hitPoint.point, Quaternion.LookRotation(_hitPoint.normal));
                Target target = _hitPoint.transform.GetComponent<Target>();
                if (target != null)
                {
                    obj.transform.SetParent(target.body.transform);
                    target.onHit.Invoke();
                }
            }

            gun.gunContain--;
            _gunMovement.Shooting();
            shootingEffect.Invoke();

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
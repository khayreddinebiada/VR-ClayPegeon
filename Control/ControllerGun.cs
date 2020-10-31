using System.Collections;
using UnityEngine;
using game.objects;
using game.movement;
using game.animation;
using game.manager;
using UnityEngine.Events;
using UnityEngine.UI;
using game.ui;
using game.data;

namespace game.control
{
    [RequireComponent(typeof(ControllerInputs))]
    [RequireComponent(typeof(GunMovement))]
    public class ControllerGun : MonoBehaviour
    {

        [Header("Controller Gun")]
        public GunInfo gun;
        public bool isCharging = false;
        [SerializeField]
        private UnityEvent shootingEffect;
        [SerializeField]
        private UnityEvent onCharge;
        [SerializeField]
        private GameObject bullEffectPrefab;
        [SerializeField]
        private bool isShootgun = false;
        [SerializeField]
        private int shootgunBullets = 3;

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


        private float waitingTime = 0;
        private ControllerInputs _controllerInputs;
        private GunMovement _gunMovement;
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


            // text.text = _controllerInputs.isCharging.ToString();
            if (isCharging || GameManager.instance.GameIsPaused())
                return;


            CheckChargeGun();
            ControllerShooting();
            ControllerTargetPoint();
        }

        #region Gun

        private void CheckChargeGun()
        {
            MainCanvasManager.instance.ChargeTheGun(gun.gunContain, gun.maxSavingBullets);
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
                onCharge.Invoke();
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
                    ShootingBullet(shootgunBullets);
                    waitingTime = gun.detlaTimeBetweenBullets;
                    _controllerInputs.isShootingDown = false;
                    gun.gunContain--;
                    _gunMovement.Shooting();
                    shootingEffect.Invoke();
                    MainCanvasManager.instance.RemoveOneBullet(gun.maxSavingBullets);
                    MainCanvasManager.instance.ChargeTheGun(gun.gunContain, gun.maxSavingBullets);

                }
            }
            else
            {
                waitingTime -= Time.deltaTime;
                if (_controllerInputs.isShootingPress && waitingTime <= 0)
                {
                    ShootingBullet(shootgunBullets);
                    waitingTime = gun.detlaTimeBetweenBullets;
                    gun.gunContain--;
                    _gunMovement.Shooting();
                    shootingEffect.Invoke();
                    MainCanvasManager.instance.RemoveOneBullet(gun.maxSavingBullets);
                    MainCanvasManager.instance.ChargeTheGun(gun.gunContain, gun.maxSavingBullets);

                }
            }
        }

        // When the player shoot will excute this function.
        private void ShootingBullet(int loop)
        {

            float randX = Random.Range(-gun.focusRadius, gun.focusRadius);
            float randY = Random.Range(-gun.focusRadius, gun.focusRadius);

            RaycastHit[] _hitPoint;
            _hitPoint = Physics.RaycastAll(transform.position, gun.body.transform.TransformDirection(Vector3.forward + new Vector3(randX, randY)), maxDistance, layerMaskEnvironment);
            foreach (RaycastHit hit in _hitPoint)
            {
                CheckPointHits(hit);
            }

            RaycastHit hitGlass;
            Physics.Raycast(transform.position, gun.body.transform.TransformDirection(Vector3.forward + new Vector3(randX, randY)), out hitGlass,  maxDistance, layerMaskEnvironment);
            CheckPointHitsGlass(hitGlass);

            loop--;
            if (0 < loop && isShootgun)
                ShootingBullet(loop);
        }

        private void CheckPointHitsGlass(RaycastHit hit)
        {
            if (hit.collider != null)
            {
                if (!hit.collider.gameObject.CompareTag("Glass"))
                    return;

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.onHit.Invoke();
                    target.CalculateScoreOnHit(hit.point);
                }
            }
        }

        private void CheckPointHits(RaycastHit hit)
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Glass"))
                    return;

                GameObject obj = Instantiate(bullEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    obj.transform.SetParent(target.body.transform);
                    target.onHit.Invoke();
                    target.CalculateScoreOnHit(hit.point);
                }
            }
        }
        #endregion

        #region Target Point
        private void ControllerTargetPoint()
        {
            RaycastHit _hit;
            Transform point = targetPoint;
            Transform center = gun.body.transform;
            if (Physics.Raycast(transform.position, center.TransformDirection(Vector3.forward), out _hit, maxDistance, layerMaskEnvironment))
            {
                if (_hit.collider.gameObject.layer == 5 && _controllerInputs.isShootingDown)
                {
                    Instantiate(bullEffectPrefab, _hit.point, Quaternion.LookRotation(_hit.normal));
                    VRButton vRButton = _hit.collider.gameObject.GetComponent<VRButton>();
                    _gunMovement.Shooting();
                    shootingEffect.Invoke();

                    if(vRButton != null)
                        vRButton.onClick.Invoke();

                    return;
                }

                float scale = Mathf.Clamp((_hit.distance - 5) * scaleOfTargetPoint, 1, Mathf.Infinity);
                point.localScale = new Vector3(1, 1, 1) * scale;
                point.position = _hit.point;

                if (_hit.distance <= minDistance)
                {
                    point.localScale = new Vector3(1, 1, 1);
                    point.localPosition = Vector3.forward;
                }
            }
            else
            {
                point.localScale = new Vector3(1, 1, 1) * maxDistance * scaleOfTargetPoint;
                point.localPosition = Vector3.forward * maxDistance;
            }

        }
        #endregion
    }
}
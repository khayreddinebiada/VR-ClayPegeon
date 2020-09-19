using UnityEngine;
using System.Collections;

namespace game.objects
{
    [RequireComponent(typeof(Target))]
    [RequireComponent(typeof(BoxCollider))]
    public class StaticTarget : MonoBehaviour
    {
        private bool isActivated = false;
        [SerializeField]
        private Transform gotTo;
        [SerializeField]
        private float speedMoving = 10;
        [SerializeField]
        private float speedRotation = 10;

        private BoxCollider _boxCollider;
        private Target _target;
        private float _waitAndDestroyObject = 3;

        // Start is called before the first frame update
        void Awake()
        {
            _boxCollider = GetComponent<BoxCollider>();
            _target = GetComponent<Target>();
        }
        private void Start()
        {
            if (isActivated)
            {
                _boxCollider.enabled = true;
            }
            else
            {
                _boxCollider.enabled = false;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (!isActivated)
            {
                _target.body.transform.position = Vector3.MoveTowards(_target.body.transform.position, gotTo.position, speedMoving * Time.deltaTime);
                _target.body.transform.rotation = Quaternion.RotateTowards(_target.body.transform.rotation, gotTo.rotation, speedRotation * Time.deltaTime);
            }
            else
            {
                _target.body.transform.position = Vector3.MoveTowards(_target.body.transform.position, transform.position, speedMoving * Time.deltaTime);
                _target.body.transform.rotation = Quaternion.RotateTowards(_target.body.transform.rotation, transform.rotation, speedRotation * Time.deltaTime);
            }
        }

        public void StartShowTarget()
        {
            isActivated = true;
            _boxCollider.enabled = true;
        }

        public void OnHit()
        {
            isActivated = false;
            _boxCollider.enabled = false;
            StartCoroutine(WaitAndDestroy());
        }

        IEnumerator WaitAndDestroy()
        {
            yield return new WaitForSeconds(_waitAndDestroyObject);
            Destroy(this);
        }

    }
}

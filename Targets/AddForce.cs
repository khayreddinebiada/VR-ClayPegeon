using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.target
{
    public class AddForce : MonoBehaviour
    {
        [SerializeField]
        private bool byRandom = false;
        [SerializeField]
        private float _forceValue;
        [SerializeField]
        private Vector3 _hitPoint;
        private Rigidbody _rigidbody;

        [SerializeField]
        private bool _byRandomTorque = false;
        [SerializeField]
        private float _torqueValue;
        [SerializeField]
        private Vector3 _forceTorque;

        // Start is called before the first frame update
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void AddForceOnHit()
        {
            if(!byRandom)
                _rigidbody.AddForce(_hitPoint * _forceValue);
            else
                _rigidbody.AddForce((new Vector3(Random.Range(-1,1), Random.Range(0.5f, 1), Random.Range(0.5f, 1))) * _forceValue);

            if (_byRandomTorque)
                _rigidbody.AddTorque(new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 1), Random.Range(0.5f, 1)) * _torqueValue);
            else
                _rigidbody.AddTorque(_forceTorque * _torqueValue);
        }

    }
}

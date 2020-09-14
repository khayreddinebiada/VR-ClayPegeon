using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.control
{
    public class ControllerGun : MonoBehaviour
    {
        [SerializeField]
        private Transform targetPoint;

        [SerializeField]
        public Ray r
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ControllerGunRotation();
        }

        private void ControllerGunRotation()
        {
            transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);
        }

        private void ControllerTargetPoint()
        {
            _targetPoint *= 
        }
    }
}
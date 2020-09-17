using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.animation
{
    [RequireComponent(typeof(Animator))]
    public class GunAnimation : MonoBehaviour
    {
        private Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void ChargeGun()
        {
            animator.enabled = true;
            animator.Play("Charge Gun");
        }
        public void DeactivateChargeGun()
        {
            animator.enabled = false;
        }
    }
}

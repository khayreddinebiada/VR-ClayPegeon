using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game.target
{
    public class PendingBottleRotate : MonoBehaviour
    {
        private Animator _animator;

        [SerializeField]
        private Transform[] _glassPieces;
        // Start is called before the first frame update
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _animator.enabled = false;
            StartCoroutine(WaitAndStartAnimation());
        }
        private IEnumerator WaitAndStartAnimation()
        {
            yield return new WaitForSeconds(Random.Range(0, 1f));
            _animator.enabled = true;
        }

        private void GoToInitPoint()
        {
            _animator.SetBool("Go Center", true);
        }

        private void SetParentsForPieces()
        {
            foreach (Transform item in _glassPieces)
            {
                item.SetParent(transform.parent);
            }
        }

        public void OnHit()
        {
            GoToInitPoint();
            SetParentsForPieces();
        }
    }
}

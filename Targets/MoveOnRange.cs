using UnityEngine;
using System.Collections;

namespace game.target
{
    public class MoveOnRange : MonoBehaviour
    {

        private Target target;
        [SerializeField]
        private bool _isMove = true;
        [SerializeField]
        private float speedMoving = 1;
        [SerializeField]
        private float timeForChange = 10;
        [SerializeField]
        private Vector3 _axis;

        private bool _onRight = false;

        // Start is called before the first frame update
        void Awake()
        {
            target = GetComponent<Target>();
            StartCoroutine(WaitAndChange());
        }

        // Update is called once per frame
        void Update()
        {
            if (_isMove)
            {
                transform.localPosition += _axis * ((_onRight) ? -1 : 1) * speedMoving * Time.deltaTime;
            }
        }

        private IEnumerator WaitAndChange()
        {
            yield return new WaitForSeconds(timeForChange);
            _onRight = !_onRight;
            StartCoroutine(WaitAndChange());
        }

        private void StopMove()
        {
            _isMove = false;
        }
    }
}
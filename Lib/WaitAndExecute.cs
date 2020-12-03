using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace game.lib
{
    public class WaitAndExecute : MonoBehaviour
    {
        [SerializeField]
        private float _timeWaiting = 1;
        [SerializeField]
        private UnityEvent _onTime;

        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(WaitAndExcecute());
        }

        private IEnumerator WaitAndExcecute()
        {
            yield return new WaitForSeconds(_timeWaiting);
            _onTime.Invoke();
        }
    }
}

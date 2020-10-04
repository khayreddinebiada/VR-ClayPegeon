using game.objects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace game.manager
{
    public class TargetManager : MonoBehaviour
    {
        public enum TargetType { OneByOne, AllGroup };

        public static TargetManager instance;
        [SerializeField]
        private TargetType targetType = TargetType.OneByOne;
        [SerializeField]
        private UnityEvent onAllTargetsHited;

        private Target[] _targets;

        private int targetHited = 0;
        void Awake()
        {
            instance = this;
            _targets = GetComponentsInChildren<Target>();
        }

        // Start is called before the first frame update
        void Start()
        {
            switch(targetType)
            {
                case TargetType.OneByOne:
                    _targets[targetHited].StartShowTarget();
                    break;
                case TargetType.AllGroup:
                    foreach(Target target in _targets)
                        target.StartShowTarget();
                    break;
            }
            
        }

        public void OnHitOneTarget()
        {
            targetHited++;

            if(targetHited == _targets.Length)
            {
                onAllTargetsHited.Invoke();
                print("Game finished");
            }
            else
            {
                if(targetType == TargetType.OneByOne)
                    _targets[targetHited].StartShowTarget();
            }
        }
    }
}
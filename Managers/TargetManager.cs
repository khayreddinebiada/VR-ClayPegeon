using game.objects;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace game.manager
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager instance;

        [SerializeField]
        private UnityEvent onAllTargetsHited;

        public List <Target> targets;

        private int targetHited = 0;
        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {

            targets[targetHited].StartShowTarget();
        }

        public void OnHitOneTarget()
        {
            targetHited++;

            if(targetHited == targets.Count)
            {
                onAllTargetsHited.Invoke();
                print("Game finished");
            }
            else
            {
                targets[targetHited].StartShowTarget();
            }
        }
    }
}
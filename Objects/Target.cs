using UnityEngine;
using UnityEngine.Events;

namespace game.objects
{
    public class Target : MonoBehaviour
    {
        public enum TargetType
        {
            Static = 0,
            Duck = 0,
            Pigeon  = 0
        };

        public UnityEvent onHit;

        [Header("Gizmas")]
        [SerializeField]
        private bool makeGizmas = false;
        [SerializeField]
        private Color color = Color.red;



        [Header("Data")]
        [SerializeField]
        public TargetType targetType;
        public GameObject body;
        public float factorPoint;
        [SerializeField]
        private Transform center;
        public bool isRemoved = false;
        public float radius = 1;

        private void Awake()
        {

        }


        private void OnDrawGizmos()
        {
            if (makeGizmas)
            {
                Gizmos.color = color;
                Gizmos.DrawSphere(center.position, radius);
            }
        }

        public void StartShowTarget()
        {
            switch (targetType)
            {
                case TargetType.Static:
                    GetComponent<StaticTarget>().StartShowTarget();
                    break;
            }
        }
    }

}
using UnityEngine;
using UnityEngine.Events;

namespace game.objects
{
    public class Target : MonoBehaviour
    {
        public enum TargetType
        {
            Static = 0,
            Duck = 1,
            Pigeon  = 2
        };

        public UnityEvent onHit;

        [Header("Gizmas")]
        [SerializeField]
        private bool activateGizmas = false;
        [SerializeField]
        private Color color = Color.red;



        [Header("Data")]
        [SerializeField]
        private int score = 0;
        [SerializeField]
        public TargetType targetType;
        public GameObject body;
        public Transform center;
        public bool isRemoved = false;
        public float radius = 1;
        [SerializeField]
        private float[] scoreRanges;

        private void Awake()
        {

        }


        private void OnDrawGizmos()
        {
            if (activateGizmas)
            {
                Gizmos.color = color;
                Gizmos.DrawSphere(center.position, radius);
            }
        }

        public int GetScore()
        {
            return score;
        }

        public void CalculateScoreOnHit(Vector3 hitPointPosition)
        {
            float distance = Vector3.Distance(hitPointPosition, center.position) / radius;
            print(distance);
            for (int i = 0; i < scoreRanges.Length - 1; i++)
            {
                if (scoreRanges[i] <= distance && distance < scoreRanges[i + 1])
                {
                    score = 10 - i;
                    return;
                }
            }
            score = 5;
            return;
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
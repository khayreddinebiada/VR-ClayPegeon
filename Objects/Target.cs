using game.manager;
using UnityEngine;
using UnityEngine.Events;

namespace game.objects
{
    public class Target : MonoBehaviour
    {
        public enum TargetType
        {
            TargetField = 0,
            Glass = 1,
            Object  = 2
        };

        public UnityEvent onHit;

        [Header("Gizmas")]
        [SerializeField]
        private bool activateGizmas = false;
        [SerializeField]
        private Color color = Color.red;



        [Header("Data")]
        public int index;
        public bool isScoreByPoints = true;
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

        [SerializeField]
        private bool destroyIfTouchFloor = false;

        private TargetManager targetManager;

        private void Awake()
        {
            index = transform.GetSiblingIndex();
            targetManager = GetComponentInParent<TargetManager>();
            onHit.AddListener(() => targetManager.OnHitOneTarget(index));

            if(targetType == TargetType.Glass)
            {
                onHit.AddListener(() => {
                    GetComponent<Animator>().enabled = true;
                });
            }
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
            if (targetType != TargetType.TargetField)
                return;

            float distance = Vector3.Distance(hitPointPosition, center.position) / radius;
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

        public void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.layer == 8)
            {
                if (destroyIfTouchFloor)
                {
                    onHit.Invoke();
                }
            }
        }
        public int GetLastScoreForAdd()
        {
            isRemoved = true;
            return score;
        }
        public void StartShowTarget()
        {
            switch (targetType)
            {
                case TargetType.TargetField:
                    GetComponent<MovementTarget>().StartShowTarget();
                    break;
            }
        }
    }

}
using game.manager;
using UnityEngine;
using UnityEngine.Events;

namespace game.target
{
    public class Target : MonoBehaviour
    {
        public enum TargetType
        {
            TargetField = 0,
            Glass = 1,
            Object  = 2,
            Can
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
            targetManager = GetComponentInParent<TargetManager>();
            onHit.AddListener(() => targetManager.OnHitOneTarget(index));

            if (targetType == TargetType.Glass)
            {
                onHit.AddListener(() => {
                    GetComponent<Animator>().enabled = true;
                });
            }

            if (targetType == TargetType.Can)
            {
                onHit.AddListener(() => {
                    GetComponent<AddForce>().AddForceOnHit();
                });
            }

            radius = radius * transform.localScale.x;
            if(center == null && isScoreByPoints)
            {
                Debug.LogError("You need check IsScoreByPoints and Center Variables");
            }
        }


        public int GetScore()
        {
            return score;
        }

        public void CalculateScoreOnHit(Vector3 hitPointPosition)
        {
            if (!isScoreByPoints)
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
                    if (targetType != TargetType.Can)
                        onHit.Invoke();
                    else
                    {
                        targetManager.OnHitOneTarget(index);
                    }

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
using game.objects;
using game.others;
using game.ui;
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

        private int totalPoints;
        [SerializeField]
        private Target[] _targets;

        private int targetHited = 0;
        [SerializeField]
        private Transform numbers;
        private Audio[] audioNumbers;

        void Awake()
        {
            audioNumbers = numbers.GetComponentsInChildren<Audio>();

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

        public int GetTotalPoints()
        {
            return totalPoints;
        }

        public void OnHitOneTarget(int currentTargetIndexHited)
        {
            if (_targets[currentTargetIndexHited].isRemoved)
                return;

            audioNumbers[_targets[currentTargetIndexHited].GetScore() - 1].Play();

            totalPoints += _targets[currentTargetIndexHited].GetLastScoreForAdd();

            MainCanvasManager.instance.UpdateTotalPoint(totalPoints);

            targetHited++;


            if (targetHited == _targets.Length)
            {
                onAllTargetsHited.Invoke();
                GameManager.instance.EndGame();
            }
            else
            {
                if(targetType == TargetType.OneByOne)
                    _targets[currentTargetIndexHited].StartShowTarget();
            }

        }
    }
}
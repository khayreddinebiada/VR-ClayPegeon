using game.target;
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
        public Transform numbers;
        private Audio[] audioNumbers;

        [Header("Bullets and Coin")]
        [SerializeField]
        private int _bulletOnLevel = 5;
        public int bulletOnLevel
        {
            get { return _bulletOnLevel; }
        }
        [SerializeField]
        private int _scaleTimeCoin = 1;
        public int scaleTimeCoin
        {
            get { return _scaleTimeCoin; }
        }
        [SerializeField]
        private int _scaleStarCoin = 10;
        public int scaleStarCoin
        {
            get { return _scaleStarCoin; }
        }

        private void Awake()
        {
            instance = this;
            _targets = GetComponentsInChildren<Target>();

            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i].index = i;
            }
        }

        // Start is called before the first frame update
        private void Start()
        {
            audioNumbers = numbers.GetComponentsInChildren<Audio>();
            switch (targetType)
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

            int score = _targets[currentTargetIndexHited].GetScore();

            if (0 < score && score <= audioNumbers.Length)
                audioNumbers[score - 1].Play();

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
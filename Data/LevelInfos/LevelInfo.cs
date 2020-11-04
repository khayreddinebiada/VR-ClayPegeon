using UnityEngine;

namespace game.data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Add Level", order = 1)]
    public class LevelInfo : ScriptableObject

    {

        [SerializeField]
        private int _indexLevel = -1;
        public int indexLevel
        {
            get { return _indexLevel; }
        }

    }
}

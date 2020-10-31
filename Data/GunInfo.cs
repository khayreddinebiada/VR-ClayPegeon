using game.objects;
using UnityEngine;

namespace game.data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Add Gun", order = 1)]
    public class GunInfo : ScriptableObject
    {

        [SerializeField]
        private int _index;
        public int Index
        {
            get { return _index; }
        }

        [SerializeField]
        private string _name;
        public string Name
        {
            get { return _name; }
        }

        [SerializeField]
        private int _unlockLevel = -1;
        public int unlockLevel
        {
            get { return _unlockLevel; }
        }

        [SerializeField]
        private int _price = -1;
        public int price
        {
            get { return _price; }
        }

        [SerializeField]
        private Vector3 _movingOnShoot = new Vector3(-0.5f, 0.1f, 0);
        public Vector3 movingOnShoot
        {
            get { return _movingOnShoot; }
        }

        public int gunContain = 0;

        [SerializeField]
        private int _maxBulletInGun = 2;
        public int maxBulletInGun
        {
            get { return _maxBulletInGun; }
        }

        public int maxSavingBullets = 12;
        public bool machineGun = false;

        [SerializeField]
        private float _detlaTimeBetweenBullets = 0.1f;
        public float detlaTimeBetweenBullets
        {
            get { return _detlaTimeBetweenBullets; }
        }

        [SerializeField]
        private float _focusRadius;
        public float focusRadius
        {
            get { return _focusRadius; }
        }

        [SerializeField]
        private GameObject _body;
        public GameObject body
        {
            get { return _body; }
        }

    }
}

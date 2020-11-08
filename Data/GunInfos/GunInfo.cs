using UnityEngine;
using UnityEngine.UI;

namespace game.data
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Add Gun", order = 1)]
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
        private GameObject _prefabOnPlay;
        public GameObject prefabOnPlay
        {
            get { return _prefabOnPlay; }
        }

        [SerializeField]
        private bool _isShootgun;
        public bool isShootgun
        {
            get { return _isShootgun; }
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
        [Range(0,1)]
        private float percentMovingOnShoot = 0.1f;
        [SerializeField]
        [Range(0, 1)]
        private float percentFocusRadius = 0.1f;
        [SerializeField]
        [Range(0,1)]
        private float percentAdditionalBullets = 0.1f;

        public void UpdateDataOnCanvas(Image imageMovingOnShoot, Image imageFocusRadius, Image imageAdditionalBullets)
        {
            imageMovingOnShoot.fillAmount = percentMovingOnShoot;
            imageFocusRadius.fillAmount = percentFocusRadius;
            imageAdditionalBullets.fillAmount = percentAdditionalBullets;
        }
    }
}

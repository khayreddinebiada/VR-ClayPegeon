using UnityEngine;

namespace game.objects
{
    public class Gun : MonoBehaviour
    {
        public bool machineGun;
        public float detlaTimeBetweenBullets = 0.1f;
        public Bullet bullet;
        public float focusRadius;
        public GameObject body;
    }

}

using UnityEngine;

namespace game.objects
{
    public class Gun : MonoBehaviour
    {
        public bool zoomSupport = false;
        public int zoonFieldValue = 42;
        public int unzoonFieldValue = 60;
        public Vector3 factorControllerGun = new Vector3(-0.5f, 0.1f, 0);
        public int gunContain = 0;
        public int maxBulletInGun = 2;
        public int maxSavingBullets = 12;
        public bool machineGun;
        public float detlaTimeBetweenBullets = 0.1f;
        public Bullet bullet;
        public float focusRadius;
        public GameObject body;
    }

}

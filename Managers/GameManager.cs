using UnityEngine;
using game.control;


namespace game.manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public ControllerGun controllerGun;
        public TargetManager targetManager;

        // Start is called before the first frame update
        void Awake()
        {
            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
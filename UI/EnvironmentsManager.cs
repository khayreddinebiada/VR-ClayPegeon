using game.data;
using UnityEngine;
using UnityEngine.UI;

namespace game.ui
{
    public class EnvironmentsManager : MonoBehaviour
    {
        private static EnvironmentsManager _instance;
        public static EnvironmentsManager instance
        {
            get { return _instance; }
        }

        private Environment[] environments;
        [SerializeField]
        private Transform _content;

        [SerializeField]
        private Image selectedImage;

        private int _indexCurrentEnvironment;
        public int indexCurrentEnvironment
        {
            get { return _indexCurrentEnvironment; }
        }

        private void Awake()
        {
            _instance = this;
            environments = _content.GetComponentsInChildren<Environment>();
        }

        // Start is called before the first frame update
        void Start()
        {
            SelectCurrentEnvironment();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SelectCurrentEnvironment()
        {
            foreach (Environment env in environments)
            {
                if (!GlobalData.EnvironmentIsCompleted(env.indexEnvironment))
                {
                    selectedImage.transform.position = env.transform.position;
                    _indexCurrentEnvironment = env.indexEnvironment;
                    break;
                }
            }
        }

    }
}

using game.data;
using game.manager;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace game.ui
{
    public class MainCanvasManager : MonoBehaviour
    {
        public static MainCanvasManager instance;

        [Header("View")]
        [SerializeField]
        private GameObject prefbBulletImage;
        [SerializeField]
        private GameObject _winPanel;
        [SerializeField]
        private GameObject _lostPanel;

        [Header("On Game")]
        [SerializeField]
        private Transform bulletsPanel;
        [SerializeField]
        private Text totalBullet;
        [SerializeField]
        private Text timeOnGame;
        [SerializeField]
        private Text textTotalPoints;

        [Header("On Win")]
        [SerializeField]
        private Text textTimeOnWin;
        [SerializeField]
        private Text textCoinOnWin;
        [SerializeField]
        private Text textPointOnWin;
        [SerializeField]
        private Text textBonus;
        [SerializeField]
        private GameObject _newEnvironmentPanel;
        public GameObject newEnvironmentPanel
        {
            get { return _newEnvironmentPanel; }
        }

        [Header("On Lost")]
        [SerializeField]
        private Text textTimeOnLost;
        [SerializeField]
        private Text textCoinOnLost;
        [SerializeField]
        private Text textPointOnLost;

        [Header("End Game")]
        [SerializeField]
        private Animator starAnim;

        // Start is called before the first frame update
        void Awake()
        {
            instance = GetComponent<MainCanvasManager>();
        }

        private void Update()
        {
            timeOnGame.text = (SecondToTime((int)GameManager.instance.GetCurrentTime()));
        }

        public void MakeStars(int numberStars)
        {
            switch (numberStars)
            {
                case 1:
                    starAnim.Play("1 Star");
                    break;
                case 2:
                    starAnim.Play("2 Star");
                    break;
                case 3:
                    starAnim.Play("3 Star");
                    break;
            }
        }

        private string SecondToTime(int sec)
        {
            int minutes = Mathf.FloorToInt(sec / 60);
            int seconds = Mathf.FloorToInt(sec % 60);
            return (minutes + ":" + ((seconds < 10) ? ("0" + seconds.ToString()) : seconds.ToString()));
        }

        public void ChargeTheGun(int gunContain, int total)
        {
            totalBullet.text = total.ToString();

            for (int i = bulletsPanel.childCount; i < gunContain; i++)
            {
                GameObject obj = Instantiate(prefbBulletImage, bulletsPanel);
            }
        }

        public void UpdateTotalPoint(int points)
        {
            textTotalPoints.text = points.ToString();
        }

        public void PlayerLost(float time, int coin, int points)
        {
            textTimeOnLost.text = (SecondToTime((int)GameManager.instance.GetCurrentTime()));
            textCoinOnLost.text = coin + "$";
            textPointOnLost.text = points.ToString();
            StartCoroutine(WaitAndShowPanel(_lostPanel, 2));
        }

        public void PlayerWin(float time, int coin, int points)
        {
            textTimeOnWin.text = (SecondToTime((int)GameManager.instance.GetCurrentTime()));
            textCoinOnWin.text = coin + "$";
            textPointOnWin.text = points.ToString();
            StartCoroutine(WaitAndShowPanel(_winPanel, 2));
        }

        private IEnumerator WaitAndShowPanel(GameObject panel, float time)
        {
            yield return new WaitForSeconds(time);
            panel.SetActive(true);
        }

        public void GoToNextLevel()
        {
            GameManager.instance.GoToTheNextLevel();
        }

        public void AddBonus(int bonusAmount)
        {
            textBonus.text = "+" + bonusAmount.ToString() + "$";
        }

        public void RemoveOneBullet(int total)
        {
            totalBullet.text = total.ToString();
            Destroy(bulletsPanel.GetChild(0).gameObject);
        }

        public void Replay()
        {
            GameManager.instance.Replay();
        }

        public void GoMenu()
        {
            GameManager.instance.GoMenu();
        }
    }
}
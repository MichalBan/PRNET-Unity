using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scenes.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public Button PlayButton, StatsButton, ExitButton;
        // Start is called before the first frame update
        void Start()
        {
            PlayButton.onClick.AddListener(Play);
            StatsButton.onClick.AddListener(Stats);
            ExitButton.onClick.AddListener(Exit);
        }

        void Play()
        {
            SceneManager.LoadScene("GameScene");
        }

        void Stats()
        {
            SceneManager.LoadScene("StatsScene");
        }

        void Exit()
        {
            Debug.Log("exitgame");
            Application.Quit();
        }
    }
}

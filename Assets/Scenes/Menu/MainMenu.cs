using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scenes.Menu
{
    public class MainMenu : MonoBehaviour
    {
        public Button PlayButton, StatsButton, ExitButton;
        public TMP_InputField InputText;

        // Start is called before the first frame update
        void Start()
        {
            PlayButton.onClick.AddListener(Play);
            StatsButton.onClick.AddListener(Stats);
            ExitButton.onClick.AddListener(Exit);
            InputText.onValueChanged.AddListener(LimitNickLength);

            PlayButton.interactable = false;
            StatsButton.interactable = false;

            if (PlayerDataHolder.Instance.PlayerNick != null)
            {
                InputText.SetTextWithoutNotify(PlayerDataHolder.Instance.PlayerNick);
                PlayButton.interactable = true;
                StatsButton.interactable = true;
            }
        }

        private void LimitNickLength(string input)
        {
            if (input.Length > 20)
            {
                InputText.SetTextWithoutNotify(input[..20]);
            }

            if (input.Length > 0)
            {
                PlayButton.interactable = true;
                StatsButton.interactable = true;
                PlayerDataHolder.Instance.PlayerNick = InputText.text;
            }
            else
            {
                PlayButton.interactable = false;
                StatsButton.interactable = false;
            }
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